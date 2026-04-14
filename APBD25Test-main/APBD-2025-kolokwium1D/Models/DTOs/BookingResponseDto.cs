namespace APBD_2025_kolokwium1D.Models.DTOs
{
    public class BookingResponseDto
    {
        public DateTime Date { get; set; }
        public GuestDto Guest { get; set; } = new GuestDto();
        public EmployeeDto Employee { get; set; } = new EmployeeDto();
        public List<BookingAttractionDto> Attractions { get; set; } 
            = new List<BookingAttractionDto>();
    }

    public class GuestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }

    public class EmployeeDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
    }

    public class BookingAttractionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}