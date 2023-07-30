using Hoshmand.Core.Dto.Requests;

namespace Hoshmand.Core.Interfaces.ExternalServices
{
    public interface IHoshmandClientProxy
    {
        Task<TResult> SendJsonRequestAsync<TInput, TResult>(HttpMethod httpMethod, string serviceUrl, TInput input, Func<object, TResult> resultAction, string queryString) where TInput : class where TResult : class;
        Task<TResult> SendFormDataRequestAsync<TResult>(HttpMethod httpMethod, string serviceUrl, List<FormDataRequestDto> files, Func<object, TResult> resultAction) where TResult : class;
    }
}
