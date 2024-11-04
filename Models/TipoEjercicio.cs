using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class TipoEjercicio
{
    [Key]
    public int TipoEjercicioID { get; set; }

    public string? Nombre { get; set; }
    public bool Eliminado { get; set; }
    public int? PersonaID { get; set; }

    public virtual Persona Persona { get; set; }
    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}

 public class VistaTipoEjercicio
    {
        public int TipoEjercicioID { get; set; }
        public string? Nombre { get; set; }
        public List<VistaEjerciciosGeneral> VistaEjerciciosGeneral  { get; set; }
    }