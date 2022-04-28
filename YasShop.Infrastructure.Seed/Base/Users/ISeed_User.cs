using System.Threading.Tasks;

namespace YasShop.Infrastructure.Seed.Base.Users
{
    public interface ISeed_User
    {
        Task<bool> RunAsync();
    }
}