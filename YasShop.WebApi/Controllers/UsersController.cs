using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.Endpoint.input;
using YasShop.Application.Users;
using YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;

namespace YasShop.WebApi.Controllers
{
    [ApiController]
    [Route("/Users")]
    public class UsersController : Controller
    {
        private readonly ILogger _Logger;
        private readonly IUserApplication _userApplication;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILocalizer _localizer;

        public UsersController(ILogger logger, IUserApplication userApplication, IJwtBuilder jwtBuilder, IServiceProvider serviceProvider, ILocalizer localizer)
        {
            _Logger=logger;
            _userApplication=userApplication;
            _jwtBuilder=jwtBuilder;
            _serviceProvider=serviceProvider;
            _localizer=localizer;
        }

        public async Task<JsonResult> ChangeUserAccLevelAsync(InpChangeAccLevel Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_serviceProvider);
                }
                #endregion Validation


                return default;
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
    }
}
