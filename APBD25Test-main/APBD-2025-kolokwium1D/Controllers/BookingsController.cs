using APBD_2025_kolokwium1D.Exceptions;
using APBD_2025_kolokwium1D.Models.DTOs;
using APBD_2025_kolokwium1D.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_2025_kolokwium1D.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public BookingsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            try
            {
                var booking = await _dbService.GetBookingByIdAsync(id);
                return Ok(booking);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingRequestDto createBookingRequest)
        {
            if (createBookingRequest.Attractions == null || !createBookingRequest.Attractions.Any())
            {
                return BadRequest("At least one attraction is required.");
            }

            try
            {
                await _dbService.AddBookingAsync(createBookingRequest);
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return CreatedAtAction(
                nameof(GetBooking),
                new { id = createBookingRequest.BookingId },
                createBookingRequest
            );
        }
    }
}