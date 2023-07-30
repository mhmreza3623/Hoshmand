using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.SettingServices;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Hoshmand.Infrastructure.ExternalServices
{
    public class HoshmandClientProxy : IHoshmandClientProxy
    {
        private const string JsonContentType = "application/json";
        private readonly IServiceSettings _serviceSettings;

        public HoshmandClientProxy(IServiceSettings serviceSettings)
        {
            _serviceSettings = serviceSettings;
        }

        public HttpClient GetClient(string url)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };

            return httpClient;
        }


        public async Task<K> SendAsync<T, K>(HttpMethod httpMethod, string serviceUrl, T input, Func<object, K> resultAction, string queryString)
            where T : class
            where K : class
        {
            var httpClient = GetClient(_serviceSettings.HoshmandOrderBaseAddress);
            HttpResponseMessage response = new HttpResponseMessage();

            switch (httpMethod.Method.ToLower())
            {
                case "post":
                    response = await httpClient.PostAsync(serviceUrl, new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, JsonContentType));
                    break;
                case "put":
                    response = await httpClient.PutAsync(serviceUrl, new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, JsonContentType));
                    break;
                case "delete":
                    throw new Exception("Invalid http method");
                default:
                    response = await httpClient.GetAsync($"{serviceUrl}?{queryString}");
                    break;

            }

            if (response.IsSuccessStatusCode)
            {
                return resultAction(response);
            }

            return null;
        }

        public async Task<HttpResponseMessage> SendFile(string orderId, StreamContent idCard1, StreamContent idCard2)
        {
            var client = GetClient(_serviceSettings.HoshmandIdCardBaseAddress);


            using (var multipartFormContent = new MultipartFormDataContent())
            {
                //Load the file and set the file's Content-Type header
                idCard1.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                idCard2.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                //Add the file
                multipartFormContent.Add(idCard1, name: "file", fileName: "house.png");
                multipartFormContent.Add(idCard2, name: "file", fileName: "house.png");

                //Send it
                var response = await client.PostAsync($"/idCard2/{orderId}", multipartFormContent);
                response.EnsureSuccessStatusCode();

                return response;
            }
        }
    }
}
