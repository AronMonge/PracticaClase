using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Peliculas;
using Abstracciones.Modelos.Servicios.Series;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class SeriesServicio: ISerieServicio
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguracion _configuracion;
        public SeriesServicio(IHttpClientFactory httpClientFactory, IConfiguracion configuracion)
        {
            _httpClientFactory = httpClientFactory;
            _configuracion = configuracion;
        }
        public async Task<IEnumerable<TV>> ObtenerSeries(string genero, IEnumerable<Genero> listaGeneros, string tipoLista)
        {
            var ListasDisponibles = new List<string> { "airing_today", "on_the_air", "popular", "top_rated" };
            if (!ListasDisponibles.Contains(tipoLista))
            {
                throw new Exception($"La lista '{tipoLista}' no es válida. Intenta con: airing_today, on_the_air, popular o top_rated");
            }
              var metodoLista = "";
              switch (tipoLista)
            {
                case "airing_today":
                        metodoLista = "ObtenerSeriesxGeneroAiringToday";
                        break;
                    case "on_the_air":
                        metodoLista = "ObtenerSeriesxGeneroOnTheAir";
                        break;
                    case "popular":
                        metodoLista = "ObtenerSeriesxGeneroPopular";
                        break;
                    case "top_rated":
                        metodoLista = "ObtenerSeriesxGeneroTopRated";
                        break;
              }
            return await ObtenerSeriesDesdeApi("ApiEndpointsSeries", metodoLista, genero, listaGeneros);
        }

        private async Task<IEnumerable<TV>> ObtenerSeriesDesdeApi(string seccion, string nombreMetodo, string genero, IEnumerable<Genero> listaGeneros)
        {
            var endpointCompleto = _configuracion.ObtenerMetodo(seccion, nombreMetodo);
            var token = _configuracion.ObtenerValor("TokenServicioPeliculas");

            var cliente = _httpClientFactory.CreateClient("ServicioSeries");
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var respuesta = await cliente.GetAsync(endpointCompleto);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dto = JsonSerializer.Deserialize<SeriesResponse>(contenido, opciones);

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

            var lista = new List<TV>();

            foreach (var item in dto.results)
            {
                if (item.genre_ids != null && item.genre_ids.Contains(generoBuscado.Id)) 
                {
                    lista.Add(new TV
                    {
                        Id = item.id,
                        Titulo = item.name,
                        Imagen = item.poster_path,
                        Descripcion = item.overview,
                        Fecha = item.first_air_date,
                        Calificacion = item.vote_average
                    });
                }
            }
            return lista;
        }
    }
}
