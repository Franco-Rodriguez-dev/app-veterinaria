using AutoMapper;
using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Profiles
{
    public class VeterinariaProfile : Profile
    {
        public VeterinariaProfile()
        {
            // DTO -> Entity
            CreateMap<PersonaMascotaCreateDTO, Personas>();
            CreateMap<MascotaCreateDTO, Mascota>();

            // Entity -> DTO
            CreateMap<Personas, PersonaMascotaListDTO>();
            CreateMap<Mascota, PersonaMascotaListDTO>();
        }
    }
}
