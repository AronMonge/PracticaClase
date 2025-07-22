using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<ActionResult<IEnumerable<Usuario>>> GetAll();
        Task<ActionResult<Usuario>> GetById(int id);
        Task<ActionResult<Usuario>> Create(Usuario u);
        Task<IActionResult> Delete(int id);
    }
}
