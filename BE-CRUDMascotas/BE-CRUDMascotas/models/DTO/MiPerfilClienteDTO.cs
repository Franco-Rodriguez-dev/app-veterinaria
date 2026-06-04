namespace BE_CRUDMascotas.models.DTO
{
    public class MiPerfilClienteDTO
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public int Edad { get; set; }
        public Sexo Sexo { get; set; }
        public string Telefono { get; set; } = "";
        public List<MiPerfilMascotaDTO> Mascotas { get; set; } = new();
    }
}
