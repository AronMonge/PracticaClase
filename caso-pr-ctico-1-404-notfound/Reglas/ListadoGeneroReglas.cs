using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Generos;

namespace Reglas
{
    public class ListadoGenerosReglas : IListadoGenerosReglas
    {
        private readonly IGeneroServicio _generoServicio;

        public ListadoGenerosReglas(IGeneroServicio generoServicio)
        {
            _generoServicio = generoServicio;
        }

        public async Task<IEnumerable<GeneroListado>> ListarGenerosPeliculas()
        {
            var lista = await _generoServicio.ObtenerGenerosPeliculas();
            return lista.Select(x => new GeneroListado
            {
                Titulo = x.Nombre
            });
        }

        public async Task<IEnumerable<GeneroListado>> ListarGenerosSeries()
        {
            var lista = await _generoServicio.ObtenerGenerosSeries();
            return lista.Select(x => new GeneroListado
            {
                Titulo = x.Nombre
            });
        }
    }
}
