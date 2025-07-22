using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _da;
        public UsuarioFlujo(IUsuarioDA da) => _da = da;

        public Task<IEnumerable<Usuario>> ObtenerTodos() =>
            _da.ObtenerTodos();

        public Task<Usuario?> ObtenerPorId(int id) =>
            _da.ObtenerPorId(id);

        public Task<int> Crear(Usuario u) =>
            _da.Crear(u);

        public Task<bool> Eliminar(int id) =>
            _da.Eliminar(id);
    }
}
