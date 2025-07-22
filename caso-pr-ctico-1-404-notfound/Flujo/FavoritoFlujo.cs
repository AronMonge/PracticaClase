using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class FavoritoFlujo : IFavoritoFlujo
    {
        private readonly IFavoritoDA _da;
        public FavoritoFlujo(IFavoritoDA da) => _da = da;

        public Task<IEnumerable<Favorito>> ObtenerPorUsuario(int usuarioId) =>
            _da.ObtenerPorUsuario(usuarioId);

        public Task<Favorito> ObtenerPorId(int usuarioId, int favoritoId) =>
            _da.ObtenerPorId(usuarioId, favoritoId);

        public Task<int> Crear(Favorito favorito) =>
            _da.Crear(favorito);

        public Task<bool> Actualizar(int usuarioId, int favoritoId, Favorito favorito) =>
            _da.Actualizar(usuarioId, favoritoId, favorito);

        public Task<bool> Eliminar(int usuarioId, int favoritoId) =>
            _da.Eliminar(usuarioId, favoritoId);
    }
}
