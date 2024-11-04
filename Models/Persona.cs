using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models;

public class Persona
{
    [Key]
    public int PersonaID { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public float Altura { get; set; }
    [DisplayFormat(DataFormatString = "{0:F3}")]
    public float Peso { get; set; }
    public Sexo Sexo { get; set; }
    public string? Nombre { get; set; }
    public string? CuentaID { get; set; }

    public virtual ICollection<Lugar> Lugares { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; }

    public virtual ICollection<TipoEjercicio> TipoEjercicios { get; set; }

    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}
public enum Sexo
{
    Masculino =1,
    Femenino,
    Otro

}
