using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Proyecto1.Models;

namespace Proyecto1.Models
{
    public class EjercicioFisico{
        [Key]
        public int IdEjercicioFisico { get; set; }
        public int? PersonaID { get; set; }
        public int? EventoID { get; set; }
        public int TipoEjercicioID { get; set; }
        public int LugarID { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public EstadoEmocional EstadoInicio { get; set; }
        public EstadoEmocional EstadoFin { get; set; }
        public string? Observaciones { get; set; }
        public virtual TipoEjercicio TipoEjercicio { get; set; }
         public virtual Lugar Lugar { get; set; }
         public virtual Evento Evento { get; set; }
         public virtual Persona Persona { get; set; }
        [NotMapped]
        public TimeSpan Intervalo { get {return Fin - Inicio;} }
    }

    public class VistaInforme{
        public int TipoEjercicioID { get; set; }
        public string? TipoEjercicioNombre { get; set; }
        public List<VistaEjercicios> Ejercicios { get; set; }
    }


    public class VistaEjercicios{
        public int IdEjercicioFisico { get; set; }
        public int TipoEjercicioID { get; set; }
        public string? EjercicioNombre { get; set; }
        public string? Lugar { get; set; }
        public string? Evento { get; set; }
        public string InicioString { get; set; }
        public string FinString { get; set; }
        public string? EstadoInicio { get; set; }
        public string? EstadoFin { get; set; }
        public string? Observaciones { get; set; }
        public string? Duracion { get; set; }
        public string? Kcal { get; set; }
    }

    public class Lugarvista{
        public int IdLugar { get; set; }
        public string Nombre { get; set; }
        public List<LugarEjercicios> LugarEjercicios { get; set; }
    }

       public class LugarEjercicios{
        public int IdEjercicioFisico { get; set; }
        
        public int TipoEjercicioID { get; set; }
        public string? EjercicioNombre { get; set; }
        public string InicioString { get; set; }
        public string FinString { get; set; }
        public string? Observaciones { get; set; }
        public string? Duracion { get; set; }
        public string? Kcal { get; set; }
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

public class VistaEjerciciosGeneral{
     public string InicioString { get; set; }
        public string FinString { get; set; }
        public string? EstadoInicio { get; set; }
        public string? EstadoFin { get; set; }
        public string? Observaciones { get; set; }
        public string? Kcal { get; set; }
       
}
}
