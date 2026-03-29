using System.ComponentModel.DataAnnotations;

namespace BE_CRUDMascotas.models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        // Relación 1:N → Un rol puede tener muchos usuarios
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
