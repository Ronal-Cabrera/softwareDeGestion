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
        public string? FechaDecision { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? TratamientosRecomendados { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? MedicamentosRecetados { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? CambiosEstiloVida { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? SeguimientoProximasCitas { get; set; }
    }
}
