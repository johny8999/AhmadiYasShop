using System.Threading.Tasks;

namespace YasShop.Infrastructure.Seed.Base.Languages
{
    public interface ISeed_Language
    {
        Task<bool> RunAsync();
    }
}