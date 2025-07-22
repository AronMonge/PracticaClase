using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Generos;
using Abstracciones.Modelos.Servicios.Series;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace caso_pr_ctico_1_404_notfoundWEB.Pages.Generos.Series
{
    public class SeriesXGeneroModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private readonly HttpClient client;

        public string? Genero { get; set; }
        public SeriesRequest seriesR { get; set; } = default!;
        public IList<SeriesResponse> airing_today { get; set; } = default!;
        public IList<SeriesResponse> on_the_air { get; set; } = default!;
        public IList<SeriesResponse> popular { get; set; } = default!;
        public IList<SeriesResponse> top_rated { get; set; } = default!;

        public SeriesXGeneroModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
            client = new HttpClient();
        }

        public async Task OnGet(string? genero)
        {
            if (string.IsNullOrEmpty(genero)) return;

            Genero = genero;

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSeriesXGenero");

            var airingTask = TipoLista(endpoint, genero, "airing_today");
            var onAirTask = TipoLista(endpoint, genero, "on_the_air");
            var popularTask = TipoLista(endpoint, genero, "popular");
            var topRatedTask = TipoLista(endpoint, genero, "top_rated");

            await Task.WhenAll(airingTask, onAirTask, popularTask, topRatedTask);

            airing_today = await airingTask;
            on_the_air = await onAirTask;
            popular = await popularTask;
            top_rated = await topRatedTask;
        }

        public async Task<IList<SeriesResponse>> TipoLista(string endpoint, string genero, string lista)
        {
            try
            {
                seriesR = new SeriesRequest
                {
                    genero = genero,
                    tipoLista = lista
                };

                var respuesta = await client.PostAsJsonAsync(endpoint, seriesR);
                respuesta.EnsureSuccessStatusCode();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<List<SeriesResponse>>(resultado, opciones);

            }
            catch (Exception ex)
            {
                return new List<SeriesResponse>();
            }
        }
    }
}
