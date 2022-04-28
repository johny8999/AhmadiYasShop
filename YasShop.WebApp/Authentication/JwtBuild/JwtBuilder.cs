using YasShop.Application.Users;

namespace YasShop.WebApp.Authentication.JwtBuild
{
    public class JwtBuilder: IJwtBuilder
    {
        private readonly IUserApplication _UserApplication;
        public JwtBuilder(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }
    }
}
