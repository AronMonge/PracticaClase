using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuarios/{usuarioId:int}/listas")]
    [Authorize(Policy = "UserOrAdmin")]
    public class ListaVisualizacionController : ControllerBase, IListaVisualizacionController
    {
        private readonly IListaVisualizacionFlujo _flujo;
        public ListaVisualizacionController(IListaVisualizacionFlujo flujo)
            => _flujo = flujo;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaVisualizacion>>> GetAll(int usuarioId)
        {
            var listas = await _flujo.ObtenerPorUsuario(usuarioId);
            return Ok(listas);
        }


        [HttpGet("{listaId:int}")]
        public async Task<ActionResult<ListaVisualizacion>> GetById(int usuarioId, int listaId)
        {
            var lista = await _flujo.ObtenerDetalle(usuarioId, listaId);
            return lista is null ? NotFound() : Ok(lista);
        }


        [HttpPost]
        public async Task<ActionResult<ListaVisualizacion>> Create(int usuarioId, [FromBody] ListaVisualizacion lista)
        {
            lista.UsuarioId = usuarioId;
            lista.FechaAgregado = DateTime.Now;

            var newId = await _flujo.Crear(lista);
            lista.ListaId = newId;

            return CreatedAtAction(
                nameof(GetById),
                new { usuarioId, listaId = newId },
                lista
            );
        }


        [HttpPut("{listaId:int}")]
        public async Task<IActionResult> Update(int usuarioId, int listaId, [FromBody] ListaVisualizacion lista)
        {
            lista.UsuarioId = usuarioId;
            lista.ListaId = listaId;

            lista.FechaAgregado = lista.FechaAgregado == default
                                    ? DateTime.Now
                                    : lista.FechaAgregado;

            var updated = await _flujo.Editar(usuarioId, listaId, lista);
            return updated ? Ok(lista) : NotFound();
        }


        [HttpDelete("{listaId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int usuarioId, int listaId)
        {
            var deleted = await _flujo.Eliminar(usuarioId, listaId);
            return deleted ? NoContent() : NotFound();
        }
    }
}

