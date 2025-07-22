using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class FavoritoBase
    {
        [Required(ErrorMessage = "El tipo es requerido")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "El tipo debe tener entre 2 y 50 caracteres")]
        public string Tipo { get; set; } = default!;

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, MinimumLength = 2,
            ErrorMessage = "El título debe tener entre 2 y 200 caracteres")]
        public string Titulo { get; set; } = default!;

        [StringLength(1000,
            ErrorMessage = "El comentario no puede exceder 1000 caracteres")]
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "La calificación es requerida")]
        public bool CalificacionUsuario { get; set; }
    }

    public class FavoritoRequest : FavoritoBase
    {
        [Required(ErrorMessage = "El Id de usuario es requerido")]
        public int UsuarioId { get; set; }
    }

    public class FavoritoResponse : FavoritoBase
    {
        public int FavoritoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaFavorito { get; set; }
    }
}

