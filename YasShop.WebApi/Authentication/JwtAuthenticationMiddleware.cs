using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace YasShop.WebApi.Authentication
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _CookieName;
        private readonly string _SecretKey;
        public JwtAuthenticationMiddleware(RequestDelegate next, string cookieName, string secretKey)
        {
            _next = next;
            _CookieName = cookieName;
            _SecretKey = secretKey;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            string _EncryptedToken = null;

            for (int i = 0; i <= 10; i++)
                if (context.Request.Cookies.Any(a => a.Key == _CookieName + i))
                    _EncryptedToken = context.Request.Cookies[_CookieName + i];

            if (_EncryptedToken != null)
                context.Request.Headers.Add("Authorization", _EncryptedToken.AesDecrypt(_SecretKey));

            await _next(context);
        }
    }
}
