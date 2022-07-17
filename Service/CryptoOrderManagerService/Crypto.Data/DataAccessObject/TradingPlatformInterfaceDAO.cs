using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Crypto.Data.DataAccessObjectInterface;

namespace Crypto.Data.DataAccessObject
{
    public class TradingPlatformInterfaceDAO : ITradingPlatformInterfaceDAO
    {
        CryptoContext CryptoContext;

        public async Task<TradingPlatformInterface> GetTradingPlatformInterfaceByTradingPlatformName(string platformName)
        {
            using(CryptoContext = new CryptoContext())
            {
                var result = await (from tpi in CryptoContext.TradingPlatformInterfaces
                                    join tp in CryptoContext.TradingPlatforms on tpi.TradingPlatformId equals tp.TradingPlatformId
                                    where tp.TradingPlatformName == platformName
                                    select tpi).FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task<TradingPlatformInterface> GetTradingPlatformInterfaceByTradingPlatformID(int tradingPlatformID)
        {
            using (CryptoContext = new CryptoContext())
            {
                var result = await (from tpi in CryptoContext.TradingPlatformInterfaces
                                   where tpi.TradingPlatformId == tradingPlatformID
                                   select tpi).FirstOrDefaultAsync();

                return result;
            }
        }
    }
}
