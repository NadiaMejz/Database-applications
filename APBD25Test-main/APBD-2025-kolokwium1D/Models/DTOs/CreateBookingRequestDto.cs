namespace APBD_2025_kolokwium1D.Models.DTOs
{
    public class CreateBookingRequestDto
    {
        public int BookingId { get; set; }
        public int GuestId { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public List<BookingAttractionInputDto> Attractions { get; set; } 
            = new List<BookingAttractionInputDto>();
    }

    public class BookingAttractionInputDto
    {
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}