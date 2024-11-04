using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class Lugar
{
    [Key]
    public int LugarID { get; set; }

    public string? Nombre { get; set; }
    public bool Eliminado { get; set; }
    public int? PersonaID { get; set; }
    public virtual Persona? Persona { get; set; }
    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}

public class VistaLugar
{
    public int LugarID { get; set; }
    public string? Nombre { get; set; }
    public List<VistaTipoEjercicio> vistaTipoEjercicios { get; set; }
}