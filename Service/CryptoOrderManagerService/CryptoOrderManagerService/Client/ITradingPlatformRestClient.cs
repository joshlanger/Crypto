using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoOrderManagerService.Client
{
    public interface ITradingPlatformRestClient
    {
        Task<RestResponse> GetAddresses(string resource, string apiKey, string apiSecret);
        Task<RestResponse> GetMarkets(string resource);
    }
}
