using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoOrderManagerService.Model.Bittrex
{
    public class OrderToCancel
    {
        public string type { get; set; }
        public string id { get; set; }
    }
}
