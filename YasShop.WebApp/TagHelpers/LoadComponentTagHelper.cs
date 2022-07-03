using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace YasShop.WebApp.TagHelpers
{
    [HtmlTargetElement("LoadComponet")]
    public class LoadComponentTagHelper:TagHelper
    {
        public string Url { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            
           
        }
    }
}
