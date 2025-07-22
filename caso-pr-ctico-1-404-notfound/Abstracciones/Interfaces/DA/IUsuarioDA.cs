using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<IEnumerable<Usuario>> ObtenerTodos();
        Task<Usuario?> ObtenerPorId(int id);
        Task<int> Crear(Usuario usuario);
        Task<bool> Eliminar(int id);
    }
}
