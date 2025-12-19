using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Mapper;

namespace Common.Configuration;

public static class MapperConfig
{
    public static void ConfiguerServices(IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperProfile(
            //provider.GetService(IUserWebClient);
            ));
        }).CreateMapper());
    }
}
