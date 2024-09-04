using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class Lugar
{
    [Key]
    public int LugarID { get; set; }

    public string? Nombre { get; set; }
    public bool Eliminado { get; set; }
    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}
