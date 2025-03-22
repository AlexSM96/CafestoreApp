namespace Cafestore.Persistance.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCafestoreDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<CafestoreDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("CafestoreDb")))
            .AddScoped<ICafestoreDbContext, CafestoreDbContext>();
    }
}
