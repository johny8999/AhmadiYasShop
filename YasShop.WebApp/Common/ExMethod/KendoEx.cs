using Framework.Application.Services.Localizer;
using Kendo.Mvc.UI.Fluent;

namespace YasShop.WebApp.Common.ExMethod
{
    public static class KendoEx
    {
        public static GridBuilder<T> DefaultSettings<T>(this GridBuilder<T> Builder, ILocalizer Localizer) where T : class
        {
            return Builder.Pageable(a =>
             {
                 a.Messages(msg =>
                 {
                     msg.Display(Localizer["TelerikPageToFrom"]);
                     msg.Empty(Localizer["ItemNotFound"]);
                     msg.ItemsPerPage(Localizer["TelerikItemsPerPage"]);
                     msg.Of(Localizer["TelerikOf"]);
                     msg.MorePages(Localizer["TelerikMorePages"]);
                     msg.Refresh(Localizer["Refresh"]);
                     msg.Previous(Localizer["Previous"]);
                     msg.Next(Localizer["Next"]);
                     msg.First(Localizer["First"]);
                     msg.Last(Localizer["Last"]);
                     msg.Page(Localizer["Page"]);
                 });
                 a.AlwaysVisible(true);
                 a.ButtonCount(5);
                 a.Input(false);
                 a.PreviousNext(true);
                 a.Responsive(true);
                //a.Numeric(true);
            });
        }
    }
}
