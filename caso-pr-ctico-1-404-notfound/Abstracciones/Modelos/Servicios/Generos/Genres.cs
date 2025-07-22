using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Peliculas
{
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class GenreResponse
    {
        public List<Genres> Genres { get; set; } = new();
    }

}
