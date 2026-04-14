using APBD_2025_kolokwium1D.Models.DTOs;

namespace APBD_2025_kolokwium1D.Services
{
    public interface IDbService
    {
        Task<BookingResponseDto> GetBookingByIdAsync(int bookingId);
        Task AddBookingAsync(CreateBookingRequestDto request);
    }
}