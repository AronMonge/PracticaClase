using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize(Policy = "UserOrAdmin")]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IUsuarioFlujo _flujo;
        public UsuarioController(IUsuarioFlujo flujo) => _flujo = flujo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll() =>
            Ok(await _flujo.ObtenerTodos());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var u = await _flujo.ObtenerPorId(id);
            return u is null ? NotFound() : Ok(u);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario u)
        {
            if (string.IsNullOrWhiteSpace(u.Rol))
                u.Rol = "user";

            var newId = await _flujo.Crear(u);
            u.UsuarioId = newId;
            return CreatedAtAction(nameof(GetById), new { id = newId }, u);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _flujo.Eliminar(id);
            return ok ? NoContent() : NotFound();
        }
    }
}





