using Abstracciones.Modelos.Servicios.Peliculas;
using Abstracciones.Modelos.Servicios.Series;

namespace Abstracciones.Interfaces.Servicios
{
    public interface ISerieServicio
    {
        Task<IEnumerable<TV>> ObtenerSeries(string genero, IEnumerable<Genero> listaGeneros, string tipoLista);
    }
}
