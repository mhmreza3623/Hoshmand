using Hoshmand.Core.Common;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Dto.Response;
using Hoshmand.Core.Entities;
using Hoshmand.Core.Interfaces.ApplicationServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.Repositories;
using Hoshmand.Core.Interfaces.SettingServices;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Hoshmand.Application.Services;
public class HoshmandAppService : IHoshmandAppService
{
    private readonly IHoshmandClientProxy _hoshmandServiceProxy;
    private readonly IServiceSettings _serviceSettings;
    private readonly IGeneralRepository<OrderRequestEntity> _orderRepo;
    private readonly IGeneralRepository<NumPhoneRequestEntity> _numPhoneRepo;
    private readonly IGeneralRepository<CheckCodeRequestEntity> _checkCodeRepo;
    private readonly IGeneralRepository<IdCardRequestEntity> _idCardRepo;

    public HoshmandAppService(
        IHoshmandClientProxy hoshmandServiceProxy,
        IServiceSettings serviceSettings,
        IGeneralRepository<OrderRequestEntity> orderRepo,
        IGeneralRepository<NumPhoneRequestEntity> numPhoneRepo,
        IGeneralRepository<CheckCodeRequestEntity> checkCodeRepo,
        IGeneralRepository<IdCardRequestEntity> idCardRepo
        )
    {
        _hoshmandServiceProxy = hoshmandServiceProxy;
        _serviceSettings = serviceSettings;
        _orderRepo = orderRepo;
        _numPhoneRepo = numPhoneRepo;
        _checkCodeRepo = checkCodeRepo;
        this._idCardRepo = idCardRepo;
    }

    public async Task<string> Authentication(IFormFile idCardLink, IFormFile idCardLink2, string mobile, string nationalCode)
    {
        var orderRequst = await GetOrder();

        if (orderRequst == null) { }

        var numPhoneResponse = await CheckNumPhone(mobile, nationalCode, orderRequst.Id, orderRequst.OrderId);

        if (string.IsNullOrEmpty(numPhoneResponse)) { }

        var checkCodeResponse = await CheckCode(orderRequst.Id, orderRequst.OrderId, numPhoneResponse);

        if (checkCodeResponse)
        {
            await IdCard(orderRequst.Id, orderRequst.OrderId, idCardLink, idCardLink2);
        }

        return string.Empty;
    }


    //Hoshmand services
    private async Task<OrderRequestEntity> GetOrder()
    {
        var order = _orderRepo.Add(new OrderRequestEntity
        {
            OrderId = Guid.NewGuid().ToString()
        });


        var response = await _hoshmandServiceProxy
            .SendJsonRequestAsync(
            HttpMethod.Post,
            "/GetOrderId2/",
            new OrderRequestDto { orderId = order.OrderId },
           orderServiceCallback,
            string.Empty);

        UpdateResponse(_orderRepo, order, response);

        if (response != null && response.Status.ToLower() == "inprogress")
        {
            return order;
        }

        return null;

    }

    private async Task<string> CheckNumPhone(string mobile, string natinalCode, int orderRequestId, string orderId)
    {
        var numPhoneRequest = _numPhoneRepo.Add(new NumPhoneRequestEntity
        {
            OrderRequestId = orderRequestId,
            Number = natinalCode,
            Phone = mobile
        });


        var response = await _hoshmandServiceProxy
            .SendJsonRequestAsync(
            HttpMethod.Put,
            $"/PostIdNumPhone2/{orderId}",
            new NumPhoneRequestDto { phone = mobile, idNum = natinalCode },
           NumPhoneServiceCallback,
            string.Empty);

        UpdateResponse(_numPhoneRepo, numPhoneRequest, response);

        if (response != null &&
            !string.IsNullOrWhiteSpace(response.RejectMessage))
        {
            return response.MessageCodeOutput;

        }

        return string.Empty;

    }

    private async Task<bool> CheckCode(int orderRequestId, string orderId, string messageCodeOutput)
    {
        var checkCodeRequest = _checkCodeRepo.Add(new CheckCodeRequestEntity
        {
            MessageCodeInput = messageCodeOutput,
            OrderRequestId = orderRequestId
        });

        var response = await _hoshmandServiceProxy
            .SendJsonRequestAsync<CheckCodeRequestDto, HoshmandOrderResponseDto>(
            HttpMethod.Put,
            $"/checkCode2/{orderId}",
            new CheckCodeRequestDto { messageCodeInput = messageCodeOutput },
           CheckCodeServiceCallback,
            string.Empty);

        UpdateResponse(_checkCodeRepo, checkCodeRequest, response);


        if (response != null && response.Status.ToLower() == "inprogress")
        {
            return true;
        }

        return false;
    }

    public async Task<IdCardRequestDto> IdCard(int orderRequestId, string orderId, IFormFile idCardLink, IFormFile idCardLink2)
    {
        var idCard = _idCardRepo.Add(new IdCardRequestEntity
        {
            CreateDate = DateTime.Now,
            ImageId1 = idCardLink.ToByteArray(),
            ImageId2 = idCardLink2.ToByteArray(),
            OrderRequestId = orderRequestId,
        });


        var idCard1 = new StreamContent(idCardLink.OpenReadStream());
        var idCard2 = new StreamContent(idCardLink2.OpenReadStream());

        var request = new List<FormDataRequestDto>() {
                    new FormDataRequestDto {
                        FileName = idCardLink.FileName,
                        ContentStream = idCard1,
                        FormFieldName = "idCardLink",
                        ContentType = idCardLink.ContentType },

        new FormDataRequestDto {
                        FileName = idCardLink2.FileName,
                        ContentStream = idCard2,
                        FormFieldName = "idCardLink2",
                        ContentType = idCardLink2.ContentType }
        };

        var response = await _hoshmandServiceProxy.SendFormDataRequestAsync<IdCardRequestDto>(HttpMethod.Put, $"/idCard2/{orderId}", request, IdCardCallBack);

        UpdateResponse(_idCardRepo, idCard, response);

        return response;

    }

    private async Task<CompareFaceResponseDto> CompareIdcardFace2(int orderRequestId, string orderId, IFormFile faceImage, IFormFile video)
    {
        var faceImageContent = new StreamContent(faceImage.OpenReadStream());
        var videoContent = new StreamContent(video.OpenReadStream());

        var request = new List<FormDataRequestDto>() {
                    new FormDataRequestDto {
                        FileName = faceImage.FileName,
                        ContentStream = faceImageContent,
                        FormFieldName = "faceImageLink",
                        ContentType = faceImage.ContentType },

        new FormDataRequestDto {
                        FileName = video.FileName,
                        ContentStream = videoContent,
                        FormFieldName = "videoLink",
                        ContentType = video.ContentType }
        };

        var response = await _hoshmandServiceProxy.SendFormDataRequestAsync<CompareFaceResponseDto>(HttpMethod.Put, $"/CompareIdcardFace2/{orderId}", request, CompareFaceResponseDtoCallBack);

        return response;
    }


    //service CallBacks
    private HoshmandOrderResponseDto orderServiceCallback(object arg)
    {
        return JsonSerializer.Deserialize<HoshmandOrderResponseDto>(((HttpResponseMessage)arg).Content.ReadAsStreamAsync().Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
    private HoshmandResponseDto NumPhoneServiceCallback(object arg)
    {
        return JsonSerializer.Deserialize<HoshmandResponseDto>(((HttpResponseMessage)arg).Content.ReadAsStreamAsync().Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
    private HoshmandResponseDto CheckCodeServiceCallback(object arg)
    {
        return JsonSerializer.Deserialize<HoshmandResponseDto>(((HttpResponseMessage)arg).Content.ReadAsStreamAsync().Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
    private IdCardRequestDto IdCardCallBack(object arg)
    {
        return JsonSerializer.Deserialize<IdCardRequestDto>(((HttpResponseMessage)arg).Content.ReadAsStreamAsync().Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
    private CompareFaceResponseDto CompareFaceResponseDtoCallBack(object arg)
    {
        return JsonSerializer.Deserialize<CompareFaceResponseDto>(((HttpResponseMessage)arg).Content.ReadAsStreamAsync().Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
    private void UpdateResponse<TInput>(IGeneralRepository<TInput> repository, TInput entity, object response) where TInput : BaseEntity
    {
        entity.RawResponse = JsonSerializer.Serialize(response);
        entity.ResponsDate = DateTime.Now;
        repository.Udate(entity);
    }

}

