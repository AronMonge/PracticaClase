using Abstracciones.Modelos.Servicios.Generos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IListadoGenerosReglas
    {
        Task<IEnumerable<GeneroListado>> ListarGenerosPeliculas();
        Task<IEnumerable<GeneroListado>> ListarGenerosSeries();
    }
}
