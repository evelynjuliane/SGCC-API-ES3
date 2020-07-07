using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGCC_API.Model;

namespace SGCC_API.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Visitante> Visitantes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Recepcao> Recepcoes { get; set; }
        public DbSet<LogRecepcao> Logs { get; set; }

    }
}
