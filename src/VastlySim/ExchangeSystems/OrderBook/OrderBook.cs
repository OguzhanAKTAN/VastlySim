namespace VastlySim.ExchangeSystems.OrderBook;

    public class OrderBook
    {
        private readonly IPriceExecutionModel _executionModel;
        private List<Order> _bids = new();
        private List<Order> _asks = new();
        
        public double LastTradedPrice { get; private set; }
        
        public OrderBook(IPriceExecutionModel executionModel)
        {
            _executionModel = executionModel;
        }
        
        public void AddOrder(Order order)
        {
            if (order.Type == OrderType.Buy)
                _bids.Add(order);
            else
                _asks.Add(order);

            SortOrders();
        }

        public void Tick()
        {
            CleanupExpiredOrders();
            MatchOrders();
        }
        
        private void SortOrders()
        {
            _bids = _bids.OrderByDescending(o => o.Price).ThenBy(o => o.Timestamp).ToList();
            _asks = _asks.OrderBy(o => o.Price).ThenBy(o => o.Timestamp).ToList();
        }
        //TODO: this needs to be fast and efficient
        private void MatchOrders()
        {
            while (_bids.Count > 0 && _asks.Count > 0 && _bids[0].Price >= _asks[0].Price)
            {
                var bid = _bids[0];
                var ask = _asks[0];
                var tradeQty = Math.Min(bid.Quantity, ask.Quantity);
                
                //Calculate market price based on model
                LastTradedPrice = _executionModel.GetTradePrice(ask, bid);

                bid.Quantity -= tradeQty;
                ask.Quantity -= tradeQty;
                 
                //leave orders if not filled yet
                if (bid.Quantity <= 0)
                {
                    _bids[0].Expire();
                    _bids.RemoveAt(0);
                }
                if (ask.Quantity <= 0)
                {
                    _asks[0].Expire();
                    _asks.RemoveAt(0);
                }
            }
        }
        
        public (double? bid, double? ask) GetTopPricePairOfBook()
        {
            double? bid = _bids.Count > 0 ? _bids[0].Price : null;
            double? ask = _asks.Count > 0 ? _asks[0].Price : null;
            return (bid, ask);
        }
        
        public (Order? bid, Order? ask) GetTopOrdersOfBook()
        {
            Order? topBid = _bids.Count > 0 ? _bids[0] : null;
            Order? topAsk = _asks.Count > 0 ? _asks[0] : null;
            return (topBid, topAsk);
        }
        
        private void CleanupExpiredOrders()
        { 
            DateTime now = DateTime.UtcNow;
            var expiredBids = _bids.Where(o => (now - o.Timestamp).TotalSeconds > o.OrderLifetimeSeconds).ToList();
            foreach (var bid in expiredBids)
            {
                bid.Expire();
                _bids.Remove(bid);
            }

            var expiredAsks = _asks.Where(o => (now - o.Timestamp).TotalSeconds > o.OrderLifetimeSeconds).ToList();
            foreach (var ask in expiredAsks)
            {
                ask.Expire();
                _asks.Remove(ask);
            }
        }
    }