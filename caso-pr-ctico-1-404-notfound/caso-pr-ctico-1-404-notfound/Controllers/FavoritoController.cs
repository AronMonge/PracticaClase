using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuarios/{usuarioId:int}/favoritos")]
    [AllowAnonymous]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoFlujo _favoritoFlujo;

        public FavoritoController(IFavoritoFlujo favoritoFlujo)
            => _favoritoFlujo = favoritoFlujo;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorito>>> GetAll(int usuarioId)
        {
            var lista = await _favoritoFlujo.ObtenerPorUsuario(usuarioId);
            return Ok(lista);
        }


        [HttpGet("{favoritoId:int}")]
        public async Task<ActionResult<Favorito>> GetById(int usuarioId, int favoritoId)
        {
            var fav = await _favoritoFlujo.ObtenerPorId(usuarioId, favoritoId);
            return fav is null ? NotFound() : Ok(fav);
        }


        [HttpPost]
        public async Task<ActionResult<Favorito>> Create(int usuarioId, [FromBody] Favorito favorito)
        {
            favorito.UsuarioId = usuarioId;
            favorito.FechaFavorito = DateTime.Now;

            var newId = await _favoritoFlujo.Crear(favorito);
            favorito.FavoritoId = newId;

            return CreatedAtAction(
                nameof(GetById),
                new { usuarioId, favoritoId = newId },
                favorito
            );
        }


        [HttpPut("{favoritoId:int}")]
        public async Task<ActionResult<Favorito>> Update(
            int usuarioId,
            int favoritoId,
            [FromBody] Favorito favorito)
        {
            favorito.UsuarioId = usuarioId;
            favorito.FavoritoId = favoritoId;


            favorito.FechaFavorito = favorito.FechaFavorito == default
                                       ? DateTime.Now
                                       : favorito.FechaFavorito;

            var updated = await _favoritoFlujo.Actualizar(usuarioId, favoritoId, favorito);
            return updated ? Ok(favorito) : NotFound();
        }


        [HttpDelete("{favoritoId:int}")]
        public async Task<IActionResult> Delete(int usuarioId, int favoritoId)
        {
            var deleted = await _favoritoFlujo.Eliminar(usuarioId, favoritoId);
            return deleted ? NoContent() : NotFound();
        }
    }
}





