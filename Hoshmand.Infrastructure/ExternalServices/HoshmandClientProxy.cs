using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.SettingServices;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
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

        public async Task<K> SendAsync<T,K>(HttpMethod httpMethod, string serviceUrl, T input, Func<object, K> resultAction, string queryString)
            where T : class
            where K : class
        {
            var httpClient = GetClient(_serviceSettings.HoshmandOrderBaseAddress);
            HttpResponseMessage response = new HttpResponseMessage();

            switch (httpMethod.Method)
            {
                case "Post":
                    response = await httpClient.PostAsync(serviceUrl, new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, JsonContentType));
                    break;
                case "Put":
                    response = await httpClient.PutAsync(serviceUrl, new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, JsonContentType));
                    break;
                case "Delete":
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
    }
}
