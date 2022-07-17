using System;
using System.Collections.Generic;

#nullable disable

namespace DomainModel
{
    public partial class TradingPlatformInterface
    {
        public TradingPlatformInterface()
        {
            TradingPlatformApiendpoints = new HashSet<TradingPlatformApiendpoint>();
        }

        public int TradingPlatformInterfaceId { get; set; }
        public int TradingPlatformId { get; set; }
        public string Apikey { get; set; }
        public string ApibaseUrl { get; set; }
        public string Apisecret { get; set; }

        public virtual TradingPlatform TradingPlatform { get; set; }
        public virtual ICollection<TradingPlatformApiendpoint> TradingPlatformApiendpoints { get; set; }
    }
}
