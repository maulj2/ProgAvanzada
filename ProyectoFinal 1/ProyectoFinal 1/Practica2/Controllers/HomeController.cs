using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios.Contrato;
using System.Security.Claims;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUsuarioService _usuarioServicio;

    public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioServicio)
    {
        _logger = logger;
        _usuarioServicio = usuarioServicio;
    }

    public async Task<IActionResult> Index()
    {
        ClaimsPrincipal claimuser = HttpContext.User;

        if (claimuser.Identity.IsAuthenticated)
        {
            // Obtener el nombre del usuario directamente
            string nombreUsuario = User.Identity.Name;

            ViewData["nombreUsuario"] = nombreUsuario;

            // Obtener el usuario autenticado y pasarlo a la vista
            Usuario usuario = await _usuarioServicio.GetUsuarioPorNombre(nombreUsuario);

            // Retornar la vista con el modelo de Usuario
            return View(usuario);
        }

        // Redirigir a la página de inicio de sesión si no está autenticado
        return RedirectToAction("IniciarSesion", "Inicio");
    }


}
