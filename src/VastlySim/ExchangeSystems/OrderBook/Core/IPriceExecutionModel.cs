namespace VastlySim.ExchangeSystems.OrderBook;

public interface IPriceExecutionModel
{
    double GetTradePrice(Order bid, Order ask);
}