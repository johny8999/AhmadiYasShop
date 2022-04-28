using Framework.Const;
using Microsoft.AspNetCore.Http;

namespace YasShop.WebApp.Common.ExMethod
{
    public static class ResponseEx
    {
        public static void DeleteAuthCookie(this HttpResponse _Response)
        {
            _Response.Cookies.Delete("AspNetCore.Identity.Application");

            for (int i = 1; i <= 10; i++)
            {
                _Response.Cookies.Delete("AspNetCore.Identity.ApplicationC" + i);
                _Response.Cookies.Delete(AuthConst.CookieName + i);
            }
        }
    }
}
