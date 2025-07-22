using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ListaVisualizacionFlujo : IListaVisualizacionFlujo
    {
        private readonly IListaVisualizacionDA _da;
        public ListaVisualizacionFlujo(IListaVisualizacionDA da)
            => _da = da;

        public Task<IEnumerable<ListaVisualizacion>> ObtenerPorUsuario(int usuarioId)
            => _da.ObtenerPorUsuario(usuarioId);

        public Task<ListaVisualizacion?> ObtenerDetalle(int usuarioId, int listaId)
            => _da.ObtenerDetalle(usuarioId, listaId);

        public Task<int> Crear(ListaVisualizacion lista)
            => _da.Crear(lista);

        public Task<bool> Editar(int usuarioId, int listaId, ListaVisualizacion lista)
            => _da.Editar(usuarioId, listaId, lista);

        public Task<bool> Eliminar(int usuarioId, int listaId)
            => _da.Eliminar(usuarioId, listaId);
    }
}

