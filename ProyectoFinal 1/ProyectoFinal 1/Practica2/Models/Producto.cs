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
}