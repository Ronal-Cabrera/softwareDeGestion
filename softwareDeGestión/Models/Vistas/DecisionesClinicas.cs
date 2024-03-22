using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class DecisionesClinicas
    {
        public DecisionesClinicas()
        {
        }

        public int? DecisionID { get; set; }
        public string? PacienteID { get; set; }
        public string? name { get; set; }

        public string? FechaDecision { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? TratamientosRecomendados { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? MedicamentosRecetados { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? CambiosEstiloVida { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? SeguimientoProximasCitas { get; set; }
    }
}
