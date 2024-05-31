using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto1.Controllers;
// [Authorize]
public class EjerciciosController : Controller
{
    private readonly ApplicationDbContext _context;

    public EjerciciosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Ejercicios()
    {
        return View();
    }

    public JsonResult GetEjercicios(int? TipoEjercicioID)
    {
        var TipoEjercicios = _context.TipoEjercicios.ToList();

        if (TipoEjercicioID != null)
        {
            TipoEjercicios = TipoEjercicios.Where(e => e.TipoEjercicioID == TipoEjercicioID).ToList();
        }

        return Json(TipoEjercicios.ToList());
    }




    public JsonResult GuardarTipoEjercicio(string nombre, int TipoEjercicioID, bool Eliminado)
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
            if (TipoEjercicioID == 0)
            {
                //3- VERIFICAMOS SI EXISTE EN BASE DE DATOS UN REGISTRO CON LA MISMA DESCRIPCION
                //PARA REALIZAR ESA VERIFICACION BUSCAMOS EN EL CONTEXTO, ES DECIR EN BASE DE DATOS 
                //SI EXISTE UN REGISTRO CON ESA DESCRIPCION  
                var existeTipoEjercicio = _context.TipoEjercicios.Where(t => t.Nombre == nombre).Count();
                if (existeTipoEjercicio == 0)
                {
                    //4- GUARDAR EL TIPO DE EJERCICIO
                    var tipoEjercicio = new TipoEjercicio
                    {
                        Nombre = nombre
                    };
                    _context.Add(tipoEjercicio);
                    _context.SaveChanges();
                    resultado = "Se ha creado el nuevo ejercicio";
                }
                else if (existeTipoEjercicio == _context.TipoEjercicios.Where(t => t.Nombre == nombre && t.Eliminado == true).Count())
{
                    var activarEjercicio = _context.TipoEjercicios.FirstOrDefault(t => t.Nombre == nombre && t.Eliminado == true);
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
                var tipoEjercicioEditar = _context.TipoEjercicios.Where(t => t.TipoEjercicioID == TipoEjercicioID).SingleOrDefault();
                if (tipoEjercicioEditar != null)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UN REGISTRO CON EL MISMO NOMBRE PERO QUE EL ID SEA DISTINTO AL QUE ESTAMOS EDITANDO
                    var existeTipoEjercicio = _context.TipoEjercicios.Where(t => t.Nombre == nombre && t.TipoEjercicioID != TipoEjercicioID).Count();
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

    public JsonResult EliminarTipoEjercicio(int TipoEjercicioID)
    {
        var Ejercicio = _context.TipoEjercicios.Find(TipoEjercicioID);
        _context.Remove(Ejercicio);
        _context.SaveChanges();

        return Json(true);
    }

    public JsonResult DesactivarTipoEjercicio(int TipoEjercicioID)
    {
        var Ejercicio = _context.TipoEjercicios.Find(TipoEjercicioID);
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





