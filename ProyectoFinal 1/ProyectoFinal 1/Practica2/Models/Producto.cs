using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public partial class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required]
        public string NombreProducto { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public int DisponibilidadInventario { get; set; }

        public string VideoJuegoURL { get; set; }

        [Required]
        public string EstadoProducto { get; set; }
    }
}