using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Proyecto1.Controllers;

public class EjerciciosController : Controller
{
    private readonly ApplicationDbContext _context;

    public EjerciciosController(ApplicationDbContext context){
        _context = context;
    }

       public IActionResult Ejercicios()
    {
        return View();
    }

    public JsonResult GetEjercicios(int? id)
    {
    var Ejercicios = _context.Ejercicios.ToList();

    if (Ejercicios != null) {
        Ejercicios = Ejercicios.Where(e => e.Id == id).ToList();
    }

        return Json(_context.Ejercicios.ToList());
    }

}