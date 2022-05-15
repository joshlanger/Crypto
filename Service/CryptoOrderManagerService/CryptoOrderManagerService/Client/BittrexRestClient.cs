using CryptoOrderManagerService.Model.Bittrex;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

        public async Task<RestResponse> GetClosedOrders(string resource, string apiKey, string apiSecret)
        {
            var request = BuildRestRequestAuthenticationHeaders(resource, apiKey, apiSecret);

            var response = await Client.ExecuteAsync(request);

            //var responseModel = JsonSerializer.Deserialize<List<BittrexClosedOrder>>(response.Content);

            return response;
        }

        public async Task<RestResponse> GetOpenOrders(string resource, string apiKey, string apiSecret)
        {
            var request = BuildRestRequestAuthenticationHeaders(resource, apiKey, apiSecret);

            var response = await Client.ExecuteAsync(request);

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

        private RestRequest BuildRestRequestAuthenticationHeaders(string resource, string apiKey, string apiSecret)
        {
            var unixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            var bodyHash = GetRequestBodyContentHash("");

            var unhashedSignature = unixTimeStamp + BaseUrl + resource + "GET" + bodyHash;

            var signatureHash = CreateSignature(apiSecret, unhashedSignature);

            RestRequest request = new RestRequest(resource)
               .AddHeader("Api-Key", apiKey)
               .AddHeader("Api-Timestamp", unixTimeStamp)
               .AddHeader("Api-Content-Hash", bodyHash)
               .AddHeader("Api-Signature", signatureHash);

            return request;
        }
    }
}
