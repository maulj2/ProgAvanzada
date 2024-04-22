using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.Recursos;
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
        private List<Producto> ObtenerProductosDesdeBaseDeDatos()
        {

            var productos = _context.Productos.ToList(); 
            return productos;
        }
        public IActionResult Index()
        {
            var producto = ObtenerProductosDesdeBaseDeDatos();
            return View(producto);
        }
        public async Task<IActionResult> AgregarAlCarrito(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();

 
                var item = carrito.FirstOrDefault(i => i.Producto.IdProducto == id);
                if (item != null)
                {

                    return PartialView("_Confirmacion");
                }


                carrito.Add(new CarritoItem { Producto = producto, Cantidad = 1 });
                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }


            var productosEnCarrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito")
                                       ?.Select(item => item.Producto)
                                       .ToList();

            if (productosEnCarrito != null)
            {

                return PartialView("_Confirmacion", productosEnCarrito);
            }
            else
            {
 
                return PartialView("_ConfirmacionError");
            }

        }
        private Producto ObtenerProductoPorId(int id)
        {

            return _context.Productos.FirstOrDefault(p => p.IdProducto == id);
        }
        public async Task<IActionResult> _ConfirmarEliminacion(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(); 
            }
            return View(producto); 
        }



        // Acción GET para mostrar la vista de confirmación de eliminación
        [HttpGet]
        public async Task<IActionResult> ConfirmarEliminacionAsync()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // Acción POST para confirmar la eliminación del producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmacionEliminacion(int id)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();

            var item = carrito.FirstOrDefault(i => i.Producto.IdProducto == id);
            if (item != null)
            {
                carrito.Remove(item);
                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
