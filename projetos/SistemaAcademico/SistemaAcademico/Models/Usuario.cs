namespace SistemaAcademico.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; } // Nullable = ? é para criar tipo 
        public string? Email { get; set; } // Nullable = ? 
        public string? Senhna { get; set; } // Nullable = ? 
    }
}
