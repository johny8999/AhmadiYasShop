using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace YasShop.WebApp.TagHelpers
{
    [HtmlTargetElement("LoadComponet")]
    public class LoadComponentTagHelper:TagHelper
    {
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            return base.ProcessAsync(context, output);
        }
    }
}
