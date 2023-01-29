using Framework.Application.Exceptions;
using Framework.Application.Services.Email;
using Framework.Application.Services.Localizer;
using Framework.Application.Services.SMS;
using Framework.Common.ExMethods;
using Framework.Const;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YasShop.Application.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.AccessLevel;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Application.Contracts.ApplicationDTO.Users;
using YasShop.Application.Contracts.Endpoint.input;
using YasShop.Application.Contracts.Endpoint.Output;
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
        private readonly ISmsSender _SmsSender;

        public UserApplication(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer,
            IUserRepository userRepository, IAccessLevelApplication accessLevelApplication, IEmailSender emailSender, ISmsSender smsSender)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
            _UserRepository = userRepository;
            _AccessLevelApplication = accessLevelApplication;
            _EmailSender = emailSender;
            _SmsSender=smsSender;
        }

        public async Task<tblUsers> FindByIdAsync(string userId)
        {
            return await _UserRepository.FindByIdAsync(userId);
        }
        public async Task<tblUsers> FindByEmailAsync(string Email)
        {
            return await _UserRepository.FindByEmailAsync(Email);
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
                        return new OperationResult().Succeeded();
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

                        return new OperationResult().Succeeded(user.Id.ToString());
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
                return new OperationResult().Succeeded("Email Confirmation has been Succssed");
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

                return new OperationResult().Succeeded();
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

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> LoginByEmailPasswordAsync(InpLoginByEmailPassword Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                var qUser = await _UserRepository.FindByEmailAsync(Input.Email);
                if (qUser is null)
                    return new OperationResult().Failed(_Localizer["Username or password is incorect"]);

                var _Result = await LoginAsync(new InpLogin() { UserId = qUser.Id.ToString(), Password = Input.Password });
                if (_Result.IsSuccess)
                    return _Result;
                else
                    return new OperationResult().Failed(_Result.Message);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex.Message);
                return new OperationResult().Succeeded(ex.Message);
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
                    return new OperationResult().Succeeded(qUser.Id.ToString());

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

        public async Task<OutIGetAllDetailsForUser> GetAllDetailsForUserAsync(InpGetAllDetailsForUser Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                return await _UserRepository.GetNoTraking.Where(a => a.Id == Input.UserId.ToGuid()).Select(a => new OutIGetAllDetailsForUser()
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

        public async Task<OperationResult> ForgetPasswordAsync(InpForgetPassword Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region CheckUser
                tblUsers qUser = null;
                {
                    qUser = await _UserRepository.FindByEmailAsync(Input.Email);

                    if (qUser is null)
                        return new OperationResult().Failed("Email not found");

                    //if(!qUser.EmailConfirmed)
                    //    return new OperationResult().Failed("Email not found");

                    if (!qUser.IsActive)
                        return new OperationResult().Failed("Email not found");

                }
                #endregion CheckUser

                #region GenerateToken
                string Token = null;
                {
                    Token = await _UserRepository.GeneratePasswordResetTokenAsync(qUser);
                    Token = qUser.Id +", "+Token;
                    Token = Token.AesEncrypt(AuthConst.SecretKey);
                    Token= WebUtility.UrlEncode(Token);
                }
                #endregion GenerateToken

                #region GenerateLink
                string GenerateLink = null;
                {
                    GenerateLink = Input.ForgetPasswordUrl.Replace("[TOKEN]", Token);
                }
                #endregion GenerateLink

                #region Generate Email Template
                string GenerateEmailTemplate = "<a href=\"[Link]\">ClickMe</a>";//TODO : Create email Eemplate table and from database
                {
                    GenerateEmailTemplate = GenerateEmailTemplate.Replace("[Link]", GenerateLink);
                }
                #endregion Generate Email Template

                #region SendEmail
                {
                    await _EmailSender.SendAsync(qUser.Email, AuthConst.Issuer + _Localizer["PasswordRecovery"], GenerateEmailTemplate);
                }
                #endregion SendEmail
                return new OperationResult().Succeeded("Email has been sent and you should click link");
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

        public async Task<OperationResult> ResetPasswordAsync(InpResetPassword Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region Token decrypte
                string Token = null;
                string UserId = null;
                {
                    Token = Input.Token.AesDecrypt(AuthConst.SecretKey);
                    UserId = Token.Split(", ")[0];
                    Token = Token.Split(", ")[1];
                }
                #endregion Token decrypte

                #region Get user
                tblUsers qUser = null;
                {
                    qUser =await _UserRepository.FindByIdAsync(UserId);
                }
                #endregion Get user

                #region Reset password
                {
                    var _Result = await _UserRepository.ResetPasswordAsync(qUser, Token, Input.Password);
                    if (_Result.Succeeded)
                        return new OperationResult().Succeeded(_Localizer["Your password has been changed successly"]);

                    else
                        return new OperationResult().Failed(String.Join(',', _Result.Errors.Select(a => a.Description)));
                }
                #endregion Reset password
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

        public async Task<OperationResult> LoginByPhoneNumberStep1Async(InpLoginByPhoneNumberStep1 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region Fatch User and Validations
                tblUsers tUser;
                {
                    tUser = await _UserRepository.Get
                                            .Where(a => a.PhoneNumber == Input.PhoneNumber)
                                            //.Where(a => a.PhoneNumberConfirmed)
                                            .SingleOrDefaultAsync();

                    if (tUser == null)
                    {
                        tUser = new tblUsers
                        {
                            Id=new Guid().SequentialGuid(),
                            PhoneNumber= Input.PhoneNumber,
                            Date=DateTime.Now,
                            Email=null,
                            EmailConfirmed=false,
                            FullName="",
                            IsActive=true,
                            PhoneNumberConfirmed=false,
                            NormalizedEmail=null,
                            UserName=Input.PhoneNumber,
                            NormalizedUserName = Input.PhoneNumber.ToUpper(),
                            OTPData=null,
                            PasswordHash=null,
                            AccessLevelId = (await _AccessLevelApplication.GetIdByNameAsync(new InpGetIdByName()
                            {
                                Name = "NotConfirmedUser"
                            })).ToGuid(),
                        };

                        await _UserRepository.AddAsync(tUser, true);
                    }

                    //else if (tUser.PhoneNumberConfirmed == false)
                    //    return new OperationResult().Failed("User not found");

                }
                #endregion

                #region Create Otp Code
                string _OTPCode = "";
                string _EncryptedData = "";
                {
                    _OTPCode = new Random().Next(1000, 9999).ToString();

                    var _DesrializedJsonData = new OutOTPData
                    {
                        otpCode = _OTPCode.MD5(),
                        dateCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        dateExpired = DateTime.Now.AddMinutes(5).ToString("yyyy/MM/dd HH:mm:ss")
                    };

                    var _SerializedJsonData = System.Text.Json.JsonSerializer.Serialize(_DesrializedJsonData);

                    _EncryptedData = _SerializedJsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                #region Update User
                {
                    tUser.OTPData = _EncryptedData;

                    await _UserRepository.UpdateAsync(tUser);
                }
                #endregion

                #region Send Sms
                {
                    var _Result = _SmsSender.SendLoginCode(tUser.PhoneNumber, _OTPCode);
                    if (_Result == false)
                        return new OperationResult().Failed("سرویس دهنده SMS پاسخگو نیست.");
                }
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult<string>> LoginByPhoneNumberStep2Async(InpLoginByPhoneNumberStep2 Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region Fatch User and Validations
                tblUsers tUser = new();
                {
                    tUser = await _UserRepository.Get
                                            .Where(a => a.PhoneNumber == Input.PhoneNumber)
                                            //.Where(a => a.PhoneNumberConfirmed)
                                            .SingleOrDefaultAsync();

                    if (tUser == null)
                        return new OperationResult<string>().Failed("User not found");

                    //if(tUser.PhoneNumberConfirmed == false)
                    //    return new OperationResult().Failed("User not found");
                }
                #endregion

                #region Check Otp code
                {
                    var _DecryptedData = tUser.OTPData.AesDecrypt(AuthConst.SecretKey);

                    var _DeserializedData = System.Text.Json.JsonSerializer.Deserialize<OutOTPData>(_DecryptedData);

                    if (DateTime.Parse(_DeserializedData.dateExpired) < DateTime.Now)
                        return new OperationResult<string>().Failed("کد وارد شده منقضی شده است.");

                    if (_DeserializedData.otpCode != Input.OTPCode.MD5())
                        return new OperationResult<string>().Failed("کد وارد شده صحیح نمیباشد.");
                }
                #endregion

                #region PhoneNumber Confirm
                {
                    if (tUser.PhoneNumberConfirmed == false)
                    {
                        tUser.PhoneNumberConfirmed = true;
                        await _UserRepository.UpdateAsync(tUser, true);
                    }
                }
                #endregion

                return new OperationResult<string>().Succeeded(tUser.Id.ToString());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<string>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<string>().Failed("Error500");

            }
        }

        public async Task<OperationResult> ResendOtpCodeAsync(InpResendOtpCode Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region Fatch User and Validations
                tblUsers tUser = new();
                {
                    tUser = await _UserRepository.Get
                                            .Where(a => a.PhoneNumber == Input.PhoneNumber)
                                            //.Where(a => a.PhoneNumberConfirmed)
                                            .SingleOrDefaultAsync();

                    if (tUser == null)
                        return new OperationResult<string>().Failed("User not found");

                    //if(tUser.PhoneNumberConfirmed == false)
                    //    return new OperationResult().Failed("User not found");
                }
                #endregion

                #region Check Expire Data
                {
                    var _DecryptedData = tUser.OTPData.AesDecrypt(AuthConst.SecretKey);

                    var _DeserializedData = System.Text.Json.JsonSerializer.Deserialize<OutOTPData>(_DecryptedData);

                    if (DateTime.Parse(_DeserializedData.dateCreated).AddMinutes(2) >= DateTime.Now)
                        return new OperationResult().Failed("برای ارسال مجدد کد، 2 دقیقه صبر نمایید");
                }
                #endregion

                #region Create Otp Code
                string _OTPCode = "";
                string _EncryptedData = "";
                {
                    _OTPCode = new Random().Next(1000, 9999).ToString();

                    var _DesrializedJsonData = new OutOTPData
                    {
                        otpCode = _OTPCode.MD5(),
                        dateCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        dateExpired = DateTime.Now.AddMinutes(5).ToString("yyyy/MM/dd HH:mm:ss")
                    };

                    var _SerializedJsonData = System.Text.Json.JsonSerializer.Serialize(_DesrializedJsonData);

                    _EncryptedData = _SerializedJsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                #region Update User
                {
                    tUser.OTPData = _EncryptedData;

                    await _UserRepository.UpdateAsync(tUser);
                }
                #endregion

                #region Send Sms
                {
                    var _Result = _SmsSender.SendLoginCode(tUser.PhoneNumber, _OTPCode);
                    if (_Result == false)
                        return new OperationResult().Failed("سرویس دهنده SMS پاسخگو نیست.");
                }
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> ChangeUserAccLevelAsync(InpChangeUserAccLevel Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion Validation

                #region Fetch User
                tblUsers tUsers;
                {
                    tUsers = await _UserRepository.Get.Where(a => a.Id == Input.UserId.ToGuid()).SingleOrDefaultAsync();
                    if (tUsers == null)
                        return new OperationResult().Failed("User not found");

                }
                #endregion

                #region Check AccLevel Exsit
                {
                    var _Result = await _AccessLevelApplication.CheckExistAccLevelAsync(new InpCheckExistAccLevel { AccLevelId= Input.AccLevelId });
                    if (_Result == false)
                        return new OperationResult().Failed("AccLevel Not Found");
                }
                #endregion

                #region Update User
                {

                }
                #endregion

                #region Update UserRoles
                {

                }
                #endregion

                return new OperationResult().Succeeded();
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
    }
}
