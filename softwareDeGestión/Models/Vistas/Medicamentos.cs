using System.ComponentModel.DataAnnotations;
namespace softwareDeGestión.Models.Vistas
{
    public class Medicamentos
    {
        public Medicamentos() { }

        public int? MedicamentoID { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 100.")]
        public string? NombreMedicamento { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? DescripcionMedicamento { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? InstruccionesUso { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public string? EfectosSecundarios { get; set; }
    }
}
