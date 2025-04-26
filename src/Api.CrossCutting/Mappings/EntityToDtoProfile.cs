using AutoMapper;
using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Domain.Dtos.Uf;
using Domain.Dtos.User;
using Domain.Entities;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            #region User
            CreateMap<UserDto, UserEntity>()
                .ReverseMap();

            CreateMap<UserDtoCreateResult, UserEntity>()
                .ReverseMap();

            CreateMap<UserDtoUpdateResult, UserEntity>()
                .ReverseMap();
            #endregion

            #region Uf
            CreateMap<UfDto, UfEntity>()
                .ReverseMap();

            CreateMap<MunicipioDto, MunicipioEntity>()
                .ReverseMap();
            #endregion

            #region Municipio
            CreateMap<MunicipioDtoCompleto, MunicipioEntity>()
                .ReverseMap();

            CreateMap<MunicipioDtoCreateResult, MunicipioEntity>()
                .ReverseMap();

            CreateMap<MunicipioDtoUpdateResult, MunicipioEntity>()
                .ReverseMap();
            #endregion

            #region Cep
            CreateMap<CepDto, CepEntity>()
                .ReverseMap();

            CreateMap<CepDtoCreateResult, CepEntity>()
                .ReverseMap();

            CreateMap<CepDtoUpdateResult, CepEntity>()
                .ReverseMap();
            #endregion
        }
    }
}
