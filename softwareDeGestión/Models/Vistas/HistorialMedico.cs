using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class HistorialMedico
    {
        public HistorialMedico()
        {
        }

        public int? HistorialID { get; set; }

        public string? FechaConsulta { get; set; }

        [Required(ErrorMessage = "Peso es requerido")]
        [Range(1, 300, ErrorMessage = "Rago de Peso, 1 a 300")]
        public float? Peso { get; set; }

        [Required(ErrorMessage = "Altura es requerido")]
        [Range(1, 3, ErrorMessage = "Rago de Altura, 1 a 3")]
        public float? Altura { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 10.")]
        public string? TipoDiabetes { get; set; }


        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 20.")]
        public string? PresionArterial { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? AntecedentesFamiliares { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? OtrosAntecedentes { get; set; }
    }
}
