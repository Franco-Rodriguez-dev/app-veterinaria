using BE_CRUDMascotas.models;
using System.ComponentModel.DataAnnotations;

namespace BE_CRUDMascotas.models.DTO
{
    public class HistorialMascotaUpdateDTO
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TipoHistorialMascota Tipo { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        [StringLength(80, ErrorMessage = "El titulo no puede superar los 80 caracteres")]
        public string Titulo { get; set; } = "";

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripcion no puede superar los 500 caracteres")]
        public string Descripcion { get; set; } = "";

        [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres")]
        public string Observaciones { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "El precio debe ser mayor o igual a 0")]
        public decimal? Precio { get; set; }

        public DateTime? ProximaVisita { get; set; }
    }
}
