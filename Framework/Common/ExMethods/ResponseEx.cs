using Framework.Const;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace Framework.Common.ExMethods
{
    public static class ResponseEx
    {
        public static HttpContext CreateAuthCookies(this HttpResponse Response, string AuthToken, bool RememberMe)
        {
            #region Remove cookies
            {
                Response.Cookies.Delete(".AspNetCore.Identity.Application");
                for (int i = 1; i <= 10; i++)
                {
                    Response.Cookies.Delete(".AspNetCore.Identity.ApplicationC" + i);
                    Response.Cookies.Delete(AuthConst.CookieName + i);
                }
            }
            #endregion Remove cookies

            #region Add New Cookies
            {
                int _CookieLimitCount = 2000;
                int _Counter = 0;
                while (AuthToken != null)
                {
                    if (AuthToken.Length > _CookieLimitCount)
                    {
                        string Section = AuthToken.Substring(0, _CookieLimitCount);
                        AuthToken = AuthToken.Remove(0, _CookieLimitCount);

                        Response.Cookies.Append(AuthConst.CookieName + _Counter, Section,
                            RememberMe ? new CookieOptions { Expires = DateTime.Now.AddHours(48) } : new CookieOptions());
                    }
                    else
                    {
                        Response.Cookies.Append(AuthConst.CookieName + _Counter, AuthToken,
                             RememberMe ? new CookieOptions { Expires = DateTime.Now.AddHours(48) } : new CookieOptions());

                        AuthToken = null;
                    }
                    _Counter++;
                }
            }
            #endregion Add New Cookies
        }
    }
}
