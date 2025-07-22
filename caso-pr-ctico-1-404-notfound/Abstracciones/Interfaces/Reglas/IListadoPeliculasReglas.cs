using Abstracciones.Modelos.Servicios.Peliculas;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IListadoPeliculasReglas
    {
        Task<IEnumerable<PeliculaListado>> ListarPeliculasxGenero(string genero, string tipoLista);
    }
}
