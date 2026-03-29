namespace BE_CRUDMascotas.models.DTO
{
    public class MascotaDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public string Color { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
    }
}
