using Hoshmand.Core.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshmand.Core.Interfaces.Shared
{
    public interface IHttpClientUtility
    {
        Task<TResult> SendJsonRequestAsync<TInput, TResult>(HttpMethod httpMethod, string serviceUrl, TInput input, Func<object, TResult> resultAction, string queryString) where TInput : class where TResult : class;
        Task<TResult> SendFormDataRequestAsync<TResult>(HttpMethod httpMethod, string serviceUrl, List<FormDataRequestDto> files, Func<object, TResult> resultAction) where TResult : class;
    }
}
