using Framework.Common.ExMethods;
using Framework.Const;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YasShop.WebApp.Authentication
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string _EncryptedToken = null;

            for (int i = 1; i <= 10; i++)
                if (context.Request.Cookies.Any(a => a.Key == AuthConst.CookieName + i))
                    _EncryptedToken = context.Request.Cookies[AuthConst.CookieName] + i.ToString();

            if (_EncryptedToken != null)
                context.Request.Headers.Add("Authorization", _EncryptedToken.AesDecrypt(AuthConst.SecretKey));

            await _next(context);
        }
    }
}
