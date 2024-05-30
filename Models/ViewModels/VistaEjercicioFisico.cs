using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class VistaEjercicioFisico
    {     
        public string? Mes { get; set; }
        public int? Dia { get; set; }
        public int CantidadMinutos { get; set; }
    }
