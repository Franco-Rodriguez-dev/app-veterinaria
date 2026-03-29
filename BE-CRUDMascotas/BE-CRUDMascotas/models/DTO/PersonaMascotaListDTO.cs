
namespace BE_CRUDMascotas.models.DTO
{
    public class PersonaMascotaListDTO
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        public int MascotaId { get; set; }
        public string NombreMascota { get; set; }
        public string Raza { get; set; }
        public float Peso { get; set; }

    }
}
