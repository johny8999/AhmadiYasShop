using System.Threading.Tasks;

namespace YasShop.Infrastructure.Seed.Base.Main
{
    public interface ISeed_main
    {
        Task<bool> RunAsync();
    }
}