using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Favoritos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        private const int _usuarioId = 1;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public FavoritoRequest favorito { get; set; } = new FavoritoRequest();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            favorito.UsuarioId = _usuarioId;

            string plantilla = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarFavorito");
            string url = string.Format(plantilla, _usuarioId);

            using var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(url, favorito);
            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}




