namespace Cinema.WebApi
{
    public static class APIDependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
