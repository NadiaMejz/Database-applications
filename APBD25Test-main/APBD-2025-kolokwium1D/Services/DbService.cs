using APBD_2025_kolokwium1D.Exceptions;
using APBD_2025_kolokwium1D.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD_2025_kolokwium1D.Services
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;
        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
        }

        public async Task<BookingResponseDto> GetBookingByIdAsync(int bookingId)
        {
            const string query = @"
                SELECT b.date,
                       g.first_name, g.last_name, g.date_of_birth,
                       e.first_name, e.last_name, e.employee_number,
                       a.name, a.price, ba.amount
                FROM Booking b
                JOIN Guest g             ON b.guest_id       = g.guest_id
                JOIN Employee e          ON b.employee_id    = e.employee_id
                JOIN Booking_Attraction ba ON b.booking_id    = ba.booking_id
                JOIN Attraction a        ON ba.attraction_id  = a.attraction_id
                WHERE b.booking_id = @bookingId;";

            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@bookingId", bookingId);
            await conn.OpenAsync();

            await using var rdr = await cmd.ExecuteReaderAsync();
            BookingResponseDto booking = null;

            while (await rdr.ReadAsync())
            {
                if (booking == null)
                {
                    booking = new BookingResponseDto
                    {
                        Date = rdr.GetDateTime(0),
                        Guest = new GuestDto
                        {
                            FirstName = rdr.GetString(1),
                            LastName = rdr.GetString(2),
                            DateOfBirth = rdr.GetDateTime(3)
                        },
                        Employee = new EmployeeDto
                        {
                            FirstName = rdr.GetString(4),
                            LastName = rdr.GetString(5),
                            EmployeeNumber = rdr.GetString(6)
                        },
                        Attractions = new List<BookingAttractionDto>()
                    };
                }

                booking.Attractions.Add(new BookingAttractionDto
                {
                    Name = rdr.GetString(7),
                    Price = rdr.GetDecimal(8),
                    Amount = rdr.GetInt32(9)
                });
            }

            if (booking == null)
                throw new NotFoundException($"Booking with ID {bookingId} not found.");

            return booking;
        }

        public async Task AddBookingAsync(CreateBookingRequestDto request)
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand { Connection = conn };
            await conn.OpenAsync();

            await using var tx = await conn.BeginTransactionAsync();
            cmd.Transaction = (SqlTransaction)tx;

            try
            {
                cmd.CommandText = "SELECT 1 FROM Booking WHERE booking_id = @bookingId;";
                cmd.Parameters.AddWithValue("@bookingId", request.BookingId);
                if (await cmd.ExecuteScalarAsync() != null)
                    throw new ConflictException($"Booking with ID {request.BookingId} already exists.");
                cmd.Parameters.Clear();

                cmd.CommandText = "SELECT 1 FROM Guest WHERE guest_id = @guestId;";
                cmd.Parameters.AddWithValue("@guestId", request.GuestId);
                if (await cmd.ExecuteScalarAsync() == null)
                    throw new NotFoundException($"Guest with ID {request.GuestId} not found.");
                cmd.Parameters.Clear();

                cmd.CommandText = "SELECT employee_id FROM Employee WHERE employee_number = @empNum;";
                cmd.Parameters.AddWithValue("@empNum", request.EmployeeNumber);
                var empObj = await cmd.ExecuteScalarAsync();
                if (empObj == null)
                    throw new NotFoundException($"Employee with number {request.EmployeeNumber} not found.");
                var employeeId = Convert.ToInt32(empObj);
                cmd.Parameters.Clear();

                cmd.CommandText = @"
                    INSERT INTO Booking (booking_id, guest_id, employee_id, [date])
                    VALUES (@bookingId, @guestId, @employeeId, GETDATE());";
                cmd.Parameters.AddWithValue("@bookingId", request.BookingId);
                cmd.Parameters.AddWithValue("@guestId", request.GuestId);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();

                foreach (var a in request.Attractions)
                {
                    cmd.CommandText = "SELECT attraction_id FROM Attraction WHERE name = @name;";
                    cmd.Parameters.AddWithValue("@name", a.Name);
                    var attrObj = await cmd.ExecuteScalarAsync();
                    if (attrObj == null)
                        throw new NotFoundException($"Attraction '{a.Name}' not found.");
                    var attractionId = Convert.ToInt32(attrObj);
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"
                        INSERT INTO Booking_Attraction (booking_id, attraction_id, amount)
                        VALUES (@bookingId, @attractionId, @amount);";
                    cmd.Parameters.AddWithValue("@bookingId", request.BookingId);
                    cmd.Parameters.AddWithValue("@attractionId", attractionId);
                    cmd.Parameters.AddWithValue("@amount", a.Amount);
                    await cmd.ExecuteNonQueryAsync();
                    cmd.Parameters.Clear();
                }

                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
