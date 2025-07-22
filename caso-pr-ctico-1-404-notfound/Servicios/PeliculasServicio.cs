using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Peliculas;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class PeliculasServicio : IPeliculaServicio
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguracion _configuracion;

        public PeliculasServicio(IHttpClientFactory httpClientFactory, IConfiguracion configuracion)
        {
            _httpClientFactory = httpClientFactory;
            _configuracion = configuracion;
        }

        public async Task<IEnumerable<Pelicula>> ObtenerPeliculas(string genero, IEnumerable<Genero> listaGeneros, string tipoLista)
        {
            var ListasDisponibles = new List<string> { "now_playing", "popular", "top_rated", "upcoming" };
            if (!ListasDisponibles.Contains(tipoLista))
                {
                    throw new Exception($"La lista '{tipoLista}' no es válida. Intenta con: now_playing, popular, top_rated o upcoming");
                }
            var metodoLista = "";
            switch (tipoLista)
            {
                case "now_playing":
                    metodoLista = "ObtenerPeliculasxGeneroNowPlaying";
                    break;
                case "popular":
                    metodoLista = "ObtenerPeliculasxGeneroPopular";
                    break;
                case "top_rated":
                    metodoLista = "ObtenerPeliculasxGeneroTopRated";
                    break;
                case "upcoming":
                    metodoLista = "ObtenerPeliculasxGeneroUpcoming";
                    break;
            }
            return await ObtenerPeliculasDesdeApi("ApiEndpointsPeliculas", metodoLista, genero, listaGeneros);
        }

        private async Task<IEnumerable<Pelicula>> ObtenerPeliculasDesdeApi(string seccion, string nombreMetodo, string genero, IEnumerable<Genero> listaGeneros)
        {
            var endpointCompleto = _configuracion.ObtenerMetodo(seccion, nombreMetodo);
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");

            var cliente = _httpClientFactory.CreateClient("ServicioPeliculas");
            cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var respuesta = await cliente.GetAsync(endpointCompleto);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dto = JsonSerializer.Deserialize<MovieResponse>(contenido, opciones);

            var generoBuscado = listaGeneros.FirstOrDefault(g => g.Nombre.Equals(genero, StringComparison.OrdinalIgnoreCase));
            if (generoBuscado == null)
            {
                var generoDisponible = listaGeneros.Select(g => g.Nombre).ToList();
                var mensajeError = ($"El género '{genero}' no se encuentra en la lista de géneros disponibles.");
                if (generoDisponible.Any())
                {
                    mensajeError += $"Los géneros disponibles son: {string.Join(", ", generoDisponible)}.";
                }
                else
                {
                    mensajeError += " No hay géneros disponibles.";
                }
                throw new Exception(mensajeError);
            }

            var lista = new List<Pelicula>();

            foreach (var item in dto.results)
            {
                if(item.genre_ids != null && item.genre_ids.Contains(generoBuscado.Id))
                {
                    lista.Add(new Pelicula
                    {
                        Id = item.id,
                        Titulo = item.title,
                        Imagen = item.poster_path,
                        Descripcion = item.overview,
                        Fecha = item.release_date,
                        Calificacion = item.vote_average,
                    });
                }
                    
            }
            
            return lista;
        }
    }
}
