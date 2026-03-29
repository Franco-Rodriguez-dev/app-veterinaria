using System.ComponentModel.DataAnnotations;

namespace BE_CRUDMascotas.models.DTO
{
    public class PersonaCreateDTO //Para crear una persona (sin Id, sin lista de mascotas)
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede superar los 20 Caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [StringLength(20, ErrorMessage = "El Apellido no puede superar los 20 Caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El Apellido solo puede contener letras y espacios")]
        public string Apellido { get; set; } = "";

        [Range(1, 100, ErrorMessage = "La edad debe estar entre 1 y 100")]
        public int Edad { get; set; }

        [Required]
        [StringLength(20)]
        public string Sexo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^(\+54|0)?[0-9]{8,15}$", ErrorMessage = "Ingrese un número de teléfono válido (solo dígitos, puede incluir +54 o 0)")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = "";


    }

    public class PersonaUpdateDTO : PersonaCreateDTO { } //Para editar (hereda de Create)

    public class PersonaListsDTO //Para listar (sin mascotas, solo contadores o info básica)
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public int Edad { get; set; }
        public string Telefono { get; set; } = "";
        public int CantMascotas { get; set; }

    }


   
    public class PersonaDetalleDTO  // Para detalle (incluye mascotas) Para mostrar una persona con su lista de mascotas resumida
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public int Edad { get; set; }
        public Sexo Sexo { get; set; }
        public string Telefono { get; set; } = "";
        public List<MascotaMiniDTO> ListaMascotas { get; set; } = new(); //esta es los mini datos de la class MascotaMiniDTO
    }

    public class MascotaMiniDTO //Usado solo dentro de PersonaDetalleDTO (para mostrar resumen de mascotas)
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = "";
        public string Raza { get; set; } = "";
    }


}
