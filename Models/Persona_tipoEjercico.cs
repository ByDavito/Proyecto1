using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class Persona_tipoEjercicio
{
    [Key]
    public int relacionID { get; set; }
    public int PersonaID { get; set; }
    public int TipoEjercicioID { get; set; }
    public bool Eliminado { get; set; }

    
    public  TipoEjercicio TipoEjercicio { get; set; }
    public  Persona Persona { get; set; }
}
