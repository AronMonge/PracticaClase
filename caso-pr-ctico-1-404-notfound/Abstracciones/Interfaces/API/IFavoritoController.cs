using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.API
{
    public interface IFavoritoController
    {
        Task<ActionResult<IEnumerable<Favorito>>> GetAll(int usuarioId);

        Task<ActionResult<Favorito>> GetById(int usuarioId, int favoritoId);

        Task<ActionResult<Favorito>> Create(int usuarioId, Favorito favorito);

        Task<ActionResult<Favorito>> Update(int usuarioId, int favoritoId, Favorito favorito);

        Task<IActionResult> Delete(int usuarioId, int favoritoId);
    }
}
