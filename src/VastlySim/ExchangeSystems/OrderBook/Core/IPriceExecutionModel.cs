using VastlySim.ExchangeSystems.OrderBook.Data;
namespace VastlySim.ExchangeSystems.OrderBook.Core;

public interface IPriceExecutionModel
{
    double GetTradePrice(Order bid, Order ask);
}