using System.ComponentModel.DataAnnotations;

namespace BE_CRUDMascotas.models.DTO
{
    public class CambiarPasswordDTO
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public string PasswordActual { get; set; } = "";

        [Required(ErrorMessage = "La contraseña nueva es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña nueva debe tener al menos 6 caracteres")]
        public string PasswordNueva { get; set; } = "";
    }
}
