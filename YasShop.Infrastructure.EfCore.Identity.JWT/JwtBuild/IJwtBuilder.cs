using System.Threading.Tasks;

namespace YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild
{
    public interface IJwtBuilder
    {
        Task<string> CreateTokenAsync(string UserId);
    }
}