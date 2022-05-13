using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoOrderManagerService.Client
{
    public class BittrexRestClient : ITradingPlatformRestClient
    {
        public string BaseUrl { get; set; }
        public RestClient Client;

        public BittrexRestClient(string baseUrl)
        {
            BaseUrl = baseUrl;
            Client = new RestClient(baseUrl);
        }

        public async Task<RestResponse> GetAddresses(string resource, string apiKey, string apiSecret)
        {
            var unixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            var bodyHash = GetRequestBodyContentHash("");

            var signature = unixTimeStamp + BaseUrl + resource + "GET" + bodyHash;

            var signed = CreateSignature(apiSecret, signature);

            RestRequest request = new RestRequest(resource)
                .AddHeader("Api-Key", apiKey)
                .AddHeader("Api-Timestamp", unixTimeStamp)
                .AddHeader("Api-Content-Hash", bodyHash)
                .AddHeader("Api-Signature", signed)
                .AddHeader("Accept", "application/json")
                .AddHeader("Content-Type", "application/json");

            var response = await Client.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> GetMarkets(string resource)
        {

            RestRequest request = new RestRequest(resource)
                .AddHeader("Accept", "application/json")
                .AddHeader("Content-Type", "application/json");

            var response = await Client.ExecuteAsync<MarketSummaryResponse>(request);

            return response;
        }

        private string GetRequestBodyContentHash(string requestBody)
        {
            string contentHash;

            using (SHA512 shaM = new SHA512Managed())
            {
                var contentBodyHash = shaM.ComputeHash(Encoding.ASCII.GetBytes(requestBody));
                contentHash = BitConverter.ToString(contentBodyHash).Replace("-", string.Empty);
            }

            return contentHash;
        }

        private string CreateSignature(string apiSecret, string data)
        {
            var hmacSha512 = new HMACSHA512(Encoding.ASCII.GetBytes(apiSecret));
            var hash = hmacSha512.ComputeHash(Encoding.ASCII.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }

    public class Response
    {
        public Address[] Addresses { get; set; }
    }

    public class Address
    {
        public string status { get; set; }
        public string currencySymbol { get; set; }
        public string cryptoAddress { get; set; }
        public string cryptoAddressTag { get; set; }
    }

    public class MarketSummary
    {
        public string symbol { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double volume { get; set; }
        public double quoteVolume { get; set; }
        public double percentChange { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class MarketSummaryResponse
    {
        public MarketSummary[] MarketSummaries { get; set; }
    }
}
