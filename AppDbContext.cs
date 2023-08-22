using Microsoft.EntityFrameworkCore;
using Shop_API.Models;

namespace Shop_API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
    }
}
