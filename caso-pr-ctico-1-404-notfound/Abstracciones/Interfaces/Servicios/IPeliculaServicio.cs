using Abstracciones.Modelos.Servicios.Peliculas;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IPeliculaServicio
    {
        Task<IEnumerable<Pelicula>> ObtenerPeliculas(string genero, IEnumerable<Genero> listaGeneros, string tipoLista);

    }
}
