using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Models;

namespace SistemaAcademico.Data
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
    }
}
