using YasShop.WebApp.Common.Types;

namespace YasShop.WebApp.Common.Utilities.MessageBox
{
    public interface IMsgBox
    {
        JsResult AccessDeniedMsg(string _CallBackFunc = null);
        JsResult FailDefultMsg(string _CallBackFunc = null);
        JsResult FailMsg(string _Message, string _CallBackFunc = null);
        JsResult InfoMsg(string _Message, string _CallBackFunc = null);
        JsResult ModelStateMsg(string _ModelError, string _CallBackFunc = null);
        JsResult SucssessDefultMsg(string _CallBackFunc = null);
        JsResult SucssessMsg(string _Message, string _CallBackFunc = null);
    }
}