using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.API
{
    public interface IListaVisualizacionController
    {
        Task<ActionResult<IEnumerable<ListaVisualizacion>>> GetAll(int usuarioId);
        Task<ActionResult<ListaVisualizacion>> GetById(int usuarioId, int listaId);
        Task<ActionResult<ListaVisualizacion>> Create(int usuarioId, ListaVisualizacion lista);
        Task<IActionResult> Update(int usuarioId, int listaId, ListaVisualizacion lista);
        Task<IActionResult> Delete(int usuarioId, int listaId);
    }
}
