using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private const int _usuarioId = 1;  

        [BindProperty]
        public FavoritoResponse Favorito { get; set; } = default!;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(int? id)
        {
            if (id == null || id.Value == 0)
                return NotFound();

            string plantilla = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavorito");
            string url = string.Format(plantilla, _usuarioId, id.Value);

            using var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, url);
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Favorito = JsonSerializer.Deserialize<FavoritoResponse>(json, opts)
                       ?? throw new Exception("No se obtuvo el favorito");

            return Page();
        }

        public async Task<ActionResult> OnPost(int id)
        {
            if (id == 0)
                return NotFound();

            string plantilla = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarFavorito");
            string url = string.Format(plantilla, _usuarioId, id);

            using var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, url);
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}








