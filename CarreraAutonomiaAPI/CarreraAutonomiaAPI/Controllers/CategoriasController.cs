using Microsoft.AspNetCore.Mvc;
using CarreraAutonomiaAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categorias = await _service.ObtenerCategoriasActivas();
        return Ok(categorias);
    }
}
