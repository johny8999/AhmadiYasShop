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
            if (context.Request.Cookies.Any(a => a.Key == AuthConst.CookieName))
            {
                string token = context.Request.Cookies[AuthConst.CookieName].ToString();
                context.Request.Headers.Add("Authorization", token);
            }
            await _next(context);
        }
    }
}
