using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Series
{
    public class TV
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public double Calificacion { get; set; }
    }

    public class TVRequest
    {
        [Required(ErrorMessage = "El genero es requerido")]
        public string genero { get; set; }
        [Required(ErrorMessage = "El tipo de lista es requerido")]
        public string tipoLista { get; set; }
    }
}
