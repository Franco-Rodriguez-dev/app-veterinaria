using AutoMapper;
using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Profiles
{
    public class PersonaProfile: Profile
    {
        public PersonaProfile() {

            // Entity -> DTO


            //src → source (origen): es la entidad original (Personas, que viene de la base de datos).
            // dest → destination(destino): es el objeto a crear(PersonaDTO, el que se envía al front).

            CreateMap<Personas, PersonaListsDTO>()//transforma tu entidad completa en un DTO resumido (para listar).
               .ForMember(dest => dest.CantMascotas, opt => opt.MapFrom(src => src.ListMascotas != null ? src.ListMascotas.Count : 0));
               //.ForMember(...): cuenta cuántas mascotas tiene la persona.

            CreateMap<Personas, PersonaDetalleDTO>();//convierte una persona en un DTO de detalle (incluye lista de mascotas).
            CreateMap<Mascota,MascotaMiniDTO>();//convierte cada mascota a su versión mini.

            // DTO -> Entity

            CreateMap<PersonaCreateDTO, Personas>();//para crear una persona desde el front.
            CreateMap<PersonaUpdateDTO, Personas>();// para Actualizar.











        }


    }
}
