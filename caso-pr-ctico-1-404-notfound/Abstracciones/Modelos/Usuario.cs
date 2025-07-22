using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Rol { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contrasena { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Favorito>? Favoritos { get; set; }
        public ICollection<ListaVisualizacion>? ListaVisualizaciones { get; set;}
    }
}
