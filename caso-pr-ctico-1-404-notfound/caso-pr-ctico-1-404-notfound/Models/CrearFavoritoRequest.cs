namespace API.Models
{
    public class CrearFavoritoRequest
    {
        public int IdApi { get; set; }
        public string Comentario { get; set; } = null!;
        public bool CalificacionUsuario { get; set; }
    }
}

