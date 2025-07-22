using System.Text.Json.Serialization;
using Abstracciones.Modelos;
using System;

namespace Abstracciones.Modelos
{
    public class Favorito
    {
        public int FavoritoId { get; set; }    
        public int UsuarioId { get; set; }
        public string Tipo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string? Comentario { get; set; }
        public bool? CalificacionUsuario { get; set; }
        public DateTime FechaFavorito { get; set; }
    }
}





