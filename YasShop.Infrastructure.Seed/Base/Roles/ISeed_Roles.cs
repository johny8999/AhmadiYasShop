using System.Threading.Tasks;

namespace YasShop.Infrastructure.Seed.Base.Roles
{
    public interface ISeed_Roles
    {
        Task<bool> RunAsync();
    }
}