using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGCC_API.Model;

namespace SGCC_API.Repository
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Locador> Locadores { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Locatario> Locatarios { get; set; }
        public DbSet<Visitante> Visitantes { get; set; }
    }
}
