using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Peliculas;

namespace Reglas
{
    public class ListadoPeliculasReglas : IListadoPeliculasReglas
    {
        private readonly IPeliculaServicio _peliculaServicio;
        private readonly IGeneroServicio _generoServicio;

        public ListadoPeliculasReglas(IPeliculaServicio peliculaServicio, IGeneroServicio generoServicio)
        {
            _peliculaServicio = peliculaServicio;
            _generoServicio = generoServicio;
        }

        public async Task<IEnumerable<PeliculaListado>> ListarPeliculasxGenero(string genero, string tipoLista)
        {
            var listaGeneros = await _generoServicio.ObtenerGenerosPeliculas();
            var lista = await _peliculaServicio.ObtenerPeliculas(genero, listaGeneros, tipoLista);
            return lista.Select(x => new PeliculaListado
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
