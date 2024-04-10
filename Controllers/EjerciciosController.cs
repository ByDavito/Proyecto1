using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;

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
}