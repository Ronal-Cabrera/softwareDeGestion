using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class ResultadosLaboratorio
    {
        public ResultadosLaboratorio()
        {
        }

        public int? ResultadoID { get; set; }

        public int? PacienteID { get; set; }
       
        public string? name { get; set; }

        public string? FechaControl { get; set; }


        [Required(ErrorMessage = "Dato es requerido")]
        [Range(1, 150, ErrorMessage = "Valor no aceptable")]
        public float? NivelGlucosa { get; set; }


        [Required(ErrorMessage = "Dato es requerido")]
        public string? Comentarios { get; set; }

        [Required(ErrorMessage = "Dato es requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 255.")]
        public string? OtrosResultados { get; set; }


        public IFormFile? FileUpload { get; set; }
    }
}
