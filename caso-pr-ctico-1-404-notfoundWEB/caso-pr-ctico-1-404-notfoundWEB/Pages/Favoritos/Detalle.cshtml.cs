using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private const int _usuarioId = 1;

        public FavoritoResponse Favorito { get; set; } = default!;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        public async Task OnGet(int id)
        {
            string plantilla = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavorito");
            string url = string.Format(plantilla, _usuarioId, id);

            using var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, url);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var json = await respuesta.Content.ReadAsStringAsync();
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Favorito = JsonSerializer.Deserialize<FavoritoResponse>(json, opts)
                       ?? throw new Exception("No se obtuvo el favorito");
        }
    }
}






