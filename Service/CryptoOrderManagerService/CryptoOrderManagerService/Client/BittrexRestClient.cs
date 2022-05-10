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

        public async Task<RestResponse> GetAddresses(string resource, string apiKey)
        {
            var unixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            var bodyHash = GetRequestBodyContentHash("");

            var signature = unixTimeStamp + BaseUrl + resource + "GET" + bodyHash;

            var signed = CreateSignature("", signature);

            RestRequest request = new RestRequest(resource)
                .AddHeader("Api-Key", apiKey)
                .AddHeader("Api-Timestamp", unixTimeStamp)
                .AddHeader("Api-Content-Hash", bodyHash)
                .AddHeader("Api-Signature", signed);             

            var response = await Client.PostAsync(request);

            var test = response.Headers;

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
}
