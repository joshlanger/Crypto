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
        public RestClient Client = new RestClient();
        public async Task<RestResponse> Authenticate(string url, string apiKey)
        {
            DateTimeOffset currentTime = DateTimeOffset.UtcNow;
            string unixTimeStamp = currentTime.ToUnixTimeMilliseconds().ToString();

            byte[] contentBodyHash;
            string content;

            using (SHA512 shaM = new SHA512Managed())
            {
                contentBodyHash = shaM.ComputeHash(Encoding.ASCII.GetBytes(""));
                content = BitConverter.ToString(contentBodyHash).Replace("-", string.Empty);
            }

            var signature = unixTimeStamp + url + "GET" + content;

            var signed = CreateSignature("", signature);

            RestRequest request = new RestRequest(url)
                .AddHeader("Api-Key", apiKey)
                .AddHeader("Api-Timestamp", unixTimeStamp)
                .AddHeader("Api-Content-Hash", content)
                .AddHeader("Api-Signature", signed);
                
            //test

            var response = await Client.PostAsync(request);

            return response;
        }

        private string CreateSignature(string apiSecret, string data)
        {
            var hmacSha512 = new HMACSHA512(Encoding.ASCII.GetBytes(apiSecret));
            var hash = hmacSha512.ComputeHash(Encoding.ASCII.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
