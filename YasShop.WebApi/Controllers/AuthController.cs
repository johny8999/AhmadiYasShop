using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Const;
using Framework.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.Endpoint.input;
using YasShop.Application.Contracts.Endpoint.Output;
using YasShop.Application.Users;
using YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;
using YasShop.Infrastructure.EfCore.Migrations;

namespace YasShop.WebApi.Controllers;

[ApiController]
[Route("/Auth")]
public class AuthController : Controller
{
    private readonly ILogger _Logger;
    private readonly IUserApplication _userApplication;
    private readonly IJwtBuilder _jwtBuilder;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILocalizer _localizer;
    public AuthController(IUserApplication userApplication, IJwtBuilder jwtBuilder, IServiceProvider serviceProvider, ILocalizer localizer, Framework.Infrastructure.ILogger logger)
    {
        _userApplication = userApplication;
        _jwtBuilder = jwtBuilder;
        _serviceProvider = serviceProvider;
        _localizer = localizer;
        _Logger=logger;
    }

    [HttpPost]
    [Route("/Auth/RegisterByEmaiPassword")]
    public async Task<JsonResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPasswordDto input)
    {
        var qUser = await _userApplication.RegisterByEmailPasswordAsync(input.Adapt<InpRegisterByEmailPassword>());
        return new JsonResult(new OperationResult().Succeeded(200, "ok"));
    }

    [HttpPost]
    [Route("/Auth/LogInByPhoneNumberStep1")]
    public async Task<JsonResult> LoginByPhoneNumberStep1Async(InpLogInByPhoneNumberStep1 Input)
    {
        try
        {
            #region Validation
            {
                Input.CheckModelState(_serviceProvider);
            }
            #endregion Validation

            var _Result = await _userApplication.LoginByPhoneNumberStep1Async(new InpLoginByPhoneNumberStep1
            {
                PhoneNumber = Input.PhoneNumber
            });

            if (_Result.IsSuccess == false)
                return new JsonResult(new OperationResult().Failed(_Result.Message));

            return new JsonResult(new OperationResult().Succeeded(_Result.Message));
        }
        catch (ArgumentException ex)
        {
            return new JsonResult(new OperationResult().Failed(400, ex.Message));
        }
        catch (Exception)
        {

            return new JsonResult(new OperationResult().Failed(500, _localizer["Error500"]));
        }
    }

    [HttpPost]
    [Route("/Auth/ResendSmsOtpCode")]
    public async Task<JsonResult> ResendSmsOtpCodeAsync(InpResendSmsOtpCode Input)
    {
        try
        {
            #region Validation
            {
                Input.CheckModelState(_serviceProvider);
            }
            #endregion Validation

            var _Result = await _userApplication.ResendOtpCodeAsync(new InpResendOtpCode
            {
                PhoneNumber = Input.PhoneNumber
            });

            if (_Result.IsSuccess==false)
                return new JsonResult(new OperationResult().Failed(_Result.Message));

            return new JsonResult(new OperationResult().Succeeded(_Result.Message));

        }
        catch (ArgumentException ex)
        {
            return new JsonResult(new OperationResult().Failed(400, ex.Message));
        }
        catch (Exception)
        {

            return new JsonResult(new OperationResult().Failed(500, _localizer["Error500"]));
        }
    }

    [HttpPost]
    [Route("/Auth/LogInByPhoneNumberStep2")]
    public async Task<JsonResult> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input)
    {
        try
        {
            #region Validation
            {
                Input.CheckModelState(_serviceProvider);
            }
            #endregion Validation

            var qResult = await _userApplication.LoginByPhoneNumberStep2Async(new InpLoginByPhoneNumberStep2
            {
                PhoneNumber=Input.PhoneNumber,
                OTPCode=Input.OTPCode
            });
            if (!qResult.IsSuccess)
                return new JsonResult(new OperationResult().Failed(400, _localizer[qResult.Message]));

            string RandomKey = string.Empty.AesKeyGenerator();
            string Token = (await _jwtBuilder.CreateTokenAsync(qResult.Data)).AesEncrypt(RandomKey);
            string RefreshToken = RandomKey.AesEncrypt(AuthConst.SecretKey);
            return new JsonResult(
                new OperationResult<(string Token, string RefreshToken)>().Succeeded(200, "Success",
                    (Token, RefreshToken)));

        }
        catch (ArgumentException ex)
        {
            _Logger.Debug(ex);
            return new JsonResult(new OperationResult().Failed(400, ex.Message));
        }
        catch (Exception ex)
        {
            _Logger.Error(ex);
            return new JsonResult(new OperationResult().Failed(500, _localizer["Error500"]));
        }
    }

    [HttpPost]
    [Route("/Auth/LogInByEmail")]
    public async Task<JsonResult> LogInByEmailAsync([FromBody] InpLoginByEmailPasswordDto input)
    {
        var qResult = await _userApplication.LoginByEmailPasswordAsync(input.Adapt<InpLoginByEmailPassword>());
        if (!qResult.IsSuccess)
            return new JsonResult(new OperationResult().Failed(400, _localizer[qResult.Message]));

        string RandomKey = string.Empty.AesKeyGenerator();
        string Token = (await _jwtBuilder.CreateTokenAsync(qResult.Message)).AesEncrypt(RandomKey);
        string RefreshToken = RandomKey.AesEncrypt(AuthConst.SecretKey);
        return new JsonResult(
            new OperationResult<(string Token, string RefreshToken)>().Succeeded(200, "Success",
                (Token, RefreshToken)));
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
        catch (Exception)
        {
            // _Logger.Error(ex);
            return new JsonResult(new OperationResult().Failed(500, _localizer["Error500"]));
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