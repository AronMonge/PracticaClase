using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Peliculas;
using Microsoft.AspNetCore.Mvc;

namespace Peliculas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IListadoPeliculasReglas _listadoPeliculasReglas;

        public PeliculasController(IListadoPeliculasReglas listadoPeliculasReglas)
        {
            _listadoPeliculasReglas = listadoPeliculasReglas;
        }

        [HttpPost]
        [Route("XGenero")]
        public async Task<ActionResult<IEnumerable<PeliculaListado>>> ObtenerPeliculasxGenero([FromBody] PeliculasRequest peliculasRequest)
        {
            try
            {
                var resultado = await _listadoPeliculasReglas.ListarPeliculasxGenero(peliculasRequest.genero, peliculasRequest.tipoLista);
                return Ok(resultado);   
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
