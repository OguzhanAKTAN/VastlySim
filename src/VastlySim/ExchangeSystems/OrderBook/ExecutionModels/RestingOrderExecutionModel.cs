using VastlySim.ExchangeSystems.OrderBook.Core;
using VastlySim.ExchangeSystems.OrderBook.Data;
namespace VastlySim.ExchangeSystems.OrderBook.ExecutionModels;

public class RestingOrderExecutionModel : IPriceExecutionModel
{
    public double GetTradePrice(Order bid, Order ask)
    {
        // Older order is resting
        return bid.Timestamp <= ask.Timestamp ? bid.Price : ask.Price;
    }
}