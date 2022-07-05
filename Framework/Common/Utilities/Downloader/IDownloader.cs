using Framework.Common.Utilities.Downloader.Dto;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Downloader
{
    public interface IDownloader
    {
        Task<string> GetHtmlForPageAsync(InpGetHtmlForPage Input);
    }
}