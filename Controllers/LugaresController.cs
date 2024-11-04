using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Proyecto1.Controllers;
// [Authorize]
public class LugaresController : Controller
{
    private readonly ApplicationDbContext _context;

    public LugaresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Lugares(int? UsuarioID)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;
        
        return View();
    }

    public JsonResult GetLugares(int? LugarID, int UsuarioID)
    {
        var Lugares = _context.Lugares.Where(e => e.PersonaID == UsuarioID && e.Eliminado == false).ToList();

        if (LugarID != null)
        {
            Lugares = Lugares.Where(e => e.LugarID == LugarID).ToList();
        }

        return Json(Lugares);
    }




    public JsonResult GuardarLugar(string nombre, int LugarID, bool Eliminado, int UsuarioID)
    {
        //1- VERIFICAMOS SI REALMENTE INGRESO ALGUN CARACTER Y LA VARIABLE NO SEA NULL
        // if (descripcion != null && descripcion != "")
        // {
        //     //INGRESA SI ESCRIBIO SI O SI
        // }

        // if (String.IsNullOrEmpty(descripcion) == false)
        // {
        //     //INGRESA SI ESCRIBIO SI O SI 
        // }

        string resultado = "";

        if (!string.IsNullOrEmpty(nombre))
        {
            nombre = nombre.ToUpper();
            //INGRESA SI ESCRIBIO SI O SI 

            //2- VERIFICAR SI ESTA EDITANDO O CREANDO NUEVO REGISTRO
            if (LugarID == 0)
            {
                //3- VERIFICAMOS SI EXISTE EN BASE DE DATOS UN REGISTRO CON LA MISMA DESCRIPCION
                //PARA REALIZAR ESA VERIFICACION BUSCAMOS EN EL CONTEXTO, ES DECIR EN BASE DE DATOS 
                //SI EXISTE UN REGISTRO CON ESA DESCRIPCION  
                var existeTipoEjercicio = _context.Lugares.Where(t => t.Nombre == nombre).Count();
                if (existeTipoEjercicio == 0)
                {
                    //4- GUARDAR EL TIPO DE EJERCICIO
                    var lugar = new Lugar
                    {
                        Nombre = nombre,
                        PersonaID = UsuarioID
                    };
                    _context.Add(lugar);
                    _context.SaveChanges();
                    resultado = "Se ha creado el nuevo ejercicio";
                }
                else if (existeTipoEjercicio == _context.Lugares.Where(t => t.Nombre == nombre && t.Eliminado == true).Count())
                {
                    var activarEjercicio = _context.Lugares.FirstOrDefault(t => t.Nombre == nombre && t.Eliminado == true);
                    if (activarEjercicio != null)
                    {
                        activarEjercicio.Eliminado = false;
                        _context.SaveChanges();
                    }
                }

                else
                {
                    resultado = "Ya existe un registro con el mismo nombre";
                }


            }

            {
                //QUIERE DECIR QUE VAMOS A EDITAR EL REGISTRO
                var tipoEjercicioEditar = _context.Lugares.Where(t => t.LugarID == LugarID).SingleOrDefault();
                if (tipoEjercicioEditar != null)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UN REGISTRO CON EL MISMO NOMBRE PERO QUE EL ID SEA DISTINTO AL QUE ESTAMOS EDITANDO
                    var existeTipoEjercicio = _context.Lugares.Where(t => t.Nombre == nombre && t.LugarID != LugarID).Count();
                    if (existeTipoEjercicio == 0)
                    {
                        //QUIERE DECIR QUE EL ELEMENTO EXISTE Y ES CORRECTO ENTONCES CONTINUAMOS CON EL EDITAR
                        tipoEjercicioEditar.Nombre = nombre;
                        _context.SaveChanges();
                        resultado = "Se ha editado el ejercicio";
                    }
                    else
                    {
                        resultado = "Ya existe un registro con el mismo nombre";
                    }
                }
            }
        }
        else
        {
            resultado = "Debe esbribir un nombre.";
        }

        return Json(resultado);
    }

    public JsonResult EliminarTipoEjercicio(int LugarID)
    {
        var Ejercicio = _context.Lugares.Find(LugarID);
        _context.Remove(Ejercicio);
        _context.SaveChanges();

        return Json(true);
    }

    public JsonResult DesactivarTipoEjercicio(int LugarID)
    {
        var Ejercicio = _context.Lugares.Find(LugarID);
        if (Ejercicio.Eliminado == false)
        {
            Ejercicio.Eliminado = true;
            _context.SaveChanges();
            return Json(true);
        }
        else
        {
            Ejercicio.Eliminado = false;
        }
        _context.SaveChanges();
        return Json(true);

    }

}





