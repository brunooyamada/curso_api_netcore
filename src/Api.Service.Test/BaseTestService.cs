using Api.Service.Test.AutoMapper;
using AutoMapper;
using CrossCutting.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Api.Service.Test;

public abstract class BaseTestService
{
    public IMapper Mapper { get; set; }
    protected IMapper _mapper;

    public BaseTestService()
    {
        Mapper = new AutoMapperFixture().GetMapper();
    }
}

public class AutoMapperFixture : IDisposable
{
    public IMapper GetMapper()
    {
        var loggerFactory = NullLoggerFactory.Instance;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(typeof(DtoToModelProfile));
            cfg.AddProfile(typeof(ModelToEntityProfile));
            cfg.AddProfile(typeof(EntityToDtoProfile));
        }, loggerFactory);

        return config.CreateMapper();
    }

    public void Dispose()
    {
        
    }
}
