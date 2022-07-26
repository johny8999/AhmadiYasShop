using Framework.Common.Utilities.Downloader;
using Framework.Common.Utilities.Downloader.Dto;
using Framework.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YasShop.WebApp.TagHelpers
{
    [HtmlTargetElement("LoadComponet")]
    public class LoadComponentTagHelper : TagHelper
    {
        private readonly IDownloader _Downloader;

        public LoadComponentTagHelper(IDownloader downloader)
        {
            _Downloader = downloader;
        }
        #region Inputs
        public string Id { get; set; }
        public string Class { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public HttpContext HttpContext { get; set; }
        #endregion Inputs

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            if (string.IsNullOrEmpty(Url))
                throw new ArgumentNullException(nameof(Url), "Url can not be null");

            ArgumentNullException.ThrowIfNull(HttpContext, "Context can not be null");


            Url = SiteSettingConst.SiteUrl + Url;
            string HtmlData = await _Downloader.GetHtmlForPageAsync(new InpGetHtmlForPage
            {
                PageUrl = Url,
                Data = Data,
                Headers = HttpContext.Request.Headers.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)).ToDictionary(k => k.Key, v => v.Value)
            });

            if (string.IsNullOrEmpty(HtmlData))
                throw new ArgumentNullException(nameof(HtmlData), "Sorce data is null");

            output.TagName = "div";

            if (Id is not null)
                output.Attributes.SetAttribute("id", Id);

            if (Class is not null)
                output.Attributes.SetAttribute("class", Class);

            output.Content.SetHtmlContent(HtmlData);
        }
    }

}
