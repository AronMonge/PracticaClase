using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<IEnumerable<Usuario>> ObtenerTodos();
        Task<Usuario?> ObtenerPorId(int id);
        Task<int> Crear(Usuario usuario);
        Task<bool> Eliminar(int id);
    }
}
