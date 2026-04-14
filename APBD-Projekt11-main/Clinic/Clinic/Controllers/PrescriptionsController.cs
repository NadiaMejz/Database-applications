using Clinic.DTOs;
using Clinic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _service;
    public PrescriptionsController(IDbService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Add(AddPrescriptionDto dto)
    {
        try
        {
            var id = await _service.AddPrescription(dto);
            return Created($"/api/prescriptions/{id}", null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}