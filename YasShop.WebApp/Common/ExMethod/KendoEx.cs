using Framework.Application.Services.Localizer;
using Kendo.Mvc.UI.Fluent;

namespace YasShop.WebApp.Common.ExMethod
{
    public static class KendoEx
    {
        public static GridBuilder<T> DefaultSettings<T>(this GridBuilder<T> Builder,ILocalizer Localizer) where T : class
        {

        }
    }
}
