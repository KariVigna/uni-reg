using Microsoft.EntityFrameworkCore;

namespace Registrar.Models
{
    public class RegistrarContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public RegistrarContext(DbContextOptions options) : base(options) { }
    }
}