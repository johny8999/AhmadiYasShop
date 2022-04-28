using Microsoft.AspNetCore.Mvc;

namespace YasShop.WebApp.Common.Types
{
    public class JsResult:ContentResult
    {
        public JsResult(string Script)
        {
            Content = Script;
            ContentType = "application/javascript";
        }
    }
}
