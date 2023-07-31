using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Dto.Response;
using Hoshmand.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Hoshmand.Core.Interfaces.ExternalServices
{
    public interface IHoshmandClientProxy
    {
        Task<HoshmandResponseDto> CheckCode(int orderRequestId, string orderId, string messageCodeOutput);
        Task<HoshmandResponseDto> CheckNumPhone(string mobile, string natinalCode, int orderRequestId, string orderId);
        Task<CompareFaceResponseDto> CompareIdcardFace2(int orderRequestId, string orderId, IFormFile faceImage, IFormFile video);
        Task<OrderRequestEntity> GetOrder();
        Task<HoshmandIdCardResponseDto> IdCard(int orderRequestId, string orderId, IFormFile idCardLink, IFormFile idCardLink2);
    }
}
