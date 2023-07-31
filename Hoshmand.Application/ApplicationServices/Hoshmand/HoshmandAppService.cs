using FluentValidation;
using Hoshmand.Core.Common;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Dto.Response;
using Hoshmand.Core.Entities;
using Hoshmand.Core.Interfaces.ApplicationServices;
using Hoshmand.Core.Interfaces.DomainServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Hoshmand.Application.PipeLines.Hoshmand;
public class HoshmandAppService : IHoshmandAppService
{
    IHoshmandClientProxy _hoshmandServiceProxy;

    public HoshmandAppService(IHoshmandClientProxy hoshmandServiceProxy)
    {
        _hoshmandServiceProxy = hoshmandServiceProxy;
    }

    public async Task<bool> Authentication(AuthenticatonRequestModel requestModel)
    {
        HoshmandResponseDto CheckNumPhoneResponse = null;
        HoshmandResponseDto CheckCodeResponse = null;
        HoshmandIdCardResponseDto? idCardRespone = null;
        CompareFaceResponseDto compareIdcardFaceResponse = null;

        var orderRequst = await _hoshmandServiceProxy.GetOrder();

        if (orderRequst != null)
        {
            CheckNumPhoneResponse = await _hoshmandServiceProxy.CheckNumPhone(requestModel.Mobile, requestModel.NationalCode, orderRequst.Id, orderRequst.OrderId);
        }

        if (CheckNumPhoneResponse != null)
        {
            CheckCodeResponse = await _hoshmandServiceProxy.CheckCode(orderRequst.Id, orderRequst.OrderId, CheckNumPhoneResponse.MessageCodeOutput);
        }

        if (CheckCodeResponse != null)
        {
            idCardRespone = await _hoshmandServiceProxy.IdCard(orderRequst.Id, orderRequst.OrderId, requestModel.IdCardFrontImage, requestModel.IdCardBehindImage);
        }

        if (idCardRespone != null)
        {
            compareIdcardFaceResponse = await _hoshmandServiceProxy.CompareIdcardFace2(orderRequst.Id, orderRequst.OrderId, requestModel.SelfiImage, requestModel.liveVideo);

            if (compareIdcardFaceResponse != null)
            {
                //
            }
        }

        return true;
    }
}

