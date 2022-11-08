using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;

namespace YasShop.WebApp.Middlewares;

public class RefreshtTokenMiddleWare
{
    private readonly RequestDelegate _next;

    public RefreshtTokenMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var _JwtBuilder = context.RequestServices.GetService<IJwtBuilder>();
            string UserId = context.User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(b => b.Value).SingleOrDefault();
            string Token = await _JwtBuilder.CreateTokenAsync(UserId);
            context.Response.CreateAuthCookies(Token, true);
        }
        await _next(context);
    }
}
