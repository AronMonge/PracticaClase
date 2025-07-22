using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private const int _usuarioId = 1;

        public IList<FavoritoResponse> favoritos { get; set; } = new List<FavoritoResponse>();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string plantilla = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavoritos");
            string url = string.Format(plantilla, _usuarioId);

            using var cliente = new HttpClient();  
            var solicitud = new HttpRequestMessage(HttpMethod.Get, url);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            favoritos = JsonSerializer
                                   .Deserialize<List<FavoritoResponse>>(json, opciones)
                               ?? new List<FavoritoResponse>();
        }
    }
}


