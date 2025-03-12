using AutoMapper;

namespace Cinema.WebApi.Infrastructure
{
    public static class APIDependencyInjection
    {
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapperConfig.AssertConfigurationIsValid();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
