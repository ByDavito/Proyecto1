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
        var usuarios = _context.Personas.ToList();

        foreach (var usuario in usuarios)
        {
            var rol = _context.UserRoles.Where(r => r.UserId == usuario.CuentaID).SingleOrDefault();
            var rolName = _context.Roles.Where(r => r.Id == rol.RoleId).SingleOrDefault();
            var Email = _context.Users.Where(r => r.Id == usuario.CuentaID).SingleOrDefault();
            var edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;
            var imc = usuario.Peso / (usuario.Altura * usuario.Altura);
            var cuenta = new VistaCuenta
            {
                Nombre = usuario.Nombre,
                Sexo = Enum.GetName(typeof(Sexo), usuario.Sexo).ToString(),
                Peso = usuario.Peso,
                Altura = usuario.Altura,
                Rol = rolName.Name,
                Email = Email.Email,
                FechaNacimiento = usuario.FechaNacimiento.ToString("dd/MM/yyyy"),
                Edad = edad.ToString(),
                imc = imc.ToString()
            }; Usuarios.Add(cuenta);
        }
        return Json(Usuarios);
    }
}