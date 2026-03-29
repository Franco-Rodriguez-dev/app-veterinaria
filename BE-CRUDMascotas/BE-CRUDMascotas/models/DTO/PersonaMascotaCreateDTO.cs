namespace BE_CRUDMascotas.models.DTO
{
    public class PersonaMascotaCreateDTO
    {
        // Persona
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; } = string.Empty;


        // Mascota
        public MascotaCreateDTO Mascota { get; set; }
    }

    public class MascotaCreateDTO
    {
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public string Color { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
    }
}
