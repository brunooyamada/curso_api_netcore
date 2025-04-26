using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ReverseMap();

            CreateMap<UfEntity, UfModel>()
                .ReverseMap();

            CreateMap<MunicipioEntity, MunicipioModel>()
                .ReverseMap();

            CreateMap<CepEntity, CepModel>()
                .ReverseMap();
        }
    }
}
