using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        [Display(Name = "Ra")] // O nome que vai aparecer é o RA
        [Required(ErrorMessage = "O RA é obrigatório")] // RA é requirido, obrigatório
        [StringLength(10, MinimumLength = 4, ErrorMessage = "O RA deve ter entre 4 e 10 caracteres")]
        
        public string? Ra { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario  { get; set; } // Guarda os objetos da classe usuário

    }
}
