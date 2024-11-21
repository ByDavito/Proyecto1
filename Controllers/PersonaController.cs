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
[Authorize]
public class personaController : Controller
{
    private readonly ApplicationDbContext _context;

    public personaController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int UsuarioID)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;

        return View();
    }

    public JsonResult GetPersona(int? UsuarioID)
    {
        var persona = _context.Personas.Where(t => t.PersonaID == UsuarioID).FirstOrDefault();
        return Json(persona);
    }

    public JsonResult GuardarDatosPersona(int? UsuarioID, string? Nombre, float Altura, float Peso, DateTime fecha, Sexo Sexo){
        string resultado = "";
        var persona = _context.Personas.Where(t => t.PersonaID == UsuarioID).FirstOrDefault();
        if(persona != null){
        persona.Nombre = Nombre;
        persona.Altura = Altura;
        persona.Peso = Peso;
        persona.FechaNacimiento = fecha;
        persona.Sexo = Sexo;
        _context.SaveChanges();
        resultado = "Se ha actualizado el perfil";
        return Json(resultado);
        }
        else{
            resultado = "No se ha podido actualizar el perfil";
            return Json(resultado);}
    }
}