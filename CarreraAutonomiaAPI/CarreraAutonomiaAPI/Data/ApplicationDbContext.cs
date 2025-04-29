using Microsoft.EntityFrameworkCore;
using CarreraAutonomiaAPI.Models;
using System.Collections.Generic;

namespace CarreraAutonomiaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Registro> Registros { get; set; }
        public DbSet<Configuraciones> Configuraciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
