namespace Cafestore.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IAssortmentService, AssortmentService>()
            .AddScoped<IOrderService, OrderService>();
    }
}
