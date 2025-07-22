using System;
using System.Text.Json.Serialization;

namespace Abstracciones.Modelos
{
    public class ListaVisualizacion
    {
        public int ListaId { get; set; }
        public int UsuarioId { get; set; }


        public string Tipo { get; set; } = null!;


        public string Titulo { get; set; } = null!;


        public string? Detalle { get; set; }

        public DateTime FechaAgregado { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}

