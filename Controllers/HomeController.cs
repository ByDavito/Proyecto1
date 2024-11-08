using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Microsoft.AspNetCore.Authorization;
using Proyecto1.Data;
using Microsoft.AspNetCore.Identity;

namespace Proyecto1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private ApplicationDbContext _context;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _rolManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _rolManager = rolManager;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Index()
    {
        await InicializarPermisosUsuario();
        return View();
    }


    public IActionResult Ejercicios()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<JsonResult> InicializarPermisosUsuario()
    {
        // CREAR ROLES SI NO EXISTEN
        // Se verifica si el rol "ADMINISTRADOR" ya existe en la base de datos.
        var nombreRolCrearExiste = _context.Roles.Where(r => r.Name == "ADMINISTRADOR").SingleOrDefault();

        // Si el rol "ADMINISTRADOR" no existe, se crea un nuevo rol con ese nombre.
        if (nombreRolCrearExiste == null)
        {
            var roleResult = await _rolManager.CreateAsync(new IdentityRole("ADMINISTRADOR"));
        }

        // CREAR USUARIO PRINCIPAL
        // Esta variable indica si el usuario fue creado exitosamente.
        bool creado = false;

        // BUSCAR POR MEDIO DE CORREO ELECTRONICO SI EXISTE EL USUARIO
        // Se busca en la base de datos si ya existe un usuario con el correo "admin@sistema.com".
        var usuario = _context.Users.Where(u => u.Email == "admin@admin.com").SingleOrDefault();

        // Si no existe un usuario con ese correo, se procede a crearlo.
        if (usuario == null)
        {
            // Se crea un nuevo usuario con el correo "admin@admin.com" y el nombre "Admin"
            var user = new IdentityUser { UserName = "admin@admin.com", Email = "admin@admin.com" };

            // Se guarda el usuario en la base de datos con la contraseña predeterminada "admin".
            var result = await _userManager.CreateAsync(user, "admin1");

            // Se asigna el rol "ADMINISTRADOR" al nuevo usuario creado.
            await _userManager.AddToRoleAsync(user, "ADMINISTRADOR");
            

            // Se actualiza la variable "creado" para indicar si la creación fue exitosa.
            creado = result.Succeeded;
        }

        // CODIGO PARA BUSCAR EL USUARIO EN CASO DE NECESITARLO
        // Se vuelve a buscar el usuario "admin@admin.com" en la base de datos, en caso de necesitar su ID.
        var superusuario = _context.Users.Where(r => r.Email == "admin@admin.com").SingleOrDefault();
        if (superusuario != null)
        {
            // Aquí se obtiene el ID del usuario encontrado.
            var usuarioID = superusuario.Id;
        }

        // Devuelve un objeto JSON que indica si el usuario fue creado.
        return Json(creado);
    }
}
