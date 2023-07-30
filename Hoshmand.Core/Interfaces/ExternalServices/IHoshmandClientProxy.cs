namespace Hoshmand.Core.Interfaces.ExternalServices
{
    public interface IHoshmandClientProxy
    {
        Task<K> SendAsync<T, K>(HttpMethod httpMethod, string serviceUrl, T input, Func<object, K> resultAction, string queryString)
            where T :class  
            where K : class;
        Task<HttpResponseMessage> SendFile(string orderId, StreamContent idCard1, StreamContent idCard2);
    }
}
