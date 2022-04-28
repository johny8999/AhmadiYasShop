using Framework.Application.Exceptions;
using Framework.Application.Services.Email;
using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Framework.Const;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Domain.Users.UserAgg.Contracts;
using YasShop.Domain.Users.UserAgg.Entities;

namespace YasShop.Application.Users
{
    public class UserApplication : IUserApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IUserRepository _UserRepository;
        private readonly IAccessLevelApplication _AccessLevelApplication;
        private readonly IEmailSender _EmailSender;


        public UserApplication(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer,
            IUserRepository userRepository, IAccessLevelApplication accessLevelApplication, IEmailSender emailSender)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
            _UserRepository = userRepository;
            _AccessLevelApplication = accessLevelApplication;
            _EmailSender = emailSender;
        }

        public async Task<tblUsers> FindByIdAsync(string userId)
        {
            return await _UserRepository.FindByIdAsync(userId);
        }
        public async Task<OperationResult> RegisterByEmailPasswordAsync(InpRegisterByEmailPassword Input)
        {

            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion Validations

                #region Register
                {
                    var _result = await RegisterAsync(new InpRegister()
                    {
                        Email = Input.Email,
                        FullName = Input.FullName,
                        Password = Input.Password
                    });

                    if (_result.IsSuccess)
                    {
                        #region Send Confirmation Email
                        {
                            #region Generate Confirmation Link
                            string GeneratedLink = "";
                            {
                                var user = await _UserRepository.FindByIdAsync(_result.Message);
                                var code = await _UserRepository.GenerateEmailConfirmationTokenAsync(user);

                                string EncryptedToken = $"{_result.Message},{code}".AesEncrypt(AuthConst.SecretKey);
                                EncryptedToken = WebUtility.UrlEncode(EncryptedToken);
                                //EncryptedToken = WebUtility.UrlEncode(EncryptedToken);

                                GeneratedLink = Input.ConfirmationLinkTemplate.Replace("[TOKEN]", EncryptedToken);
                            }
                            #endregion Generate Confirmation Link

                            #region Generate Email Template
                            string ConfirmationEmailTemplate = "<a href='[Link]'>کلیک کنید</a>";//TODO : Reading from database
                            {
                                ConfirmationEmailTemplate = ConfirmationEmailTemplate.Replace("[Link]", GeneratedLink);
                            }
                            #endregion Generate Email Template

                            #region Send Email
                            {
                                await _EmailSender.SendAsync(Input.Email, "YasShop" + _Localizer["EmailConfirm"], ConfirmationEmailTemplate);
                            }
                            #endregion Send Email
                        }
                        #endregion SendConfirmationEmail
                        return new OperationResult().Successed();
                    }
                    else
                    {
                        return new OperationResult().Failed(_result.Message);
                    }
                }

                #endregion Register
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<OperationResult> RegisterAsync(InpRegister Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion Validations

                #region Register New Users
                {
                    tblUsers user = new()
                    {
                        Email = Input.Email,
                        FullName = Input.FullName,
                        Date = DateTime.Now,
                        IsActive = true,
                        UserName = Input.Email.Split('@')[0] + new Random().Next(1000, 9999),
                        AccessLevelId = (await _AccessLevelApplication.GetIdByNameAsync
                            (new InpGetIdByName() { Name = "NotConfirmedUser" })).ToGuid(),
                    };

                    var Result = await _UserRepository.AddAsync(user, Input.Password);
                    if (Result.Succeeded)
                    {

                        return new OperationResult().Successed(user.Id.ToString());
                    }
                    else
                    {

                        return new OperationResult().Failed(string.Join(',', Result.Errors.Select(a => a.Description)));
                    }
                }
                #endregion Register New Users
            }
            catch (ArgumentException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(_Localizer["Error500"]);
            }
        }

        public async Task<OperationResult> EmailConfirmationAsync(InpEmailConfirmation Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region Decrypted Token
                string DecryptedToken = Input.Token.AesDecrypt(AuthConst.SecretKey);
                string UserId = DecryptedToken.Split(',')[0];
                string Token = DecryptedToken.Split(',')[1];
                #endregion Decrypted Token

                #region Find User
                tblUsers qUser = null;
                {
                    qUser = await _UserRepository.FindByIdAsync(UserId);
                    if (qUser is null)
                        return new OperationResult().Failed("Token is invalid");
                }
                #endregion Find User

                #region Confirm Email
                {
                    var Result = await _UserRepository.ConfirmEmailAsync(qUser, Token);
                    if (!Result.Succeeded)
                        return new OperationResult().Failed(String.Join(',', Result.Errors));
                }
                #endregion Confirm Email

                #region Change user access level to confirmedUser
                {
                    var ConfirmAccessLevelId = await _AccessLevelApplication.GetAccessLevelIdbyNameAsync(new InpGetAccessLevelIdbyName()
                    {
                        AccessLevelName = "ConfirmedUser"
                    });

                    await ChangeUserAccessLevelAsync(new InpChangeUserAccessLevel()
                    {
                        UserId = UserId,
                        AccessLevelId = ConfirmAccessLevelId
                    });
                }
                #endregion Change user access level to confirmedUser
                return new OperationResult().Successed("Email Confirmation has been Succssed");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(_Localizer["Error500"]);
            }

        }

        public async Task<OperationResult> ChangeUserAccessLevelAsync(InpChangeUserAccessLevel Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion Validations

                #region Get user
                tblUsers qUser = null;
                {
                    qUser = await _UserRepository.FindByIdAsync(Input.UserId);
                    if (qUser is null)
                        return new OperationResult().Failed("The user has not been finded");
                }
                #endregion Get user

                #region Change user access level
                {
                    qUser.AccessLevelId = Input.AccessLevelId.ToGuid();
                    await _UserRepository.UpdateAsync(qUser);
                }
                #endregion Change user access level

                #region Change user role
                {
                    var _Result = await ChangeUserRoleByAccessLevelIdAsync(new InpChangeUserRoleByAccessLevelId()
                    {
                        AccessLevelId = Input.AccessLevelId,
                        UserId = Input.UserId,
                    });

                    if (!_Result.IsSuccess)
                        return new OperationResult().Failed(_Localizer[_Result.Message]);

                }
                #endregion Change user role

                return new OperationResult().Successed();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
        public async Task<OperationResult> ChangeUserRoleByAccessLevelIdAsync(InpChangeUserRoleByAccessLevelId Input)
        {
            try
            {
                #region Get User
                tblUsers tUser = null;
                {
                    tUser = await _UserRepository.FindByIdAsync(Input.UserId);
                }
                #endregion Get User

                #region Remove old role
                {
                    var qOldRoles = await _UserRepository.GetRolesAsync(tUser);
                    var _Result = await _UserRepository.RemoveFromRolesAsync(tUser, qOldRoles);
                    if (!_Result.Succeeded)
                        return new OperationResult().Failed(String.Join(',', _Result.Errors.Select(a => a.Description)));
                }
                #endregion Remove old role

                #region Add new roles
                {
                    var qNewRoles = await _AccessLevelApplication.GetUserRoleByAccessId(new InpGetUserRoleByAccessId()
                    {
                        AccessLevelId = Input.AccessLevelId
                    });
                    var _Result = await _UserRepository.AddToRolesAsync(tUser, qNewRoles);
                    if (!_Result.Succeeded)
                        return new OperationResult().Failed(String.Join(',', _Result.Errors.Select(a => a.Description)));
                }
                #endregion Add new roles

                return new OperationResult().Successed();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> LoginByEmailPasswordAsync(InpLoginByEmailPassword input)
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation

                var qUser = await _UserRepository.FindByEmailAsync(input.Email);
                if (qUser is null)
                    return new OperationResult().Failed(_Localizer["Username or password is incorect"]);
                var _Result = await LoginAsync(new InpLogin() { UserId = qUser.Id.ToString(), Password = input.Password });
                if (_Result.IsSuccess)
                    //TODO :JWT
                    return default;
                else
                    return new OperationResult().Failed(_Result.Message);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return new OperationResult().Successed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(_Localizer["Error500"]);
            }
        }

        private async Task<OperationResult> LoginAsync(InpLogin input)
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation

                var qUser = await _UserRepository.FindByIdAsync(input.UserId);
                if (qUser is null)
                    return new OperationResult().Failed(_Localizer["Username or password is incorect"]);

                if (qUser.IsActive == false)
                    return new OperationResult().Failed(_Localizer["your Account is disable"]);

                var _Result = await _UserRepository.PasswordSignInAsync(qUser, input.Password, false, true);

                if (_Result.Succeeded)
                {
                    return new OperationResult().Successed(qUser.Id.ToString());
                }
                else
                {
                    if (_Result.IsLockedOut)
                        return new OperationResult().Failed(_Localizer["your Account is lockedOut"]);
                    else if (_Result.IsNotAllowed)
                        return new OperationResult().Failed(_Localizer["Username or password is incorect"]);
                    else
                        return new OperationResult().Failed(_Localizer["Username or password is incorect"]);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutIGetAllDetailsForUser> GetAllDetailsForUserAsync(InpGetAllDetailsForUser input)
        {
            try
            {
                #region Validation
                input.CheckModelState(_ServiceProvider);
                #endregion Validation

                return await _UserRepository.GetNoTraking.Where(a => a.Id == input.UserId.ToGuid()).Select(a => new OutIGetAllDetailsForUser()
                {
                    Id = a.Id.ToString(),
                    UserName = a.UserName,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    FullName = a.FullName,
                    AccessLevelId = a.AccessLevelId.ToString(),
                    Date = a.Date,
                    IsActive = a.IsActive,
                    AccessLevelTitle = a.tblAccessLevel.Name

                }).SingleOrDefaultAsync();
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
