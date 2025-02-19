using AutoMapper;

namespace Cinema.Web
{
    public static class WebDependencyInjection
    {
        public static IServiceCollection AddWebAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new WebMappingProfile()));
            mapperConfig.AssertConfigurationIsValid();

            services.AddAutoMapper(typeof(WebMappingProfile));

            return services;
        }
    }
}
