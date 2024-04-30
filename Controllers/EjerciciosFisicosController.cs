using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;

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
        return View();
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

    public JsonResult GuardarTipoEjercicio(int IdEjercicioFisico, int IdEjercicio, DateTime Inicio, DateTime Fin, EstadoEmocional EstadoInicio, EstadoEmocional EstadoFin, string Observaciones)
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