using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Proyecto1.Models;

namespace Proyecto1.Models
{
    public class EjercicioFisico{
        [Key]
        public int IdEjercicioFisico { get; set; }
        public int IdEjercicio { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public EstadoEmocional EstadoInicio { get; set; }
        public EstadoEmocional EstadoFin { get; set; }
        public string? Observaciones { get; set; }
        public virtual Ejercicio Ejercicio { get; set; }
    }
    public enum EstadoEmocional{
        Feliz =1,
        Triste,
        Enojado,
        Ansioso,
        Estresado,
        Relajado,
        Aburrido,
        Emocionado,
        Agobiado,
        Confundido,
        Optimista,
        Pesimista,
        Motivado,
        Cansado,
        Euforico,
        Agitado,
        Satisfecho,
        Desanimado,
    }

}