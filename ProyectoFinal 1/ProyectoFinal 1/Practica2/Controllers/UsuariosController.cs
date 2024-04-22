using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly _dbContext _context;

        public UsuariosController(_dbContext context)
        {
            _context = context;
        }

        // Crear
        public IActionResult UsuarioCrear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsuarioCrear([Bind("IDUsuario,NombreUsuario,Clave,Correo,EstadoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UsuarioView));
            }
            return View(usuario);
        }

        // Leer
        public async Task<IActionResult> UsuarioView()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // Editar
        public async Task<IActionResult> UsuarioEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsuarioEdit(int id, [Bind("IDUsuario,NombreUsuario,Clave,Correo,EstadoUsuario")] Usuario usuario)
        {
            if (id != usuario.IDUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(UsuarioView));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IDUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(usuario);
        }


        // Eliminar
        public async Task<IActionResult> UsuarioDel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("UsuarioDel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsuarioDelConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UsuarioView));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IDUsuario == id);
        }
    }
}
