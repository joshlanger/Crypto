using CryptoOrderManagerService.Client;
using MassTransit;
using DomainModel.Message;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoOrderManagerService
{
    public class OrderInfoQueueConsumer : IConsumer<IGetOrderInfoCommand>
    {
        public async Task Consume(ConsumeContext<IGetOrderInfoCommand> context)
        {
            //Need to set up DB access.  Need project for data layer and domain model

            ITradingPlatformRestClient RestClient = new BittrexRestClient(@"https://api.bittrex.com/v3/");
            var test = await RestClient.GetOpenOrders("orders/open", "", "");
            var response = await RestClient.GetClosedOrders("orders/closed", "", "");

            throw new NotImplementedException();
        }
    }
}
