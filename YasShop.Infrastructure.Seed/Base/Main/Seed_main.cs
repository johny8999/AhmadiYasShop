using Framework.Infrastructure;
using System;
using System.Threading.Tasks;
using YasShop.Infrastructure.Seed.Base.AccessLevel;
using YasShop.Infrastructure.Seed.Base.Languages;
using YasShop.Infrastructure.Seed.Base.Roles;
using YasShop.Infrastructure.Seed.Base.Users;

namespace YasShop.Infrastructure.Seed.Base.Main
{
    public class Seed_main : ISeed_main
    {
        private readonly ISeed_Language _Seed_Language;
        private readonly ISeed_AccessLevel _Seed_AccessLevel;
        private readonly ISeed_User _Seed_User;
        private readonly ISeed_Roles _Seed_Roles;
        private readonly ILogger _Logger;

        public Seed_main(ISeed_Language seed_Language, ILogger logger,
            ISeed_AccessLevel seed_AccessLevel, ISeed_User seed_User, ISeed_Roles seed_Roles)
        {
            _Seed_Language = seed_Language;
            _Logger = logger;
            _Seed_AccessLevel = seed_AccessLevel;
            _Seed_User = seed_User;
            _Seed_Roles = seed_Roles;
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                await _Seed_Language.RunAsync();
                await _Seed_AccessLevel.RunAsync();
                await _Seed_Roles.RunAsync();
                await _Seed_User.RunAsync();
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }
    }
}
