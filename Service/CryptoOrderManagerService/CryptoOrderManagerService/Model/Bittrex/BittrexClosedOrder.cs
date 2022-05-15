using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoOrderManagerService.Model.Bittrex
{
    public class BittrexClosedOrder
    {
        public string id { get; set; }
        public string marketSymbol { get; set; }
        public string direction { get; set; }
        public string type { get; set; }
        public string quantity { get; set; }
        public string limit { get; set; }
        public string ceiling { get; set; }
        public string timeInForce { get; set; }
        public string clientOrderId { get; set; }
        public string fillQuantity { get; set; }
        public string commission { get; set; }
        public string proceeds { get; set; }
        public string status { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string closedAt { get; set; }
        public OrderToCancel orderToCancel { get; set; }
    }
}
