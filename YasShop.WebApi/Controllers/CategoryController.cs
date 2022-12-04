using Framework.Application.Exceptions;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.Endpoint.Input;
using YasShop.Application.Users;
using YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;

namespace YasShop.WebApi.Controllers
{
    [ApiController]
    [Route("/Category")]
    public class CategoryController : Controller
    {
        private readonly ILogger _Logger;
        private readonly ICate _userApplication;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILocalizer _localizer;

        public CategoryController(ILogger logger, IUserApplication userApplication, IJwtBuilder jwtBuilder, IServiceProvider serviceProvider, ILocalizer localizer)
        {
            _Logger=logger;
            _userApplication=userApplication;
            _jwtBuilder=jwtBuilder;
            _serviceProvider=serviceProvider;
            _localizer=localizer;
        }

        public async Task<JsonResult> GetListCategoryAsync(InpGetListCategory Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_serviceProvider) ;
                }
                #endregion

                var qData = await _
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new JsonResult(new OperationResult().Failed(ex.Message));
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new JsonResult(new OperationResult().Failed("Error500"));
            }
        }
    }
}
