namespace Cafestore.Domain.Extensions;

public static class QueryableExtension 
{
    public static IQueryable<OrderEntity> Filter(this IQueryable<OrderEntity> orderEntities, OrderFilter filter)
    {
        if(filter is null)
        {
            return orderEntities;
        }

        return orderEntities
            .Where(e => filter.OrderStatus == null || e.OrderStatus == filter.OrderStatus.Value);
    }
}
