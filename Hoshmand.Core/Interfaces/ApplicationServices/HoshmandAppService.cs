using Microsoft.AspNetCore.Http;

namespace Hoshmand.Core.Interfaces.ApplicationServices
{
    public interface IHoshmandAppService
    {
        Task<string> Authentication(IFormFile idCardLink, IFormFile idCardLink2, string mobile, string nationalCode);
    }
}
