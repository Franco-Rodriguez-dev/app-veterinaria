//todo esto esta en el 2:00:00 del video
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_CRUDMascotas.models
{
    public class Mascota
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(30, ErrorMessage = "La raza no puede superar los 30 caracteres")]
        public string Raza { get; set; } = "";

        [StringLength(30, ErrorMessage = "El color no puede superar los 30 caracteres")]
        public string Color { get; set; } = "";

        [Range(0, 25, ErrorMessage = "La edad debe estar entre 0 y 25 años")]
        public int Edad { get; set; }

        [Range(0.1, 100, ErrorMessage = "El peso debe estar entre 0.1 y 100 kg")]
        public float Peso { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // ===================================================
        // 🔹 Relación con Persona (1 Persona -> muchas Mascotas)
        // ===================================================
        [ForeignKey("Persona")]
        public int PersonaId { get; set; } // FK en la tabla Mascota

        public Personas Persona { get; set; } = null!; // Propiedad de navegación
    }
}