using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Proyecto1.Controllers;

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

    public JsonResult GetEjercicios(int? IdEjercicio)
    {
        var Ejercicios = _context.Ejercicios.ToList();

        if (IdEjercicio != null)
        {
            Ejercicios = Ejercicios.Where(e => e.IdEjercicio == IdEjercicio).ToList();
        }

        return Json(Ejercicios.ToList());
    }

   


    public JsonResult GuardarTipoEjercicio(string nombre, int IdEjercicio)
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

        if (!String.IsNullOrEmpty(nombre))
        {
            nombre = nombre.ToUpper();
            //INGRESA SI ESCRIBIO SI O SI 

            //2- VERIFICAR SI ESTA EDITANDO O CREANDO NUEVO REGISTRO
            if (IdEjercicio == 0)
            {
                //3- VERIFICAMOS SI EXISTE EN BASE DE DATOS UN REGISTRO CON LA MISMA DESCRIPCION
                //PARA REALIZAR ESA VERIFICACION BUSCAMOS EN EL CONTEXTO, ES DECIR EN BASE DE DATOS 
                //SI EXISTE UN REGISTRO CON ESA DESCRIPCION  
                var existeTipoEjercicio = _context.Ejercicios.Where(t => t.Nombre == nombre).Count();
                if (existeTipoEjercicio == 0)
                {
                    //4- GUARDAR EL TIPO DE EJERCICIO
                    var tipoEjercicio = new Ejercicio
                    {
                        Nombre = nombre
                    };
                    _context.Add(tipoEjercicio);
                    _context.SaveChanges();
                }
                else
                {
                    resultado = "YA EXISTE UN REGISTRO CON LA MISMA DESCRIPCIÓN";
                }
            }
            else
            {
                //QUIERE DECIR QUE VAMOS A EDITAR EL REGISTRO
                var tipoEjercicioEditar = _context.Ejercicios.Where(t => t.IdEjercicio == IdEjercicio).SingleOrDefault();
                if (tipoEjercicioEditar != null)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UN REGISTRO CON EL MISMO NOMBRE PERO QUE EL ID SEA DISTINTO AL QUE ESTAMOS EDITANDO
                    var existeTipoEjercicio = _context.Ejercicios.Where(t => t.Nombre == nombre && t.IdEjercicio != IdEjercicio).Count();
                    if (existeTipoEjercicio == 0)
                    {
                        //QUIERE DECIR QUE EL ELEMENTO EXISTE Y ES CORRECTO ENTONCES CONTINUAMOS CON EL EDITAR
                        tipoEjercicioEditar.Nombre = nombre;
                        _context.SaveChanges();
                    }
                    else
                    {
                        resultado = "YA EXISTE UN REGISTRO CON LA MISMA DESCRIPCIÓN";
                    }
                }
            }
        }
        else
        {
            resultado = "DEBE INGRESAR UNA DESCRIPCIÓN.";
        }

        return Json(resultado);
    }

public JsonResult EliminarTipoEjercicio(int IdEjercicio)
    {
        var Ejercicio = _context.Ejercicios.Find(IdEjercicio);
        _context.Remove(Ejercicio);
        _context.SaveChanges();

        return Json(true);
    }

}




