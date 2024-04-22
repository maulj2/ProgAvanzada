using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Models
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }

}
