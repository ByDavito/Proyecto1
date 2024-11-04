using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Proyecto1.Controllers;

// [Authorize]
public class EjerciciosFisicosController : Controller
{
    private ApplicationDbContext _context;

    public EjerciciosFisicosController(ApplicationDbContext context)

    {
        _context = context;
    }

    public IActionResult Index(int? UsuarioID)
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

       var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;

        // Pasar la lista de opciones al modelo de la vista
        ViewBag.EstadoInicio = selectListItems.OrderBy(t => t.Text).ToList();
        ViewBag.EstadoFin = selectListItems.OrderBy(t => t.Text).ToList();
        var tipoEjercicios = _context.TipoEjercicios.Where(e => e.Eliminado == false && e.PersonaID == UsuarioID).ToList();
        var tipoEjercicioBuscar = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();
        var LugarID = _context.Lugares.Where(e => e.Eliminado == false && e.PersonaID == UsuarioID).ToList();
        var EventoID = _context.Eventos.Where(e => e.Eliminado == false && e.PersonaID == UsuarioID).ToList();


        tipoEjercicios.Add(new TipoEjercicio { TipoEjercicioID = 0, Nombre = "[SELECCIONE...]" });
        tipoEjercicioBuscar.Add(new TipoEjercicio { TipoEjercicioID = 0, Nombre = "[SELECCIONE...]" });
        EventoID.Add(new Evento { EventoID = 0, Nombre = "[SELECCIONE...]" });


        ViewBag.TipoEjercicioBuscarID = new SelectList(tipoEjercicioBuscar.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");
        ViewBag.IdEjercicio = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");
        ViewBag.LugarID = new SelectList(LugarID.OrderBy(c => c.LugarID), "LugarID", "Nombre");
        ViewBag.EventoID = new SelectList(EventoID.OrderBy(c => c.EventoID), "EventoID", "Nombre");
        return View();
    }

    public IActionResult Graficos(int? UsuarioID)
    {
        var tipoEjercicios = _context.TipoEjercicios.ToList();
        ViewBag.TipoEjercicioBuscarID = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;


        return View();
    }

    public ActionResult Informe(int? UsuarioID)
    {
        var tipoEjercicios = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();
        tipoEjercicios.Add(new TipoEjercicio { TipoEjercicioID = 0, Nombre = "[SELECCIONE...]" });
        ViewBag.IdEjercicio = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;

        return View();
    }

    public ActionResult LugarInforme(int? UsuarioID)
    {
        var tipoEjercicios = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();
        tipoEjercicios.Add(new TipoEjercicio { TipoEjercicioID = 0, Nombre = "[SELECCIONE...]" });
        ViewBag.IdEjercicio = new SelectList(tipoEjercicios.OrderBy(c => c.Nombre), "TipoEjercicioID", "Nombre");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UsuarioID = _context.Personas
                .Where(t => t.CuentaID == userId)
                .Select(t => t.PersonaID) // Proyecta solo el campo UsuarioID
                .SingleOrDefault(); ;

        ViewBag.UsuarioID = UsuarioID;

        return View();
    }

    public JsonResult GraficoLinearEjecicios(int TipoEjercicioID, int Mes, int Anio, int? UsuarioID)
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

        var Ejercicios = _context.EjerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicioID && e.Inicio.Month == Mes && e.Inicio.Year == Anio && e.PersonaID == UsuarioID).ToList();

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

    public JsonResult GraficoCircularEjecicios(int Mes, int Anio, int? UsuarioID)
    {
        var VistaTipoEjercicioFisico = new List<VistaTipoEjercicioFisico>();

        var TipoEjercicios = _context.TipoEjercicios.Where(e => e.Eliminado == false).ToList();

        foreach (var TipoEjercicio in TipoEjercicios)
        {
            var Ejercicios = _context.EjerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicio.TipoEjercicioID &&
            e.Inicio.Month == Mes && e.Inicio.Year == Anio && e.PersonaID == UsuarioID).ToList();

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

    public JsonResult ListadoInforme(DateTime? FechaDesde, DateTime? FechaHasta, int? TipoEjercicioID, int? UsuarioID)
    {
        List<VistaInforme> Ejercicios = new List<VistaInforme>();

        var tipoEjercicio = _context.TipoEjercicios.Where(t => t.Eliminado == false ).OrderBy(t => t.Nombre).ToList();
        if (TipoEjercicioID != 0)
        {
            tipoEjercicio = _context.TipoEjercicios.Where(t => t.Eliminado == false && t.TipoEjercicioID == TipoEjercicioID).OrderBy(t => t.Nombre).ToList();
        }



        foreach (var Ejercicio in tipoEjercicio)
        {
            var registros = _context.EjerciciosFisicos.Where(e => e.TipoEjercicioID == Ejercicio.TipoEjercicioID && e.PersonaID == UsuarioID).OrderBy(e => e.Inicio).ToList();

            var ejercicioMostrar = new VistaInforme
            {
                TipoEjercicioID = Ejercicio.TipoEjercicioID,
                TipoEjercicioNombre = Ejercicio.Nombre,
                Ejercicios = new List<VistaEjercicios>()
            };

            foreach (var ejercicio in registros)
            {
                if (FechaDesde != null && FechaHasta != null)
                {


                    if (ejercicio.Inicio >= FechaDesde && ejercicio.Fin <= FechaHasta)
                    {
                        var EjerciciosData = new VistaEjercicios
                        {
                            IdEjercicioFisico = ejercicio.IdEjercicioFisico,
                            TipoEjercicioID = ejercicio.TipoEjercicioID,
                            EjercicioNombre = ejercicio.TipoEjercicio.Nombre,
                            InicioString = ejercicio.Inicio.ToString("dd/MM/yyyy HH:mm"),
                            FinString = ejercicio.Fin.ToString("dd/MM/yyyy HH:mm"),
                            EstadoInicio = Enum.GetName(typeof(EstadoEmocional), ejercicio.EstadoInicio),
                            EstadoFin = Enum.GetName(typeof(EstadoEmocional), ejercicio.EstadoFin),
                            Observaciones = ejercicio.Observaciones,
                            Duracion = ejercicio.Intervalo.ToString()
                        };
                        ejercicioMostrar.Ejercicios.Add(EjerciciosData);
                    }
                }
                else
                {
                    var EjerciciosData = new VistaEjercicios
                    {
                        IdEjercicioFisico = ejercicio.IdEjercicioFisico,
                        TipoEjercicioID = ejercicio.TipoEjercicioID,
                        EjercicioNombre = ejercicio.TipoEjercicio.Nombre,
                        InicioString = ejercicio.Inicio.ToString("dd/MM/yyyy HH:mm"),
                        FinString = ejercicio.Fin.ToString("dd/MM/yyyy HH:mm"),
                        EstadoInicio = Enum.GetName(typeof(EstadoEmocional), ejercicio.EstadoInicio),
                        EstadoFin = Enum.GetName(typeof(EstadoEmocional), ejercicio.EstadoFin),
                        Observaciones = ejercicio.Observaciones,
                        Duracion = ejercicio.Intervalo.ToString()
                    };
                    ejercicioMostrar.Ejercicios.Add(EjerciciosData);
                }
            }
            Ejercicios.Add(ejercicioMostrar);
        }
        return Json(Ejercicios);
    }


    public JsonResult informeLugar(DateTime? FechaDesde, DateTime? FechaHasta)
    {
        List<Lugarvista> Informe = new List<Lugarvista>();


        var Lugares = _context.Lugares.Where(l => l.Eliminado == false && l.PersonaID == 1).OrderBy(l => l.Nombre).ToList();
         var ejerciciosFisicos = _context.EjerciciosFisicos.Where(e => e.Inicio >= FechaDesde && e.Inicio <= FechaHasta && e.PersonaID == 1).OrderBy(e => e.Inicio).ToList();
         var tipoEjercicio = _context.TipoEjercicios.OrderBy(t => t.Nombre).ToList();
        foreach (var item in Lugares)
        {
            var LugarMostrar = new Lugarvista
            {
                IdLugar = item.LugarID,
                Nombre = item.Nombre,
                LugarEjercicios = new List<LugarEjercicios>()
            };

           

            foreach (var ejercicio in ejerciciosFisicos)
            {
                if (ejercicio.LugarID == item.LugarID)
                {
                    var LugarEjerciciosData = new LugarEjercicios
                    {
                        IdEjercicioFisico = ejercicio.IdEjercicioFisico,
                        TipoEjercicioID = ejercicio.TipoEjercicioID,
                        EjercicioNombre = ejercicio.TipoEjercicio.Nombre,
                        InicioString = ejercicio.Inicio.ToString("dd/MM/yyyy HH:mm"),
                        FinString = ejercicio.Fin.ToString("dd/MM/yyyy HH:mm"),
                        Observaciones = ejercicio.Observaciones,
                        Duracion = ejercicio.Intervalo.ToString()
                    };
                    LugarMostrar.LugarEjercicios.Add(LugarEjerciciosData);
                }
            }
            Informe.Add(LugarMostrar);
        }
        return Json(Informe);
    }


    public JsonResult ListadoEjercicios(int? id, DateTime? FechaDesde, DateTime? FechaHasta, int? TipoEjercicioBuscarID, int? UsuarioID)
    {

        List<VistaEjercicios> EjerciciosMostrar = new List<VistaEjercicios>();

        var ejerciciosFisicos = _context.EjerciciosFisicos.Where(e => e.PersonaID == UsuarioID).ToList();

        if (id != null)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(e => e.IdEjercicioFisico == id).ToList();
        }

        if (FechaDesde != null && FechaHasta != null)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(e => e.Inicio >= FechaDesde && e.Inicio <= FechaHasta).ToList();
        }

        if (TipoEjercicioBuscarID != 0)
        {
            ejerciciosFisicos = ejerciciosFisicos.Where(e => e.TipoEjercicioID == TipoEjercicioBuscarID).ToList();
        }

        var Ejercicio = _context.TipoEjercicios.ToList();

        foreach (var ejercicioFisico in ejerciciosFisicos)
        {
            var ejercicio = Ejercicio.Where(e => e.TipoEjercicioID == ejercicioFisico.TipoEjercicioID).Single();

            var Lugar = _context.Lugares.Find(ejercicioFisico.LugarID);

            var Evento = _context.Eventos.Find(ejercicioFisico.EventoID);

            var ejercicioMostrar = new VistaEjercicios
            {
                IdEjercicioFisico = ejercicioFisico.IdEjercicioFisico,
                TipoEjercicioID = ejercicioFisico.TipoEjercicioID,
                EjercicioNombre = ejercicio.Nombre,
                Lugar = Lugar.Nombre,
                Evento = Evento.Nombre,
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

    public JsonResult GuardarEjercicioFisico(int IdEjercicioFisico, int TipoEjercicioID, int LugarID,int EventoID, DateTime Inicio, DateTime Fin, EstadoEmocional EstadoInicio, EstadoEmocional EstadoFin, string Observaciones, int UsuarioID)
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
                    EventoID = EventoID,
                    LugarID = LugarID,
                    Inicio = Inicio,
                    Fin = Fin,
                    EstadoInicio = EstadoInicio,
                    EstadoFin = EstadoFin,
                    Observaciones = Observaciones,
                    PersonaID = UsuarioID
                };
                _context.Add(EjercicioFisico);
                _context.SaveChanges();
                resultado = "Se ha creado el registro";
            }

            else
            {
                var ejercicioFisicoEditar = _context.EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).SingleOrDefault();

                {
                    var existeEjercicioFisico = _context.EjerciciosFisicos.Where(e => e.IdEjercicioFisico == IdEjercicioFisico).Count();
                    {
                        ejercicioFisicoEditar.TipoEjercicioID = TipoEjercicioID;
                        ejercicioFisicoEditar.LugarID = LugarID;
                        ejercicioFisicoEditar.EventoID = EventoID;
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

    public JsonResult GetEstadoEmocional()
    {
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