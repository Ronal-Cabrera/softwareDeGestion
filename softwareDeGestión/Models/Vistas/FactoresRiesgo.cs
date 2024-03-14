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
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? HistorialFamiliarDiabetes { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? ActividadFisica { get; set; }
    

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? HabitosAlimenticios { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? NivelesEstres { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? OtrosFactores { get; set; }
    }
}
