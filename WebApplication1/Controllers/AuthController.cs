using Mapster;
using Microsoft.AspNetCore.Mvc;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.input;
using YasShop.Application.Users;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/Auth")]
public class AuthController : Controller
{
    private readonly IUserApplication _userApplication;

    public AuthController(IUserApplication userApplication)
    {
        _userApplication = userApplication;
    }


    [HttpPost]
    [Route("/Auth/RegisterByEmaiPassword")]
    public async Task<JsonResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPasswordDto input)
    {
        var qUser = await _userApplication.RegisterByEmailPasswordAsync(input.Adapt<InpRegisterByEmailPassword>());
        return new JsonResult(new OperationResult().Succeeded(200, "ok"));
    }

    [HttpPost]
    [Route("/Auth/LogIn")]
    public async Task<JsonResult> LogIn([FromForm] InpLoginByEmailPasswordDto input)
    {
        var qResult = await _userApplication.LoginByEmailPasswordAsync(input.Adapt<InpLoginByEmailPassword>());
        if (qResult.IsSuccess)
            return new JsonResult(new OperationResult().Succeeded(200, "ok"));

        return new JsonResult(new OperationResult().Failed(400, qResult.Message));
    }
}