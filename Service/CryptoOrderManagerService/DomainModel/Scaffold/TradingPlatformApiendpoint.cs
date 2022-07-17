using System;
using System.Collections.Generic;

#nullable disable

namespace DomainModel
{
    public partial class TradingPlatformApiendpoint
    {
        public int TradingPlatformEndpointId { get; set; }
        public int TradingPlatformInterfaceId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public virtual TradingPlatformInterface TradingPlatformInterface { get; set; }
    }
}
