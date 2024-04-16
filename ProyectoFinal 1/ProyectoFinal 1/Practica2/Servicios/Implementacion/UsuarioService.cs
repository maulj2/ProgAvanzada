using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios.Contrato;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly _dbContext _dbContext;

        public UsuarioService(_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios
                .Where(u => u.Correo == correo && u.Clave == clave)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            if (modelo == null)
            {
                throw new ArgumentNullException(nameof(modelo));
            }

            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }

        // Nuevo método
        public async Task<Usuario> GetUsuarioPorNombre(string nombreUsuario)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios
                .Where(u => u.NombreUsuario == nombreUsuario)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }
    }
}
