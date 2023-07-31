using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Dto.Response;
using Microsoft.AspNetCore.Http;

namespace Hoshmand.Core.Interfaces.ApplicationServices
{
    public interface IHoshmandAppService
    {
        Task<bool> Authentication(IFormFile imgIdCardFront, IFormFile imgIdCardBehind, IFormFile faceImage, IFormFile liveVedio, string mobile, string nationalCode);
    }
}
