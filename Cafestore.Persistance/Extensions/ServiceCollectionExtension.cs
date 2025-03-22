namespace Cafestore.Persistance.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCafestoreDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<CafestoreDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("CafestoreDb")))
            .AddScoped<ICafestoreDbContext, CafestoreDbContext>();
    }

    public static IServiceProvider CreateDbIfNotExists(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CafestoreDbContext>();
        context.Database.EnsureCreated();
        return serviceProvider;
    }
}
