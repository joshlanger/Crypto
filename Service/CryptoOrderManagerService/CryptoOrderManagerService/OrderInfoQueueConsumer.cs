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

            ITradingPlatformRestClient RestClient = new BittrexRestClient();
            var response = await RestClient.Authenticate("https:////api.bittrex.com//v3//authentication", "");

            var n = 1;

            throw new NotImplementedException();
        }
    }
}
