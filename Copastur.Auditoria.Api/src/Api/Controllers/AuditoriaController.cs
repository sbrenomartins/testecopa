using Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuditoriaController : ControllerBase
{
    private readonly ILogger<AuditoriaController> _logger;
    private readonly IAuditoriaService _service;

    public AuditoriaController(ILogger<AuditoriaController> logger, IAuditoriaService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _service.Read();
        return Ok(response);
    }
}
