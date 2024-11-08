using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Proyecto1.Controllers;
// [Authorize]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public JsonResult GetCuentas()
    {
        List<VistaCuenta> Usuarios = new List<VistaCuenta>();
        var usuarios = _context.Personas.Include(t => t.Users).ToList();

        foreach (var usuario in usuarios)
        {
            var rol = _context.Roles.Where(r => r.Id == usuario.CuentaID).SingleOrDefault();
            var cuenta = new VistaCuenta
            {
                Nombre = usuario.Nombre,
                Sexo = usuario.Sexo,
                Peso = usuario.Peso,
                Altura = usuario.Altura,
                Rol = rol.Name,
                Email = usuario.Users.Email
            }; Usuarios.Add(cuenta);
        }
        return Json(Usuarios);
    }
}