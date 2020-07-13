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
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Predio> Predios { get; set; }
        public DbSet<Conta> Contas { get; set; }
    }
}
