using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class Ejercicio
{
    [Key]
    public int Id { get; set; }

    public string? Nombre { get; set; }
}
