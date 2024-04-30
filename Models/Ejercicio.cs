using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class Ejercicio
{
    [Key]
    public int IdEjercicio { get; set; }

    public string? Nombre { get; set; }
    public bool Eliminado { get; set; }
    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}
