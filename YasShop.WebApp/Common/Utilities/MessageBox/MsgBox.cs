using Framework.Application.Services.Localizer;
using Microsoft.VisualBasic;
using YasShop.WebApp.Common.Types;

namespace YasShop.WebApp.Common.Utilities.MessageBox
{
    public class MsgBox : IMsgBox
    {
        private readonly ILocalizer _localizer;

        public MsgBox(ILocalizer localizer)
        {
            _localizer = localizer;
        }

        private string Show(string _Title, string _Message, MsgBoxType _Type, string _OkBtnText = "OK", string _CallBackFunc = null)
        {
            _CallBackFunc = _CallBackFunc ?? "return '';";
            string Js = $@"swal.fire({{
                                         title: '{_Title.Replace("'", "`")}',
                                         html: '{_Message.Replace("'", "`").Replace("\r\n", "")}',
                                         icon: '{_Type}',
                                         confirmButtonText: '{_OkBtnText}',
                                     }}).then((result) => {{
                                         if (result.isConfirmed) {{
                                            {_CallBackFunc}
                                         }}
                                     }});";
            return Js;
        }
        public JsResult SucssessMsg(string _Message, string _CallBackFunc = null)
        {
            return new JsResult(Show("", _Message, MsgBoxType.sucssess, _localizer["OK"], _CallBackFunc));
        }

        public JsResult SucssessDefultMsg(string _CallBackFunc = null)
        {
            return new JsResult(Show("", _localizer["TheOperationWasSuccessfull"], MsgBoxType.sucssess, _localizer["OK"], _CallBackFunc));
        }

        public JsResult ModelStateMsg(string _ModelError, string _CallBackFunc = null)
        {
            return new JsResult(Show("", _ModelError, MsgBoxType.error, _localizer["OK"], _CallBackFunc));
        }

        public JsResult FailMsg(string _Message, string _CallBackFunc = null)
        {
            return new JsResult(Show("", _Message, MsgBoxType.error, _localizer["OK"], _CallBackFunc));
        }

        public JsResult FailDefultMsg(string _CallBackFunc = null)
        {
            return new JsResult(Show("", _localizer["Error500"], MsgBoxType.error, _localizer["OK"], _CallBackFunc));
        }

        public JsResult InfoMsg(string _Message, string _CallBackFunc = null)
        {
            return new JsResult(Show("", _Message, MsgBoxType.info, _localizer["OK"], _CallBackFunc));
        }

        public JsResult AccessDeniedMsg(string _CallBackFunc = null)
        {
            return new JsResult(Show("", _localizer["AccessDeniedMsg"], MsgBoxType.error, _localizer["OK"], _CallBackFunc));
        }
    }



    public enum MsgBoxType
    {
        sucssess,
        error,
        warning,
        info
    }
}
