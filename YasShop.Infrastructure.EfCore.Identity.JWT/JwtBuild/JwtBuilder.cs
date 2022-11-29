using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Const;
using Framework.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Role;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Roles;
using YasShop.Application.Users;

namespace YasShop.Infrastructure.EfCore.Identity.JWT.JwtBuild
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly IUserApplication _UserApplication;
        private readonly IRoleApplication _RoleApplication;
        private readonly ILogger _Logger;

        public JwtBuilder(IUserApplication userApplication, IRoleApplication roleApplication, ILogger logger)
        {
            _UserApplication = userApplication;
            _RoleApplication = roleApplication;
            _Logger = logger;
        }

        public async Task<string> CreateTokenAsync(string UserId)
        {
            try
            {
                #region Validation

                #endregion Validation

                #region GetUser
                OutIGetAllDetailsForUser _User = null;
                {
                    _User = await _UserApplication.GetAllDetailsForUserAsync(new InpGetAllDetailsForUser()
                    {
                        UserId = UserId
                    });

                    if (_User == null)
                        throw new ArgumentInvalidException("UserId is invalid");
                }
                #endregion GetUser

                #region GetRole
                List<string> _Roles = null;
                {
                    _Roles = await _RoleApplication.GetRoleNameByUserIdAsync(new InpGetRoleNameByUserId()
                    {
                        UserId = UserId
                    });

                    if (_Roles == null)
                        throw new ArgumentInvalidException("UserId is invalid");
                }
                #endregion GetRole

                #region Claim list
                List<Claim> Claims = new();
                {

                    Claims.AddRange(new List<Claim>{
                        new Claim(ClaimTypes.NameIdentifier, _User.Id.ToString()),
                        new Claim(ClaimTypes.Name, _User.Email?? _User.PhoneNumber),
                        new Claim(ClaimTypes.Email, _User.Email??""),
                        new Claim(ClaimTypes.MobilePhone, _User.PhoneNumber ?? ""),
                        new Claim("AccessLevel", _User.AccessLevelTitle),
                        new Claim("Date", _User.Date.ToString("yyyy/mm/dd", new CultureInfo("en-us"))),
                        new Claim(ClaimTypes.GivenName, _User.FullName??""),
                    });

                    Claims.AddRange(_Roles.Select(a => new Claim(ClaimsIdentity.DefaultRoleClaimType, a)));
                }
                #endregion Claim list

                #region Descriptor
                SecurityTokenDescriptor TokenDescriptor = null;
                {
                    var _key = Encoding.ASCII.GetBytes(AuthConst.SecretCode); //Convert string to byte
                    TokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(Claims),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature),
                        Issuer = AuthConst.Issuer,
                        Audience = AuthConst.Audience,
                        IssuedAt = DateTime.Now,
                        Expires = DateTime.Now.AddDays(2)
                    };
                }
                #endregion Descriptor

                #region Generate token
                string _GeneratedToken = null;
                {
                    var _SecurityToken=new JwtSecurityTokenHandler().CreateToken(TokenDescriptor);
                    _GeneratedToken="Bearer "+new JwtSecurityTokenHandler().WriteToken(_SecurityToken);
                }
                #endregion Generate token

               return _GeneratedToken.AesEncrypt(AuthConst.SecretKey);

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
