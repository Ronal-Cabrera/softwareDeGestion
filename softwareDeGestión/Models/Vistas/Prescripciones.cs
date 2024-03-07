using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class Prescripciones
    {
        public Prescripciones()
        {
        }

        public int? PrescripcionID { get; set; }
        public int? PacienteID { get; set; }
        public int? MedicamentoID { get; set; }
        public string? NombreMedicamento { get; set; }
        public string? FechaPrescripcion { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? DosisPrescrita { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? DuracionPrescripcion { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? NotasAdicionales { get; set; }
    }
}
