using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Generos;
using Microsoft.AspNetCore.Mvc;

namespace Peliculas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly IListadoGenerosReglas _listadoGenerosReglas;

        public GenerosController(IListadoGenerosReglas listadoGenerosReglas)
        {
            _listadoGenerosReglas = listadoGenerosReglas;
        }

        [HttpGet]
        [Route("peliculas")]
        public async Task<ActionResult<IEnumerable<GeneroListado>>> ObtenerGenerosPeliculas()
        {
            var resultado = await _listadoGenerosReglas.ListarGenerosPeliculas();
            return Ok(resultado);
        }

        [HttpGet]
        [Route("series")]
        public async Task<ActionResult<IEnumerable<GeneroListado>>> ObtenerGenerosSeries()
        {
            var resultado = await _listadoGenerosReglas.ListarGenerosSeries();
            return Ok(resultado);
        }
    }
}
