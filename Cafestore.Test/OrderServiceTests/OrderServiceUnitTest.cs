using Cafestore.Domain.Abstractions;
using Cafestore.Domain.Entities.Assortment;
using Cafestore.Domain.Entities.Order;
using Cafestore.Domain.Enums;
using Cafestore.Domain.Exceptions;
using Cafestore.Domain.Models.OrderModels;
using Cafestore.Domain.Services;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Cafestore.Test.OrderServiceTests;

public class OrderServiceUnitTest
{
    private readonly Mock<ICafestoreDbContext> _mockContext;
    private readonly IOrderService _orderService;

    public OrderServiceUnitTest()
    {
        _mockContext = new Mock<ICafestoreDbContext>();
        _orderService = new OrderService(_mockContext.Object);
    }

    [Fact]
    public async Task CreateOrder_EmptyOrderItems_ThrowException()
    {
        var orderDto = new CreateOrderDto() { ClientName = "All", OrderItems = [] };

        await Assert.ThrowsAsync<EmptyOrderException>(() => _orderService.CreateOrder(orderDto));
    }

    [Fact]
    public async Task UpdateOrder_NonExistentOrder_ThrowsException()
    {
        var orderId = 999;
        var patchDoc = new JsonPatchDocument<UpdateOrderDto>();
        _mockContext.Setup(x => x.Orders).ReturnsDbSet([]);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => _orderService.UpdateOrder(orderId, patchDoc));
    }

    [Fact]
    public async Task GetOrders_ReturnsOrders_WhenFilterIsApplied()
    {
        var orders = new List<OrderEntity>
        {
            new OrderEntity { Id = 1, ClientName = "Client1", OrderStatus = OrderStatus.Completed, PaymentType = PaymentType.Cash },
            new OrderEntity { Id = 2, ClientName = "Client2", OrderStatus = OrderStatus.Cancelled}
        };

        _mockContext.Setup(c => c.Orders)
            .ReturnsDbSet(orders);

        var filter = new OrderFilter() { OrderStatus = OrderStatus.Completed }; 

        var result = await _orderService.GetOrders(filter);

        Assert.NotNull(result);
        Assert.Equal(1, result?.Count());
    }

    [Fact]
    public async Task UpdateOrder_CompleteCancelledOrder_ThrowException()
    {
        long orderId = 2;
        var patchDoc = new JsonPatchDocument<UpdateOrderDto>();
        patchDoc.Replace(x => x.Status, OrderStatus.Completed);
        var assortmentItems = new List<AssortmentItemEntity>() { new() { Name = "Item1" } };
        var orders = new List<OrderEntity>()
        {
             new OrderEntity 
             { 
                 Id = orderId, 
                 ClientName = "Client2",
                 OrderStatus = OrderStatus.Cancelled,
                 OrderItems = assortmentItems
             }
        };

        _mockContext.Setup(c => c.AssortmentItems)
            .ReturnsDbSet(assortmentItems);
        _mockContext.Setup(c => c.Orders)
            .ReturnsDbSet(orders);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _orderService.UpdateOrder(orderId, patchDoc));
        Assert.Equal("Нельзя \"выполнить\" отменённый заказ", exception.Message);
    }
}
