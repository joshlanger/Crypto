using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoOrderManagerService.Client
{
    public interface ITradingPlatformRestClient
    {
        Task<RestResponse> GetClosedOrders(string resource, string apiKey, string apiSecret);
        Task<RestResponse> GetOpenOrders(string resource, string apiKey, string apiSecret);
    }
}
