using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;


    public class VistaTipoEjercicioFisico
    {
        public int TipoEjercicioID { get; set; }
        public string? Nombre { get; set; }

        public decimal CantidadMinutos { get; set; }

    }

