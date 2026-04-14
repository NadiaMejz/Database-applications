using Microsoft.AspNetCore.Mvc;
using Tutorial12.DTOs;
using Tutorial12.Exceptions;
using Tutorial12.Services;

namespace Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly IDbService _service;

    public TripsController(IDbService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips(int page = 1, int pageSize = 10)
    {
        var result = await _service.GetTrips(page, pageSize);
        return Ok(result);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClient(int idTrip, CreateClientDto dto)
    {
        try
        {
            await _service.AssignClientToTrip(idTrip, dto);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }
}