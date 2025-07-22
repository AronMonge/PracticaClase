using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class ListadoSeriesReglas: IListadoSerieReglas
    {
        private readonly ISerieServicio _serieServicio;
        private readonly IGeneroServicio _generoServicio;
        public ListadoSeriesReglas(ISerieServicio seriesServicio, IGeneroServicio generoServicio)
        {
            _serieServicio = seriesServicio;
            _generoServicio = generoServicio;
        }
       public async Task<IEnumerable<SerieListado>> ListarSeriesxGenero(string genero, string tipoLista) 
       {
            var listaGeneros = await _generoServicio.ObtenerGenerosSeries();
            var lista =await _serieServicio.ObtenerSeries(genero, listaGeneros, tipoLista);
            return lista.Select(x => new SerieListado
            {
                Titulo = x.Titulo,
                Imagen = x.Imagen,
                Descripcion = x.Descripcion,
                Fecha = x.Fecha,
                Calificacion = x.Calificacion
            });
        }
    }
}
