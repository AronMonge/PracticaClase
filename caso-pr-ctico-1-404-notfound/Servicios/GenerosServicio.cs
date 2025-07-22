using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Peliculas;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class GenerosServicio : IGeneroServicio
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguracion _configuracion;

        public GenerosServicio(IHttpClientFactory httpClientFactory, IConfiguracion configuracion)
        {
            _httpClientFactory = httpClientFactory;
            _configuracion = configuracion;
        }

        public async Task<IEnumerable<Genero>> ObtenerGenerosPeliculas()
        {
            return await ObtenerGenerosDesdeApi("ApiEndpointsPeliculas", "ObtenerGenerosPeliculas");
        }

        public async Task<IEnumerable<Genero>> ObtenerGenerosSeries()
        {
            return await ObtenerGenerosDesdeApi("ApiEndpointsPeliculas", "ObtenerGenerosSeries");
        }

        private async Task<IEnumerable<Genero>> ObtenerGenerosDesdeApi(string seccion, string nombreMetodo)
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
            var dto = JsonSerializer.Deserialize<GenreResponse>(contenido, opciones);

            var lista = new List<Genero>();
            foreach (var item in dto.Genres)
            {
                lista.Add(new Genero
                {
                    Id = item.Id,
                    Nombre = item.Name
                });
            }

            return lista;
        }
    }
}
