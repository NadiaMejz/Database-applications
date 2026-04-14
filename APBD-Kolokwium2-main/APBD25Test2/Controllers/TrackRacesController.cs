using APBD25Test2.DTOs;
using APBD25Test2.Exceptions;
using APBD25Test2.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD25Test2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackRacesController : ControllerBase
{
    private readonly IDbService _dbService;

    public TrackRacesController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> AddParticipants([FromBody] RaceParticipantCreateDto dto)
    {
        try
        {
            await _dbService.AddParticipationsAsync(dto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
}