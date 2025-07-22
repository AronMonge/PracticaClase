using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios.Generos;
using Abstracciones.Modelos.Servicios.Peliculas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace caso_pr_ctico_1_404_notfoundWEB.Pages.Peliculas
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        [BindProperty]
        public List<SelectListItem> GenerosPeliculas { get; set; } = default!;
        [BindProperty]
        public List<Pelicula> PeliculasResultado { get; set; } 

        [BindProperty]
        public PeliculasRequest pelicularequest { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPeliculas");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultadodesearilizado = JsonSerializer.Deserialize<List<GeneroListado>>(resultado, opciones);
                GenerosPeliculas = resultadodesearilizado.Select(m => new SelectListItem
                {
                    Text = m.Titulo.ToString(),
                }
                ).ToList();
            }
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerPeliculasXGenero");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var respuesta = await cliente.PostAsJsonAsync(endpoint, pelicularequest);
            if (respuesta.StatusCode != HttpStatusCode.OK)
            {
                var mensajeError = await respuesta.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, mensajeError);
                await OnGet();
                return Page();
            }
            else
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                PeliculasResultado = JsonSerializer.Deserialize<List<Pelicula>>(resultado, opciones);
            }
            respuesta.EnsureSuccessStatusCode();
            await OnGet();
            return Page();
        }
    }
}

