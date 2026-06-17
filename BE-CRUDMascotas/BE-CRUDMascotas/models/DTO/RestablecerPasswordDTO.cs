using System.ComponentModel.DataAnnotations;

namespace BE_CRUDMascotas.models.DTO
{
    public class RestablecerPasswordDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La contraseña temporal es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña temporal debe tener al menos 6 caracteres")]
        public string PasswordTemporal { get; set; } = "";
    }
}
