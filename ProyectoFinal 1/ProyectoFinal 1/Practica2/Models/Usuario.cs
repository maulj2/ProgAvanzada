using System;
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public byte[]? ImagenPerfil { get; set; } // Nueva propiedad para la imagen de perfil
    }
}
