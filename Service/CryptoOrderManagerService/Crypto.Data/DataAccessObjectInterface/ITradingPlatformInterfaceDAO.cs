using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Data.DataAccessObjectInterface
{
    public interface ITradingPlatformInterfaceDAO
    {
        Task<TradingPlatformInterface> GetTradingPlatformInterfaceByTradingPlatformName(string platformName);
        Task<TradingPlatformInterface> GetTradingPlatformInterfaceByTradingPlatformID(int tradingPlatformID);
    }
}
