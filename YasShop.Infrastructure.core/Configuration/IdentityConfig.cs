using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using YasShop.Domain.Users.RoleAgg.Entities;
using YasShop.Domain.Users.UserAgg.Entities;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.Core.Configuration
{
    public static class IdentityConfig
    {
        public static IdentityBuilder AddCustomIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<tblUsers, tblRoles>(a =>
            {
                a.SignIn.RequireConfirmedEmail = true;
                a.SignIn.RequireConfirmedAccount = true;

                a.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                a.Lockout.AllowedForNewUsers = true;
                a.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 15, 0);
                a.Lockout.MaxFailedAccessAttempts = 6;

                a.Password.RequireDigit = false;
                a.Password.RequireLowercase = false;
                a.Password.RequiredLength = 6;
                a.Password.RequiredUniqueChars = 0;
                a.Password.RequireNonAlphanumeric = false;
                a.Password.RequireUppercase = false;


            }).AddEntityFrameworkStores<MainContext>()
            .AddDefaultTokenProviders();

        }
    }
}
