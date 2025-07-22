using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Peliculas
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public double Calificacion { get; set; }
    }

    public class PeliculasRequest
    {
        [Required(ErrorMessage = "La propiedad genero es requerida")]
        public string genero { get; set; }
        [Required(ErrorMessage = "La propiedad tipoLista es requerida")]
        public string tipoLista { get; set; }
    }
}
