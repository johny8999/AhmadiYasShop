﻿using Framework.Application.Services.Email;
using Framework.Application.Services.IpList;
using Framework.Application.Services.SMS;
using Framework.Common.Utilities.Downloader;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YasShop.Application.AccessLevel;
using YasShop.Application.Common.ExMethod;
using YasShop.Application.Languages;
using YasShop.Application.Roles;
using YasShop.Application.UserRole;
using YasShop.Application.Users;
using YasShop.Domain.Region.LanguageAgg.Contract;
using YasShop.Domain.Users.AccessLevelAgg.Contract;
using YasShop.Domain.Users.RoleAgg.Contract;
using YasShop.Domain.Users.UserAgg.Contracts;
using YasShop.Infrastructure.EfCore.Config;
using YasShop.Infrastructure.EfCore.Context;
using YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild;
using YasShop.Infrastructure.EfCore.Repository.AccessLevel;
using YasShop.Infrastructure.EfCore.Repository.Languages;
using YasShop.Infrastructure.EfCore.Repository.Roles;
using YasShop.Infrastructure.EfCore.Repository.Users;
using YasShop.Infrastructure.Logger.SeriLoger;
using YasShop.Infrastructure.Seed.Base.AccessLevel;
using YasShop.Infrastructure.Seed.Base.Languages;
using YasShop.Infrastructure.Seed.Base.Main;
using YasShop.Infrastructure.Seed.Base.Roles;
using YasShop.Infrastructure.Seed.Base.Users;
using YasShop.Infrastructure.Services.Email;
using YasShop.Infrastructure.Services.SMS;

namespace YasShop.Infrastructure.Core.Configuration
{
    public static class Bootstraper
    {
        public static void Config(this IServiceCollection services)
        {
            services.AddCustomAutoMapper();

            #region Add Services
            {
                //Add Services
                services.AddDbContext<MainContext>(opt =>
                {
                    opt.UseSqlServer(ConnectionString.Get());
                });
                services.AddSingleton<ILogger, SeriLogger>();
                services.AddSingleton<IIPList, IPList>();
                services.AddSingleton<IEmailSender, GmailSender>();
                services.AddSingleton<IDownloader,Downloader>();
                services.AddScoped<IJwtBuilder, JwtBuilder>();
                services.AddScoped<ISmsSender, KavenegarSmsSender>();
            }
            #endregion Add Services

            //Add Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILanguagesRepository, LanguagesRepository>();
            services.AddScoped<IAccessLevelRepository, AccessLevelRepository>();
            services.AddScoped<IAccessLevelRoleRepository, AccessLevelRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            //Add Applications 
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ILanguagesApplication, LanguagesApplication>();
            services.AddScoped<IAccessLevelApplication, AccessLevelApplication>();
            services.AddScoped<IRoleApplication, RoleApplication>();
            services.AddScoped<IUserRoleApplication, UserRoleApplication>();

            //Seeds
            services.AddTransient<ISeed_Language, Seed_Language>();
            services.AddTransient<ISeed_AccessLevel, Seed_AccessLevel>();
            services.AddTransient<ISeed_main, Seed_main>();
            services.AddTransient<ISeed_User, Seed_User>();
            services.AddTransient<ISeed_Roles, Seed_Roles>();


        }
    }
}
