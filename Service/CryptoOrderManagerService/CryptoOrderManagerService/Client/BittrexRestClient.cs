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

            using (SHA512 shaM = new SHA512Managed())
            {
                contentBodyHash = shaM.ComputeHash(Encoding.UTF8.GetBytes(""));
            }

            var signature = unixTimeStamp + url + "GET" + contentBodyHash.ToString();

            RestRequest request = new RestRequest(url)
                .AddHeader("Api-Key", apiKey)
                .AddHeader("Api-Timestamp", unixTimeStamp)
                .AddHeader("Api-Content-Hash", contentBodyHash.ToString())
                .AddHeader("Api-Signature", signature);
                

            var response = await Client.PostAsync(request);

            return response;
        }
    }
}
