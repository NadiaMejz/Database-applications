using Microsoft.AspNetCore.Mvc;
using Tutorial12.Exceptions;
using Tutorial12.Services;

namespace Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IDbService _service;

    public ClientsController(IDbService service)
    {
        _service = service;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> Delete(int idClient)
    {
        try
        {
            await _service.DeleteClient(idClient);
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