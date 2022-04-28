using System.Threading.Tasks;

namespace YasShop.Infrastructure.Seed.Base.AccessLevel;

public interface ISeed_AccessLevel
{
    Task<bool> RunAsync();
}
