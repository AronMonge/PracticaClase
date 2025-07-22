using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IFavoritoFlujo
    {
        Task<IEnumerable<Favorito>> ObtenerPorUsuario(int usuarioId);
        Task<Favorito> ObtenerPorId(int usuarioId, int favoritoId);
        Task<int> Crear(Favorito favorito);
        Task<bool> Actualizar(int usuarioId, int favoritoId, Favorito favorito);
        Task<bool> Eliminar(int usuarioId, int favoritoId);
    }
}

