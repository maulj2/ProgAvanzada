﻿using Microsoft.AspNetCore.Mvc;
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


        public async Task<IActionResult> ProductoAct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: ProductoAct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductoAct(int id, [Bind("IdProducto,NombreProducto,Precio,FechaPublicacion,DisponibilidadInventario,VideoJuegoURL,EstadoProducto")] Producto producto)
        {
            if (id != producto.IdProducto)
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
                return RedirectToAction(nameof(ProductoView));
            }
            return View(producto);
        }



        // GET: ProductoDel
        public async Task<IActionResult> ProductoDel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: ProductoDel
        [HttpPost, ActionName("ProductoDel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductoDelConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProductoView));
        }

        private bool ProductoExists(int IdProducto)
        {
            return _context.Productos.Any(e => e.IdProducto == IdProducto);
        }
    }
}