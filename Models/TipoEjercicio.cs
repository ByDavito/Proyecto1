using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class TipoEjercicio
{
    [Key]
    public int TipoEjercicioID { get; set; }

    public string? Nombre { get; set; }
    public float MET { get; set; }

    public virtual ICollection<Persona_tipoEjercicio> Persona_TipoEjercicios { get; set; }
    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}

 public class VistaTipoEjercicio
    {
        public int TipoEjercicioID { get; set; }
        public string? Nombre { get; set; }
        public List<VistaEjerciciosGeneral> VistaEjerciciosGeneral  { get; set; }
    }