using Framework.Common.ExMethods;
using Framework.Const;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.PresentationDTO.input;
using YasShop.Application.Contracts.PresentationDTO.Output;
using YasShop.Application.Users;
using YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;

namespace YasShop.WebApi.Controllers;

[ApiController]
[Route("/Auth")]
public class AuthController : Controller
{
    private readonly IUserApplication _userApplication;
    private readonly IJwtBuilder _jwtBuilder;
    private readonly IServiceProvider _serviceProvider;
    public AuthController(IUserApplication userApplication, IJwtBuilder jwtBuilder, IServiceProvider serviceProvider)
    {
        _userApplication = userApplication;
        _jwtBuilder = jwtBuilder;
        _serviceProvider = serviceProvider;
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
    public async Task<JsonResult> LogInAsync([FromForm] InpLoginByEmailPasswordDto input)
    {
        var qResult = await _userApplication.LoginByEmailPasswordAsync(input.Adapt<InpLoginByEmailPassword>());
        if (qResult.IsSuccess)
        {
            string RandomKey = string.Empty.AesKeyGenerator();
            string Token = (await _jwtBuilder.CreateTokenAsync(qResult.Message)).AesEncrypt(RandomKey);
            string RefreshToken = RandomKey.AesEncrypt(AuthConst.SecretKey);
            return new JsonResult(
                new OperationResult<(string Token, string RefreshToken)>().Succeeded(200, "Success",
                    (Token, RefreshToken)));
        }

        return new JsonResult(new OperationResult().Failed(400, qResult.Message));
    }

    [HttpPost]
    [Route("/Auth/RefreshLogIn")]
    public async Task<JsonResult> RefreshLogInAsync([FromQuery] InpRefreshLogInDto input)
    {
        try
        {
            #region Validation
            {
                input.CheckModelState(_serviceProvider);
            }
            #endregion Validation

            string Token = await _jwtBuilder.CreateTokenAsync(input.UserId);
            if (Token is null)
                return new JsonResult(new OperationResult<OutRefreshLoginDto>
                {
                    Code = 500,
                    Message = "Failed"
                });

            OutRefreshLoginDto output = new();
            string key = string.Empty.AesKeyGenerator();
            string RefreshToken = key.AesEncrypt(AuthConst.SecretKey);
            string EncreptedToken = Token.AesEncrypt(key);
            output.Token = EncreptedToken;
            output.RefreshToken = RefreshToken;
            return new JsonResult(new OperationResult<OutRefreshLoginDto>
            {
                Data = output,
                Code = 200,
                Message = "success"
            });

        }
        catch (Exception e)
        {
            return new JsonResult(new OperationResult().Failed(400, e.Message));
        }
    }

    [HttpPost]
    [Route("/Auth/ResetPassword")]
    public async Task<JsonResult> ResetPasswordAsync([FromForm] InpResetPassword input)
    {
        var qReset = await _userApplication.ResetPasswordAsync(input);
        if (qReset.IsSuccess)
            return new JsonResult(new OperationResult().Succeeded(200, "ok"));

        return new JsonResult(new OperationResult().Failed(400, "Failed"));
    }
}