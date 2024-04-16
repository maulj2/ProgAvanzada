using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public partial class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string? DisponibilidadInventario { get; set; }

        public string? VideoJuegoURL { get; set; }
        public string? EstadoProducto { get; set; }
    }
    public class ProductoDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=Ari_s\\SQLEXPRESS;database=TIENDAVIDEOJUEGOS;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public ProductoDBContext() { }
        public DbSet<Producto> Productos { get; set; }
    }
}