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
public class EjerciciosController : Controller
{
    private readonly ApplicationDbContext _context;

    public EjerciciosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Ejercicios(int? UsuarioID)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;

        return View();
    }

    public JsonResult GetEjercicios(int? TipoEjercicioID, int UsuarioID)
    {
        var TipoEjercicios = _context.TipoEjercicios.Where(ejercicio => ejercicio.Persona_TipoEjercicios.Any(relacion => relacion.PersonaID == UsuarioID && !relacion.Eliminado)).ToList();

        if (TipoEjercicioID != null)
        {
            TipoEjercicios = TipoEjercicios.Where(e => e.TipoEjercicioID == TipoEjercicioID).ToList();
        }

        if (UsuarioID == 0)
        {
            TipoEjercicios = _context.TipoEjercicios.ToList();
        }

        return Json(TipoEjercicios.ToList());
    }




    public JsonResult GuardarTipoEjercicio(string nombre, int TipoEjercicioID, bool Eliminado, int UsuarioID, float met)
    {

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
                var existeTipoEjercicio = _context.TipoEjercicios.Where(t => t.Nombre == nombre).FirstOrDefault();
                var relacionExistente = _context.Persona_tipoEjercicio.FirstOrDefault(t => t.PersonaID == UsuarioID && t.TipoEjercicioID == existeTipoEjercicio.TipoEjercicioID);
                if (existeTipoEjercicio == null)
                {
                    //4- GUARDAR EL TIPO DE EJERCICIO
                    var tipoEjercicio = new TipoEjercicio
                    {
                        Nombre = nombre,
                        MET = met
                    };
                    var relacion = new Persona_tipoEjercicio
                    {
                        PersonaID = UsuarioID,
                        TipoEjercicioID = tipoEjercicio.TipoEjercicioID,
                        Eliminado = false
                    };
                    _context.Add(tipoEjercicio);
                    _context.SaveChanges();
                    resultado = "Se ha creado el nuevo ejercicio";
                }
                else if (existeTipoEjercicio != null && relacionExistente != null)
                {
                    if (relacionExistente.Eliminado == true){
                        relacionExistente.Eliminado = false;
                        _context.SaveChanges();
                        resultado = "Se ha creado el nuevo ejercicio";
                    }
                }

                else if(existeTipoEjercicio != null && relacionExistente == null)
                {
                    var relacion = new Persona_tipoEjercicio
                    {
                        PersonaID = UsuarioID,
                        TipoEjercicioID = existeTipoEjercicio.TipoEjercicioID,
                        Eliminado = false
                    };
                    _context.Add(relacion);
                    _context.SaveChanges();
                    resultado = "Se ha creado el nuevo ejercicio";
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
                        tipoEjercicioEditar.MET = met;
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

    public JsonResult DesactivarTipoEjercicio(int TipoEjercicioID, int UsuarioID)
    {
        var Ejercicio = _context.Persona_tipoEjercicio.Where(t => t.TipoEjercicioID == TipoEjercicioID && t.PersonaID == UsuarioID).FirstOrDefault();
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





