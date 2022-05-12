﻿using Framework.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace YasShop.WebApp.Authentication
{
    public static class ConfiguegIdentityJWT
    {
        //describe about token
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            //for logIn
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;
                a.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConst.SecretCode)),// a key for coding signigure
                    RequireExpirationTime = true, //mandatory expire time in token
                    ValidateLifetime = true, //cheking expire time
                    ValidateAudience = true,
                    ValidAudience = AuthConst.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = AuthConst.Issuer
                };
            });
        }

        //use token
        public static void UseJWTAuthentication(this IApplicationBuilder app,string SecretKey,string CookieName)
        {
            app.UseMiddleware<JwtAuthenticationMiddleware>(CookieName, SecretKey);
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
