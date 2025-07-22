using Abstracciones.Modelos.Servicios.Series;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IListadoSerieReglas
    {
        Task<IEnumerable<SerieListado>> ListarSeriesxGenero(string genero, string tipoLista);
    }
}
