using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ProductoController : Controller
    {
        private readonly _dbContext _context;

        public ProductoController(_dbContext context)
        {
            _context = context;
        }
        // Crear
        public IActionResult ProductoCrear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductoCrear([Bind("IdProducto,NombreProducto,Precio,FechaPublicacion,DisponibilidadInventario,VideoJuegoURL,EstadoProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProductoView));
            }
            return View(producto);
        }

        // Leer
        public async Task<IActionResult> ProductoView()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // Actualizar
        public async Task<IActionResult> ProductoAct(int? IdProducto)
        {
            if (IdProducto == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(IdProducto);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductoAct(int IdProducto, [Bind("IdProducto,NombreProducto,Precio,FechaPublicacion,DisponibilidadInventario,VideoJuegoURL,EstadoProducto")] Producto producto)
        {
            if (IdProducto != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // Eliminar
        public async Task<IActionResult> ProductoDel(int? IdProducto)
        {
            if (IdProducto == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == IdProducto);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost, ActionName("ProductoDel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdProducto)
        {
            var producto = await _context.Productos.FindAsync(IdProducto);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProductoExists(int IdProducto)
        {
            return _context.Productos.Any(e => e.IdProducto == IdProducto);
        }

    }
}