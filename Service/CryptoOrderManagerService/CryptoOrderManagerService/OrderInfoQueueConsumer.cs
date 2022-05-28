using CryptoOrderManagerService.Client;
using MassTransit;
using DomainModel.Message;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Crypto.Data.DataAccessObjectInterface;

namespace CryptoOrderManagerService
{
    public class OrderInfoQueueConsumer : IConsumer<IGetOrderInfoCommand>
    {
        private readonly ITradingPlatformInterfaceDAO TradingPlatformInterfaceDAO;

        public OrderInfoQueueConsumer(ITradingPlatformInterfaceDAO tradingPlatformInterfaceDAO)
        {
            this.TradingPlatformInterfaceDAO = tradingPlatformInterfaceDAO;
        }

        public async Task Consume(ConsumeContext<IGetOrderInfoCommand> context)
        {
            var tradingPlatformID = context.Message.PlatformInterfaceID;
            TradingPlatformInterface tradingPlatformInterface = new TradingPlatformInterface();

            if(tradingPlatformID != null)
            {
                tradingPlatformInterface = await TradingPlatformInterfaceDAO.GetTradingPlatformInterfaceByTradingPlatformID((int)tradingPlatformID);
            }

            ITradingPlatformRestClient RestClient = new BittrexRestClient(tradingPlatformInterface.ApibaseUrl);
            var test = await RestClient.GetOpenOrders("orders/open", tradingPlatformInterface.Apikey, tradingPlatformInterface.Apisecret);
            var response = await RestClient.GetClosedOrders("orders/closed", tradingPlatformInterface.Apikey, tradingPlatformInterface.Apisecret);

            throw new NotImplementedException();
        }
    }
}
