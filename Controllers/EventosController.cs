using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Proyecto1.Controllers;
// [Authorize]
public class EventosController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventosController(ApplicationDbContext context)
    {
        _context = context;
    }


    public IActionResult Index()
    {
        return View();
    }

       public JsonResult GetEventos(int? EventoID)
    {
        var Eventos = _context.Eventos.ToList();

        if (EventoID != null)
        {
            Eventos = Eventos.Where(e => e.EventoID == EventoID).ToList();
        }

        return Json(Eventos.ToList());
    }

    public JsonResult GuardarEvento(string nombre, int EventoID, bool Eliminado)
    {
        string resultado = "";

        if (!string.IsNullOrEmpty(nombre))
        {
            nombre = nombre.ToUpper();
            //INGRESA SI ESCRIBIO SI O SI 

            //2- VERIFICAR SI ESTA EDITANDO O CREANDO NUEVO REGISTRO
            if (EventoID == 0)
            {
                //3- VERIFICAMOS SI EXISTE EN BASE DE DATOS UN REGISTRO CON LA MISMA DESCRIPCION
                //PARA REALIZAR ESA VERIFICACION BUSCAMOS EN EL CONTEXTO, ES DECIR EN BASE DE DATOS 
                //SI EXISTE UN REGISTRO CON ESA DESCRIPCION  
                var existeEvento = _context.Eventos.Where(t => t.Nombre == nombre).Count();
                if (existeEvento == 0)
                {
                    //4- GUARDAR EL TIPO DE EJERCICIO
                    var Evento = new Evento
                    {
                        Nombre = nombre
                    };
                    _context.Add(Evento);
                    _context.SaveChanges();
                    resultado = "Se ha creado el nuevo ejercicio";
                }
                else if (existeEvento == _context.Eventos.Where(t => t.Nombre == nombre && t.Eliminado == true).Count())
                {
                    var activarEvento = _context.Eventos.FirstOrDefault(t => t.Nombre == nombre && t.Eliminado == true);
                    if (activarEvento != null)
                    {
                        activarEvento.Eliminado = false;
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
                var EventoEditar = _context.Eventos.Where(t => t.EventoID == EventoID).SingleOrDefault();
                if (EventoEditar != null)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE UN REGISTRO CON EL MISMO NOMBRE PERO QUE EL ID SEA DISTINTO AL QUE ESTAMOS EDITANDO
                    var existeEvento = _context.Eventos.Where(t => t.Nombre == nombre && t.EventoID != EventoID).Count();
                    if (existeEvento == 0)
                    {
                        //QUIERE DECIR QUE EL ELEMENTO EXISTE Y ES CORRECTO ENTONCES CONTINUAMOS CON EL EDITAR
                        EventoEditar.Nombre = nombre;
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

     public JsonResult EliminarEvento(int EventoID)
    {
        var Evento = _context.Lugares.Find(EventoID);
        _context.Remove(Evento);
        _context.SaveChanges();

        return Json(true);
    }

    public JsonResult DesactivarEvento(int EventoID)
    {
        var Evento = _context.Eventos.Find(EventoID);
        if (Evento.Eliminado == false)
        {
            Evento.Eliminado = true;
            _context.SaveChanges();
            return Json(true);
        }
        else
        {
            Evento.Eliminado = false;
        }
        _context.SaveChanges();
        return Json(true);

    }

   public IActionResult InformeGeneral()
   {
       return View();
   }
    public JsonResult InformeCompleto(DateTime? FechaDesde, DateTime? FechaHasta)
    {
        List<VistaEvento> VistaEvento = new List<VistaEvento>();

        var ejercicios = _context.EjerciciosFisicos.Where(e => e.Inicio >= FechaDesde && e.Fin <= FechaHasta).Include(e => e.TipoEjercicio).Include(e => e.Lugar).Include(e => e.Evento).ToList();

        foreach (var ejercicio in ejercicios)
        {
            var evento = VistaEvento.Where(e => e.EventoID == ejercicio.EventoID).SingleOrDefault();
            if(evento == null)
            {
                evento = new VistaEvento
                {
                    EventoID = ejercicio.EventoID,
                    Nombre = ejercicio.Evento.Nombre,
                    VistaLugar = new List<VistaLugar>()
                };
                VistaEvento.Add(evento);
                }
            
            var lugar = evento.VistaLugar.Where(l => l.LugarID == ejercicio.LugarID).SingleOrDefault();
            if(lugar == null)
            {
                lugar = new VistaLugar
                {
                    LugarID = ejercicio.LugarID,
                    Nombre = ejercicio.Lugar.Nombre,
                    vistaTipoEjercicios = new List<VistaTipoEjercicio>()
                };
                evento.VistaLugar.Add(lugar);
            }

            var tipoEjercicio = lugar.vistaTipoEjercicios.Where(e => e.TipoEjercicioID == ejercicio.TipoEjercicioID).SingleOrDefault();
            if(tipoEjercicio == null)
            {
                tipoEjercicio = new VistaTipoEjercicio
                {
                    TipoEjercicioID = ejercicio.TipoEjercicioID,
                    Nombre = ejercicio.TipoEjercicio.Nombre,
                    VistaEjerciciosGeneral = new List<VistaEjerciciosGeneral>()
                };
                lugar.vistaTipoEjercicios.Add(tipoEjercicio);
            }

            var Ejercicio = new VistaEjerciciosGeneral
            {
                InicioString = ejercicio.Inicio.ToString(),
                FinString = ejercicio.Fin.ToString(),
                EstadoInicio = ejercicio.EstadoInicio.ToString(),
                EstadoFin = ejercicio.EstadoFin.ToString(),
                Observaciones = ejercicio.Observaciones,
            };
            tipoEjercicio.VistaEjerciciosGeneral.Add(Ejercicio);
        }
        return Json(VistaEvento);
    }
    

}