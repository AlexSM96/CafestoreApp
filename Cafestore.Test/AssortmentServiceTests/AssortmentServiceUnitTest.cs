using Cafestore.Domain.Abstractions;
using Cafestore.Domain.Entities.Assortment;
using Cafestore.Domain.Exceptions;
using Cafestore.Domain.Models.AssortmentModels;
using Cafestore.Domain.Services;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Cafestore.Test.AssortmentServiceTests;


public class AssortmentServiceUnitTest
{
    private readonly Mock<ICafestoreDbContext> _mockContext;
    private readonly IAssortmentService _service;

    public AssortmentServiceUnitTest()
    {
        _mockContext = new Mock<ICafestoreDbContext>();
        _service = new AssortmentService(_mockContext.Object);
    }

    [Fact]
    public async Task GetAssortments_ReturnAssortmentItems()
    {
        var assortmentItems = new List<AssortmentItemEntity>()
        {
            new() { Name = "Item" },
            new() { Name = "Item2" },
            new() { Name = "Item3" }
        };

        _mockContext
            .Setup(c => c.AssortmentItems)
            .ReturnsDbSet(assortmentItems);

        var result = await _service.GetAssortments();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task AddAssortmentItem_ItemNameIsNull_ThrowsInvalidOperationException()
    {
        var newItem = new CreateAssortmentItem { Name = string.Empty };

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddAssortmentItem(newItem));
        Assert.Equal("Название продукта из ассортимента не должно быть пустым", exception.Message);
    }

    [Fact]
    public async Task AddAssortmentItem_ItemAlreadyExists_ThrowsAlreadyExistsException()
    {

        var newItem = new CreateAssortmentItem { Name = "ExistingItem" };
        var assortmentItems = new List<AssortmentItemEntity>
        {
            new() { Name = "ExistingItem" }
        };

        _mockContext
            .Setup(c => c.AssortmentItems)
            .ReturnsDbSet(assortmentItems);

        var exception = await Assert.ThrowsAsync<AlreadyExistsException>(() => _service.AddAssortmentItem(newItem));
        Assert.Equal($"Сущность с названием: {newItem.Name} уже существует", exception.Message);
    }

    [Fact]
    public async Task DeleteAssortmentItem_ItemDoesNotExist_ThrowsEntityNotFoundException()
    {
        var itemId = 1;
        _mockContext
            .Setup(c => c.AssortmentItems)
            .ReturnsDbSet([]);

        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.DeleteAssortmentItem(itemId));
        Assert.Equal("Сущность AssortmentItemEntity с Id 1 не существует", exception.Message);
    }
}
