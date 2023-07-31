using FluentValidation;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Interfaces.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace Hoshmand.Presentation.Controllers;

[ApiController]
public class HoshmandAuthController : ControllerBase
{
    private readonly IHoshmandAppService _hoshmandAppService;
    private readonly IValidator<AuthenticatonRequestModel> _validator;

    public HoshmandAuthController(IHoshmandAppService hoshmandAppService, IValidator<AuthenticatonRequestModel> validator)
    {
        this._hoshmandAppService = hoshmandAppService;
        this._validator = validator;
    }

    [HttpPost("Auth")]
    public async Task<IActionResult> Authentication([FromForm] AuthenticatonRequestModel requestModel)
    {
        var validation = await _validator.ValidateAsync(requestModel);
        if (validation.IsValid)
        {
            var result = await _hoshmandAppService.Authentication(requestModel);

            return Ok(result);
        }
        return BadRequest();
    }

}
