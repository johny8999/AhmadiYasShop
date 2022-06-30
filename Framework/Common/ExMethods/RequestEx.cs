using Microsoft.AspNetCore.Http;
using System.Net;

namespace Framework.Common.ExMethods
{
    public static class RequestEx
    {
        public static string GetCurrentEncodedUrl(this HttpRequest Request)
        {
            return WebUtility.UrlEncode(GetCurrentUrl(Request));
        }
        public static string GetCurrentUrl(this HttpRequest Request)
        {
            
            string Url = Request.Scheme + "://" + Request.Host + "/" + Request.Path;

            if (Request.QueryString.HasValue)
                Url += "?" + Request.QueryString.Value;

            return Url;
        }
    }
}
