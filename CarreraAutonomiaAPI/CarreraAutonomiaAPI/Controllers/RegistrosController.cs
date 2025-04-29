using Microsoft.AspNetCore.Mvc;
using CarreraAutonomiaAPI.Models;
using CarreraAutonomiaAPI.Services;

namespace CarreraAutonomiaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController : ControllerBase
    {
        private readonly IRegistroService _service;

        public RegistrosController(IRegistroService service)
        {
            _service = service;
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var total = await _service.ContarRegistros();
            return Ok(total);
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> Disponibles()
        {
            int total = await _service.ContarRegistros();
            int limite = await _service.ObtenerLimiteCamisas();
            return Ok(limite - total);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] Registro registro)
        {
            try
            {
                var mensaje = await _service.Registrar(registro);
                if (mensaje != "Registro exitoso")
                    return BadRequest(mensaje);

                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("admin/registros")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var registros = await _service.ObtenerTodos();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
