using System;
using System.Collections.Generic;

#nullable disable

namespace DomainModel
{
    public partial class TradingPlatform
    {
        public TradingPlatform()
        {
            TradingPlatformInterfaces = new HashSet<TradingPlatformInterface>();
        }

        public int TradingPlatformId { get; set; }
        public string TradingPlatformName { get; set; }

        public virtual ICollection<TradingPlatformInterface> TradingPlatformInterfaces { get; set; }
    }
}
