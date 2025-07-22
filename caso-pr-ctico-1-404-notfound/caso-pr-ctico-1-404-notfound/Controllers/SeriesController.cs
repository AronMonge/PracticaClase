using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Series;
using Microsoft.AspNetCore.Mvc;

namespace caso_pr_ctico_1_404_notfound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase 
    {
        private readonly IListadoSerieReglas _listadoSerieReglas;

        public SeriesController(IListadoSerieReglas listadoSerieReglas)
        {
            _listadoSerieReglas = listadoSerieReglas;
        }

        [HttpPost]
        [Route("XGenero")]
        public async Task<ActionResult<IEnumerable<SerieListado>>> ObtenerSeriesxGenero([FromBody] TVRequest tvRequest)
        {
            try
            {
                var resultado = await _listadoSerieReglas.ListarSeriesxGenero(tvRequest.genero, tvRequest.tipoLista);
                return Ok(resultado);   
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
