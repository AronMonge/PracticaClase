using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Generos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace caso_pr_ctico_1_404_notfoundWEB.Pages.Generos.Peliculas
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<GeneroListado> TituloGeneros { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endPoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPeliculas");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endPoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            TituloGeneros = JsonSerializer.Deserialize<List<GeneroListado>>(resultado, opciones);
        }


    }
}
