using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto1.Controllers;

// [Authorize]
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
        var tipoEjercicios = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();
        var tipoEjercicioBuscar = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();

        tipoEjercicios.Add(new TipoEjercicio{TipoEjercicioID = 0, Nombre = "[SELECCIONE...]"});
        tipoEjercicioBuscar.Add(new TipoEjercicio{TipoEjercicioID = 0, Nombre = "[SELECCIONE...]"});

        ViewBag.TipoEjercicioBuscarID = new SelectList(tipoEjercicioBuscar.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");
        ViewBag.IdEjercicio = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");
        return View();
    }

       public IActionResult Graficos()
    {
      var tipoEjercicios = _context.TipoEjercicios.ToList();
        ViewBag.TipoEjercicioBuscarID = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");

        return View();
    }

    public JsonResult GraficoLinearEjecicios(int TipoEjercicioID, int Mes, int Anio)
    {
        List<VistaEjercicioFisico> ejercicioFisicos = new List<VistaEjercicioFisico>();

        var diaXMes = DateTime.DaysInMonth(Anio, Mes);

        DateTime fechaMes = new DateTime();
        fechaMes = fechaMes.AddMonths(Mes - 1);
        
        for (int i = 1; i <= diaXMes; i++)
        {
            var diaMesMostrar = new VistaEjercicioFisico
            {
                Dia = i,
                Mes = fechaMes.ToString("MMM").ToUpper(),
                CantidadMinutos = 0
            };

            ejercicioFisicos.Add(diaMesMostrar);
        }

        var Ejercicios = _context.EjerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicioID && e.Inicio.Month == Mes && e.Inicio.Year == Anio).ToList();

        foreach (var ejercicio in Ejercicios.OrderBy(e => e.Inicio))
        {
            var ejercicioMostrar = ejercicioFisicos.Where(e => e.Dia == ejercicio.Inicio.Day).SingleOrDefault();
            if (ejercicioMostrar != null)
            {
                ejercicioMostrar.CantidadMinutos += (int)ejercicio.Fin.Subtract(ejercicio.Inicio).TotalMinutes;
            }
        }

        return Json(ejercicioFisicos);
    }

    public JsonResult GraficoCircularEjecicios(int Mes, int Anio)
    {
        var VistaTipoEjercicioFisico = new List<VistaTipoEjercicioFisico>();

        var TipoEjercicios = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();

        foreach (var TipoEjercicio in TipoEjercicios)
        {
            var Ejercicios = _context.EjerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicio.TipoEjercicioID && 
            e.Inicio.Month == Mes && e.Inicio.Year == Anio).ToList();

            foreach (var ejercicio in Ejercicios)
            {
                var ejercicioMostrar = VistaTipoEjercicioFisico.Where(e => e.TipoEjercicioID == TipoEjercicio.TipoEjercicioID).SingleOrDefault();
                if (ejercicioMostrar == null)
                {
                    ejercicioMostrar = new VistaTipoEjercicioFisico
                    {
                    CantidadMinutos = (int)ejercicio.Fin.Subtract(ejercicio.Inicio).TotalMinutes,
                    Nombre = ejercicio.TipoEjercicio.Nombre,
                    TipoEjercicioID = ejercicio.TipoEjercicioID
                    };
                    VistaTipoEjercicioFisico.Add(ejercicioMostrar);
                }
                else
                {
                    ejercicioMostrar.CantidadMinutos += (int)ejercicio.Fin.Subtract(ejercicio.Inicio).TotalMinutes;
                }
            }
        }

        return Json(VistaTipoEjercicioFisico);
    }



    public JsonResult ListadoEjercicios(int? id, DateTime? FechaDesde, DateTime? FechaHasta, int? TipoEjercicioBuscarID)
    {

        List<VistaEjercicios> EjerciciosMostrar = new List<VistaEjercicios>();

        var ejerciciosFisicos = _context.EjerciciosFisicos.ToList();

            if (id != null)
            {
                ejerciciosFisicos = ejerciciosFisicos.Where(e => e.IdEjercicioFisico == id).ToList();
            }

            if(FechaDesde != null && FechaHasta != null) {
                ejerciciosFisicos = ejerciciosFisicos.Where(e => e.Inicio >= FechaDesde && e.Inicio <= FechaHasta).ToList();
            }

            if(TipoEjercicioBuscarID != 0) {
                ejerciciosFisicos = ejerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicioBuscarID).ToList();
            }

            var Ejercicio = _context.TipoEjercicios.ToList();

            foreach (var ejercicioFisico in ejerciciosFisicos)
            {
                var ejercicio = Ejercicio.Where(e => e.TipoEjercicioID == ejercicioFisico.TipoEjercicioID).Single();

                var ejercicioMostrar = new VistaEjercicios
                {
                    IdEjercicioFisico = ejercicioFisico.IdEjercicioFisico,
                    TipoEjercicioID = ejercicioFisico.TipoEjercicioID,
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


    public JsonResult GetEjerciciosFisicos(int? IdEjercicioFisico)
    {
        var EjerciciosFisicos = _context.EjerciciosFisicos.ToList();

        if (IdEjercicioFisico != null)
        {
            EjerciciosFisicos = EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).ToList();
        }

        return Json(EjerciciosFisicos.ToList());

    }

    public JsonResult GuardarEjercicioFisico(int IdEjercicioFisico, int TipoEjercicioID, DateTime Inicio, DateTime Fin, EstadoEmocional EstadoInicio, EstadoEmocional EstadoFin, string Observaciones)
    {
        //1- VERIFICAMOS SI REALMENTE INGRESO ALGUN CARACTER Y LA VARIABLE NO SEA NULL
        // if (descripcion != null && descripcion != "")
        // {
        //     //INGRESA SI ESCRIBIO SI O SI
        // }         
        string resultado = "";
        if (IdEjercicioFisico != null)
        {
            //2- VERIFICAR SI ESTA EDITANDO O CREANDO NUEVO REGISTRO
            if (IdEjercicioFisico == 0)
            {
                var EjercicioFisico = new EjercicioFisico
                {
                    IdEjercicioFisico = IdEjercicioFisico,
                    TipoEjercicioID = TipoEjercicioID,
                    Inicio = Inicio,
                    Fin = Fin,
                    EstadoInicio = EstadoInicio,
                    EstadoFin = EstadoFin,
                    Observaciones = Observaciones
                };
                _context.Add(EjercicioFisico);
                _context.SaveChanges();
                resultado = "Se ha creado el registro";
            }   

            else{
                var ejercicioFisicoEditar = _context.EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).SingleOrDefault();
                
                {
                    var existeEjercicioFisico = _context.EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).Count(); {
                        ejercicioFisicoEditar.TipoEjercicioID = TipoEjercicioID;
                        ejercicioFisicoEditar.Inicio = Inicio;
                        ejercicioFisicoEditar.Fin = Fin;
                        ejercicioFisicoEditar.EstadoInicio = EstadoInicio;
                        ejercicioFisicoEditar.EstadoFin = EstadoFin;
                        ejercicioFisicoEditar.Observaciones = Observaciones;
                        _context.SaveChanges();
                        resultado = "Se ha editado el registro";
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