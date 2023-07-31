using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Pipeline;

namespace Hoshmand.Application.ApplicationServices.Hoshmand;

public class HoshmandPipelineProcess : AbstractPipeline<HoshmandPipelineContext, HoshmandPipelineRequest, HoshmandPipelineResponse>
{
    private readonly IHoshmandClientProxy _hoshmandServiceProxy;

    public object requestModel { get; private set; }

    public HoshmandPipelineProcess(IHoshmandClientProxy hoshmandServiceProxy)
    {
        this._hoshmandServiceProxy = hoshmandServiceProxy;

        AddOperation(GetOrderId);
        AddOperation(CheckNumPhone);
        AddOperation(CheckCode);
        AddOperation(IdCard);
        AddOperation(CompareIdcardFace2);

    }

    protected async Task GetOrderId(HoshmandPipelineContext cntx, PipelineOperation<HoshmandPipelineContext> next)
    {
        var orderRequst = await _hoshmandServiceProxy.GetOrder();

        cntx.OrderRequest = orderRequst;

        await next(cntx);
    }

    protected async Task CheckNumPhone(HoshmandPipelineContext cntx, PipelineOperation<HoshmandPipelineContext> next)
    {
        var checkNumPhoneResponse = await _hoshmandServiceProxy.CheckNumPhone(cntx.Request.Mobile, cntx.Request.NationalCode, cntx.OrderRequest.Id, cntx.OrderRequest.OrderId);

        cntx.CheckNumPhoneResponse = checkNumPhoneResponse;

        await next(cntx);
    }

    protected async Task CheckCode(HoshmandPipelineContext cntx, PipelineOperation<HoshmandPipelineContext> next)
    {
        var checkCodeResponse = await _hoshmandServiceProxy.CheckCode(cntx.OrderRequest.Id, cntx.OrderRequest.OrderId, cntx.CheckNumPhoneResponse.MessageCodeOutput);

        cntx.CheckCodeResponse = checkCodeResponse;

        await next(cntx);
    }

    protected async Task IdCard(HoshmandPipelineContext cntx, PipelineOperation<HoshmandPipelineContext> next)
    {
        var idCardRespone = await _hoshmandServiceProxy.IdCard(cntx.OrderRequest.Id, cntx.OrderRequest.OrderId, cntx.Request.IdCardFrontImage, cntx.Request.IdCardBehindImage);

        cntx.IdCardRespone = idCardRespone;

        await next(cntx);
    }

    protected async Task CompareIdcardFace2(HoshmandPipelineContext cntx, PipelineOperation<HoshmandPipelineContext> next)
    {
        var compareIdcardFaceResponse = await _hoshmandServiceProxy.CompareIdcardFace2(cntx.OrderRequest.Id, cntx.OrderRequest.OrderId, cntx.Request.SelfiImage, cntx.Request.LiveVideo);

        if (compareIdcardFaceResponse.Status.ToLower() == "approve")
        {
            cntx.Response.CompareIdcardFaceResponse = compareIdcardFaceResponse;
        }

        await next(cntx);
    }


}
