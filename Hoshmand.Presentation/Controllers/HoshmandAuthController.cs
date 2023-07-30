using Hoshmand.Core.Interfaces.ApplicationServices;
using Hoshmand.Core.Interfaces.SettingServices;
using Hoshmand.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoshmand.Presentation.Controllers;

[ApiController]
public class HoshmandAuthController : ControllerBase
{
    private readonly IHoshmandAppService _hoshmandAppService;

    public HoshmandAuthController(IHoshmandAppService hoshmandAppService)
    {
        this._hoshmandAppService = hoshmandAppService;
    }

    [HttpPost("Auth")]
    public async Task<IActionResult> AuthenticationByIdCardImages(IFormFile IdCard1, IFormFile IdCard2,string mobile,string nationalCode)
    {
        var result = await _hoshmandAppService.Authentication(IdCard1, IdCard2, mobile, nationalCode);

        return Ok(result);
    }

    [HttpPost("idard")]
    public async Task<IActionResult> IdCard(IFormFile IdCard1, IFormFile IdCard2)
    {
        var result = await _hoshmandAppService.IdCard(1,"2", IdCard1, IdCard2);

        return Ok(result);
    }
}
