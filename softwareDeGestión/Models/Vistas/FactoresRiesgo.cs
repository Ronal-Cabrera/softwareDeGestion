using System.ComponentModel.DataAnnotations;
namespace softwareDeGestión.Models.Vistas
{
    public class FactoresRiesgo
    {
        public FactoresRiesgo()
        {
        }

        public int? FactorID { get; set; }
        public int? PacienteID { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        public string? HistorialFamiliarDiabetes { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        public string? ActividadFisica { get; set; }


        [Required(ErrorMessage = "Dato es requerido")]
        public string? HabitosAlimenticios { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        public string? NivelesEstres { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        public string? OtrosFactores { get; set; }
    }
}
