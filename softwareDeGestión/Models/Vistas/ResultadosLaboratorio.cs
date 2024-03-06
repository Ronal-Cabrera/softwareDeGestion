using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class ResultadosLaboratorio
    {
        public ResultadosLaboratorio()
        {
        }

        public int? ResultadoID { get; set; }

        public string? FechaControl { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        public float? NivelGlucosa { get; set; }


        [Required(ErrorMessage = "Dato es requerido")]
        public string? Comentarios { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        public string? OtrosResultados { get; set; }
    }
}
