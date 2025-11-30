using VastlySim.ExchangeSystems.OrderBook.Core;
using VastlySim.ExchangeSystems.OrderBook.Data;
namespace VastlySim.ExchangeSystems.OrderBook.ExecutionModels;

public class MidpointExecutionModel : IPriceExecutionModel
{
    public double GetTradePrice(Order bid, Order ask)
    {
        return Math.Round((bid.Price + ask.Price) / 2.0, 2);
    }
}