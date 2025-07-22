using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Series
{
    public class SeriesBase
    {
        public string? Titulo { get; set; }
        public string? Imagen { get; set; }
        public string? Descripcion { get; set; }
        public string? Fecha { get; set; }
        public double? Calificacion { get; set; }
    }

    public class SeriesRequest: SeriesBase
    {
        public string? genero { get; set; }
        public string? tipoLista { get; set; }
    }

    public class SeriesResponse: SeriesBase
    {
        public string? Titulo { get; set; }
        public string? Imagen { get; set; }
        public string? Descripcion { get; set; }
        public string? Fecha { get; set; }
        public double? Calificacion { get; set; }
    }
}
