using VastlySim.ExchangeSystems.OrderBook;
namespace VastlySimTests.ExchangeSystems.OrderBook.Data;

[TestFixture]
[TestOf(typeof(Order))]
public class OrderTest
{

    [Test]
    public void Constructor_ShouldInitializeOrderWithCorrectValues()
    {
        // Arrange
        var type = OrderType.Buy;
        var price = 100.5;
        var quantity = 10;
        var lifetime = 30.0;

        // Act
        var order = new Order(type, price, quantity, lifetime);

        // Assert
        Assert.That(order.id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(order.Type, Is.EqualTo(type));
        Assert.That(order.Price, Is.EqualTo(price));
        Assert.That(order.Quantity, Is.EqualTo(quantity));
        Assert.That(order.OrderLifetimeSeconds, Is.EqualTo(lifetime));
        Assert.That(order.Timestamp, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }
}