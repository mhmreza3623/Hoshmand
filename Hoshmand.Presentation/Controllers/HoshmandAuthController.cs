using FluentValidation;
using Hoshmand.Application.ApplicationServices.Hoshmand;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Pipeline;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hoshmand.Presentation.Controllers;

[ApiController]
public class HoshmandAuthController : ControllerBase
{
    private readonly IValidator<AuthenticatonRequestModel> _validator;
    private readonly IPipeline<HoshmandPipelineContext, HoshmandPipelineRequest, HoshmandPipelineResponse> _pipeline;

    public HoshmandAuthController(IValidator<AuthenticatonRequestModel> validator, IPipeline<HoshmandPipelineContext, HoshmandPipelineRequest, HoshmandPipelineResponse> pipeline)
    {
        this._validator = validator;
        this._pipeline = pipeline;
    }

    [HttpPost("Auth")]
    public async Task<IActionResult> Authentication([FromForm] AuthenticatonRequestModel requestModel)
    {
        var validation = await _validator.ValidateAsync(requestModel);
        if (validation.IsValid)
        {
            var result = await _pipeline.ExecuteAsync(new HoshmandPipelineRequest
            {
                Mobile = requestModel.Mobile,
                IdCardBehindImage = requestModel.IdCardBehindImage,
                IdCardFrontImage = requestModel.IdCardFrontImage,
                LiveVideo = requestModel.liveVideo,
                NationalCode = requestModel.NationalCode,
                SelfiImage = requestModel.SelfiImage
            });

            return Ok(result);
        }
        return BadRequest(JsonSerializer.Serialize(validation.Errors.ToList()));
    }

}
