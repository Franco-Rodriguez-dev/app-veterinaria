using AutoMapper;
using BE_CRUDMascotas.models;
using BE_CRUDMascotas.models.DTO;

namespace BE_CRUDMascotas.models.Profiles
{
    public class MascotaProfile : Profile
    {
        public MascotaProfile() 
        {
            CreateMap<Mascota , MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }




    }
}
