using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IGeneroServicio
    {
        Task<IEnumerable<Genero>> ObtenerGenerosPeliculas();

        Task<IEnumerable<Genero>> ObtenerGenerosSeries();
    }
}
