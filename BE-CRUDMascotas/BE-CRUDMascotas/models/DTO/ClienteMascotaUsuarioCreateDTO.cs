using System.ComponentModel.DataAnnotations;

namespace BE_CRUDMascotas.models.DTO
{
    public class ClienteMascotaUsuarioCreateDTO
    {
        // Datos de la persona
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede superar los 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(20, ErrorMessage = "El apellido no puede superar los 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios")]
        public string Apellido { get; set; } = "";

        [Range(1, 100, ErrorMessage = "La edad debe estar entre 1 y 100")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio")]
        [RegularExpression(@"^(\+54|0)?[0-9]{8,15}$", ErrorMessage = "Ingrese un numero de telefono valido")]
        public string Telefono { get; set; } = "";

        // Datos de acceso
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El usuario no puede superar los 50 caracteres")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "La contrasena es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contrasena debe tener al menos 6 caracteres")]
        public string Password { get; set; } = "";

        // Datos de la mascota inicial
        [Required(ErrorMessage = "El nombre de la mascota es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de la mascota no puede superar los 50 caracteres")]
        public string NombreMascota { get; set; } = "";

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(30, ErrorMessage = "La raza no puede superar los 30 caracteres")]
        public string Raza { get; set; } = "";

        [StringLength(30, ErrorMessage = "El color no puede superar los 30 caracteres")]
        public string Color { get; set; } = "";

        [Range(0, 25, ErrorMessage = "La edad de la mascota debe estar entre 0 y 25")]
        public int EdadMascota { get; set; }

        [Range(0.1, 100, ErrorMessage = "El peso debe estar entre 0.1 y 100 kg")]
        public float Peso { get; set; }
    }
}
