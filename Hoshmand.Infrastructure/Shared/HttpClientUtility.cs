using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Interfaces.DomainServices;
using Hoshmand.Core.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hoshmand.Infrastructure.Shared
{
    public class HttpClientUtility : IHttpClientUtility
    {
        private const string JsonContentType = "application/json";
        private readonly IServiceSettings _serviceSettings;

        public HttpClientUtility(IServiceSettings serviceSettings)
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

        public async Task<TResult> SendJsonRequestAsync<TInput, TResult>(HttpMethod httpMethod, string serviceUrl, TInput input, Func<object, TResult> resultAction, string queryString)
            where TInput : class
            where TResult : class
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

        public async Task<TResult> SendFormDataRequestAsync<TResult>(HttpMethod httpMethod, string serviceUrl, List<FormDataRequestDto> files, Func<object, TResult> resultAction)
            where TResult : class
        {
            var httpClient = GetClient(_serviceSettings.HoshmandIdCardBaseAddress);


            HttpResponseMessage response = new HttpResponseMessage();


            using (var multipartFormContent = new MultipartFormDataContent())
            {
                foreach (var file in files)
                {
                    file.ContentStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    multipartFormContent.Add(file.ContentStream, name: file.FormFieldName, fileName: file.FileName);
                }

                switch (httpMethod.Method.ToLower())
                {
                    case "post":
                        response = await httpClient.PostAsync(serviceUrl, multipartFormContent);
                        response.EnsureSuccessStatusCode();
                        break;
                    case "put":
                        response = await httpClient.PutAsync(serviceUrl, multipartFormContent);
                        response.EnsureSuccessStatusCode();

                        break;
                    default:
                        throw new Exception("Invalid http method");
                }

                if (response.IsSuccessStatusCode)
                {
                    return resultAction(response);
                }

                return null;
            }
        }
    }
}
