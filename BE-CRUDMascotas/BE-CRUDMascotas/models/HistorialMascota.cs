using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_CRUDMascotas.models
{
    public class HistorialMascota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MascotaId { get; set; }

        [ForeignKey("MascotaId")]
        public Mascota Mascota { get; set; } = null!;

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

        public int? CreadoPorUsuarioId { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
