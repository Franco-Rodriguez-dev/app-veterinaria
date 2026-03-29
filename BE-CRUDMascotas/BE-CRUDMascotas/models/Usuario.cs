using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_CRUDMascotas.models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public int RolId {  get; set; }
        public Rol Rol { get; set; } = null!;  

        public int? PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public Personas Persona { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

    }
}
