using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto1.Controllers;

public class EjerciciosFisicosController : Controller 
{
    private ApplicationDbContext _context;

    public  EjerciciosFisicosController(ApplicationDbContext context)

    {
        _context = context;
    }

    public IActionResult Index()
    {
         // Crear una lista de SelectListItem que incluya el elemento adicional
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[SELECCIONE...]" }
        };

        // Obtener todas las opciones del enum
        var enumValues = Enum.GetValues(typeof(EstadoEmocional)).Cast<EstadoEmocional>();

        // Convertir las opciones del enum en SelectListItem
        selectListItems.AddRange(enumValues.Select(e => new SelectListItem
        {
            Value = e.GetHashCode().ToString(),
            Text = e.ToString().ToUpper()
        }));

        // Pasar la lista de opciones al modelo de la vista
        ViewBag.EstadoInicio = selectListItems.OrderBy(t => t.Text).ToList();
        ViewBag.EstadoFin = selectListItems.OrderBy(t => t.Text).ToList();

        var tipoEjercicios = _context.Ejercicios.ToList();
        tipoEjercicios.Add(new Ejercicio{IdEjercicio = 0, Nombre = "[SELECCIONE...]"});
        ViewBag.IdEjercicio = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "IdEjercicio", "Nombre");
        return View();
    }

    public JsonResult ListadoEjercicios(int? id)
    {

        List<VistaEjercicios> EjerciciosMostrar = new List<VistaEjercicios>();

        var ejerciciosFisicos = _context.EjerciciosFisicos.ToList();

            if (id != null)
            {
                ejerciciosFisicos = ejerciciosFisicos.Where(e => e.IdEjercicioFisico == id).ToList();
            }

            var Ejercicio = _context.Ejercicios.ToList();

            foreach (var ejercicioFisico in ejerciciosFisicos)
            {
                var ejercicio = Ejercicio.Where(e => e.IdEjercicio == ejercicioFisico.IdEjercicio).Single();

                var ejercicioMostrar = new VistaEjercicios
                {
                    IdEjercicioFisico = ejercicioFisico.IdEjercicioFisico,
                    IdEjercicio = ejercicioFisico.IdEjercicio,
                    EjercicioNombre = ejercicio.Nombre,
                    InicioString = ejercicioFisico.Inicio.ToString("dd/MM/yyyy HH:mm"),
                    FinString = ejercicioFisico.Fin.ToString("dd/MM/yyyy HH:mm"),
                    EstadoInicio = Enum.GetName(typeof(EstadoEmocional), ejercicioFisico.EstadoInicio),
                    EstadoFin = Enum.GetName(typeof(EstadoEmocional), ejercicioFisico.EstadoFin),
                    Observaciones = ejercicioFisico.Observaciones
                };
                EjerciciosMostrar.Add(ejercicioMostrar);
            }

            return Json(EjerciciosMostrar);
    }


    public JsonResult GetEjerciciosFisicos(int? id)
    {
        var EjerciciosFisicos = _context.EjerciciosFisicos.ToList();

        if (id != null)
        {
            EjerciciosFisicos = EjerciciosFisicos.Where(e => e.IdEjercicioFisico == id).ToList();
        }

        return Json(EjerciciosFisicos);

    }

    public JsonResult GuardarEjercicioFisico(int IdEjercicioFisico, int IdEjercicio, DateTime Inicio, DateTime Fin, EstadoEmocional EstadoInicio, EstadoEmocional EstadoFin, string Observaciones)
    {
        //1- VERIFICAMOS SI REALMENTE INGRESO ALGUN CARACTER Y LA VARIABLE NO SEA NULL
        // if (descripcion != null && descripcion != "")
        // {
        //     //INGRESA SI ESCRIBIO SI O SI
        // }         
        string resultado = "";
        if (IdEjercicioFisico > 0)
        {
            //2- VERIFICAR SI ESTA EDITANDO O CREANDO NUEVO REGISTRO
            if (IdEjercicioFisico == 0)
            {
                var EjercicioFisico = new EjercicioFisico
                {
                    IdEjercicioFisico = IdEjercicioFisico,
                    // IdEjercicio = IdEjercicio,
                    Inicio = Inicio,
                    Fin = Fin,
                    EstadoInicio = EstadoInicio,
                    EstadoFin = EstadoFin,
                    Observaciones = Observaciones
                };
                _context.Add(EjercicioFisico);
                _context.SaveChanges();
            }   

            else{
                var ejercicioFisicoEditar = _context.EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).SingleOrDefault();
                if (ejercicioFisicoEditar != null)
                {
                    var existeEjercicioFisico = _context.EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).Count();
                    if (existeEjercicioFisico == 0) {
                        ejercicioFisicoEditar.Inicio = Inicio;
                        ejercicioFisicoEditar.Fin = Fin;
                        ejercicioFisicoEditar.EstadoInicio = EstadoInicio;
                        ejercicioFisicoEditar.EstadoFin = EstadoFin;
                        ejercicioFisicoEditar.Observaciones = Observaciones;
                        _context.SaveChanges();
                    }
                }
            }
        }
        return Json(resultado);
    }
    
     public JsonResult GetEstadoEmocional(){
        var EstadoEmocional = Enum.GetNames(typeof(EstadoEmocional)).ToList();
        return Json(EstadoEmocional.ToList());
    }


    public JsonResult DeleteEjercicioFisico(int IdEjercicioFisico)
    {
        var EjercicioFisico = _context.EjerciciosFisicos.Find(IdEjercicioFisico);
        _context.Remove(EjercicioFisico);
        _context.SaveChanges();

        return Json(true);
    }
    
}