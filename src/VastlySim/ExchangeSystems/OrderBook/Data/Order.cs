namespace VastlySim.ExchangeSystems.OrderBook;

public enum OrderType
{
    Buy,
    Sell
}
public class Order
{
    public Guid id;
    public OrderType Type;
    public double Price;
    public int Quantity;        
    public DateTime Timestamp;
    public readonly double OrderLifetimeSeconds;

    public Order(OrderType type, double price, int quantity, double orderLifetimeSeconds)
    {
        id = Guid.NewGuid();
        Type = type;
        Price = price;
        Quantity = quantity;
        Timestamp = DateTime.UtcNow;
        OrderLifetimeSeconds = orderLifetimeSeconds;
    }
    public override string ToString()
    {
        return $"{id} : [{Price} | Q:{Quantity}]";
    }
    public virtual void Expire(){}
}