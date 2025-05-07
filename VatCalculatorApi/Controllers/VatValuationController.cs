using Microsoft.AspNetCore.Mvc;
using VatCalculatorApi.Models;
using VatCalculatorApi.Services;

namespace VatCalculatorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VatCalculatorController : ControllerBase
{
    private readonly IVatCalculatorService _service;

    public VatCalculatorController(IVatCalculatorService service)
    {
        _service = service;
    }

    [HttpPost("calculate")]
    public ActionResult<VatCalculationResponse> Calculate([FromBody] VatCalculationRequest request)
    {
        try
        {
            var result = _service.Calculate(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
