using System;
using System.Collections.Generic;
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
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private const int _usuarioId = 1;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public FavoritoResponse Favorito { get; set; } = default!;

        public async Task<IActionResult> OnGet(int id)
        {
            string plantilla = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerFavorito");
            string url = string.Format(plantilla, _usuarioId, id);

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

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var dto = new FavoritoRequest
            {
                Tipo = Favorito.Tipo,
                Titulo = Favorito.Titulo,
                Comentario = Favorito.Comentario,
                CalificacionUsuario = Favorito.CalificacionUsuario,
                UsuarioId = _usuarioId
            };

            string epEditar = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarFavorito");
            string url = string.Format(epEditar, _usuarioId, Favorito.FavoritoId);

            using var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync(url, dto);

            if (respuesta.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}







