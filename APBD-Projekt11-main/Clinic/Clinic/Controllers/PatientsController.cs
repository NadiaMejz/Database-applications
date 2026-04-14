using Clinic.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IDbService _service;
    public PatientsController(IDbService service) => _service = service;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var patient = await _service.GetPatient(id);
        return patient is null ? NotFound() : Ok(patient);
    }
}