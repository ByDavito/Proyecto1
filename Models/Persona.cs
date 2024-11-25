using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
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
    public DateTime FechaNacimiento { get; set; }
    public string? CuentaID { get; set; }

    public virtual ICollection<Lugar> Lugares { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; }

    public virtual ICollection<Persona_tipoEjercicio> Persona_TipoEjercicos { get; set; }

    public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; }
}

public class ApplicationUser : IdentityUser
{
}

public enum Sexo
{
    Masculino =1,
    Femenino,
    Otro

}

public class VistaCuenta{
    public string Nombre { get; set; }
    public string Sexo { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }
    public string Email { get; set; }
    public string Rol { get; set; }
    public string FechaNacimiento { get; set; }
    public string Edad { get; set; }
    public string imc { get; set; }
    public string tmb { get; set; }
}
