using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IListaVisualizacionDA
    {
        Task<IEnumerable<ListaVisualizacion>> ObtenerPorUsuario(int usuarioId);
        Task<ListaVisualizacion?> ObtenerDetalle(int usuarioId, int listaId);
        Task<int> Crear(ListaVisualizacion lista);
        Task<bool> Editar(int usuarioId, int listaId, ListaVisualizacion lista);
        Task<bool> Eliminar(int usuarioId, int listaId);
    }
}

