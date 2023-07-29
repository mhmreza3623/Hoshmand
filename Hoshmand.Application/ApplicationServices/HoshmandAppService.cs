using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Dto.Response;
using Hoshmand.Core.Entities;
using Hoshmand.Core.Interfaces.ApplicationServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.Repositories;
using Hoshmand.Core.Interfaces.SettingServices;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace Hoshmand.Application.ApplicationServices;
public class HoshmandAppService : IHoshmandAppService
{
    private readonly IHoshmandClientProxy _hoshmandServiceProxy;
    private readonly IServiceSettings _serviceSettings;
    private readonly IGeneralRepository<OrderRequestEntity> _orderRepo;
    private readonly IGeneralRepository<NumPhoneRequestEntity> _numPhoneRepo;
    private readonly IGeneralRepository<CheckCodeRequestEntity> _checkCodeRepo;

    public HoshmandAppService(
        IHoshmandClientProxy hoshmandServiceProxy,
        IServiceSettings serviceSettings,
        IGeneralRepository<OrderRequestEntity> orderRepo,
        IGeneralRepository<NumPhoneRequestEntity> numPhoneRepo,
        IGeneralRepository<CheckCodeRequestEntity> checkCodeRepo
        )
    {
        _hoshmandServiceProxy = hoshmandServiceProxy;
        this._serviceSettings = serviceSettings;
        this._orderRepo = orderRepo;
        this._numPhoneRepo = numPhoneRepo;
        this._checkCodeRepo = checkCodeRepo;
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
            return await IdCard(orderRequst.Id, orderRequst.OrderId, idCardLink, idCardLink2);
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


        var orderResponse = await _hoshmandServiceProxy
            .SendAsync<OrderRequestDto, HoshmandOrderResponseDto>(
            HttpMethod.Post,
            "/GetOrderId2/",
            new OrderRequestDto { OrderId = order.OrderId },
           orderServiceCallback,
            string.Empty);

        if (orderResponse != null && orderResponse.Status.ToLower() == "inprogress")
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


        var numphoneResponse = await _hoshmandServiceProxy
            .SendAsync<NumPhoneRequestDto, HoshmandResponseDto>(
            HttpMethod.Post,
            $"/PostIdNumPhone2/{orderId}",
            new NumPhoneRequestDto { Phone = mobile, Number = natinalCode },
           NumPhoneServiceCallback,
            string.Empty);

        if (numphoneResponse != null &&
            !string.IsNullOrWhiteSpace(numphoneResponse.RejectMessage))
        {
            return numphoneResponse.MessageCodeOutput;

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


        var checkCodeResponse = await _hoshmandServiceProxy
            .SendAsync<CheckCodeRequestDto, HoshmandOrderResponseDto>(
            HttpMethod.Post,
            $"/checkCode2/{orderId}",
            new CheckCodeRequestDto { MessageCode = messageCodeOutput },
           CheckCodeServiceCallback,
            string.Empty);

        if (checkCodeResponse != null && checkCodeResponse.Status.ToLower() == "inprogress")
        {
            return true;
        }

        return false;
    }

    private async Task<string> IdCard(int orderRequestId, string orderId, IFormFile idCardLink, IFormFile idCardLink2)
    {
        var action = _serviceSettings.HoshmandIdCardBaseAddress + $"/idCard2/{orderId}";
        var template = GetResponseTemplate();
        var form = GenerateForm(action, idCardLink, idCardLink2);

        return form;
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


    //template parser
    private string GetResponseTemplate()
    {
        string result;
        using (StreamReader sr = new(@"html\idCard.html"))
        {
            // Read the stream to a string, and write the string to the console.
            result = sr.ReadToEndAsync().Result;
        }

        return result;
    }
    private string GenerateForm(string action, IFormFile idCardLink, IFormFile idCardLink2)
    {
        var result = new StringBuilder($"<form name=\"idCardFrom\" method=\"post\" action=\"{action}\" target=\"_top\" >\n");
        result.Append($"<input id=\"idCardLink\" type=\"file\" value=\"{idCardLink}\" />\n");
        result.Append($"<input id=\"idCardLink2\" type=\"file\" value=\"{idCardLink2}\" />\n");
        return result.ToString();
    }
}

