using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Application.Contracts.ApplicationDTO.Languages;
using YasShop.Application.Contracts.ApplicationDTO.Result;
using YasShop.Domain.Region.LanguageAgg.Contract;
using YasShop.Domain.Region.LanguageAgg.Entities;

namespace YasShop.Application.Languages
{
    public class LanguagesApplication : ILanguagesApplication
    {
        private readonly ILanguagesRepository _languagesRepository;
        private readonly ILogger _Logger;
        private List<OutSiteLanguageCache> _SiteLangCache;
        private readonly IServiceProvider _ServiceProvider;

        public LanguagesApplication(ILanguagesRepository languagesRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _languagesRepository = languagesRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddLanguageAsync(InpAddLanguage Input)
        {
            try
            {

                if (Input is null)
                    throw new ArgumentNullException(nameof(Input));

                if (await CheckExistAsync(Input.Name, Input.NativeName, Input.Code, Input.Abbr))
                    return new OperationResult().Failed("LanguageIsDuplicate");


                await _languagesRepository.AddAsync(new tblLanguages()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = Input.Name,
                    NativeName = Input.NativeName,
                    IsActive = Input.IsActive,
                    Abbr = Input.Abbr,
                    Code = Input.Code,
                    IsRtl = Input.IsRtl,
                    UseForSiteLanguage = Input.UseForSideLanguage
                });

                return new OperationResult().Successed();
            }
            catch (ArgumentNullException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<bool> CheckExistAsync(string Name = null, string NativeName = null, string Code = null, string Abbr = null)
        {
            if (Name == null || NativeName == null || Code == null || Abbr == null)
                throw new ArgumentNullException("you must enter an argument");

            return await _languagesRepository.Get
                                                 .Where(a => Name != null ? a.Name == Name : true)
                                                 .Where(a => NativeName != null ? a.NativeName == NativeName : true)
                                                 .Where(a => Code != null ? a.Code == Code : true)
                                                 .Where(a => Abbr != null ? a.Abbr == Abbr : true)
                                                 .AnyAsync();

        }

        public async Task<string> GetCodeByAbbrAsync(string Abbr)
        {
            await LoadCacheAsync();

            return _SiteLangCache.Where(a => a.Abbr.Equals(Abbr))
                                 .Select(a => a.Code).SingleOrDefault();
        }

        private async Task LoadCacheAsync()
        {
            if (_SiteLangCache == null)
            {
                _SiteLangCache = await _languagesRepository.Get
                                                         .Where(a => a.IsActive)
                                                         .Where(a => a.UseForSiteLanguage)
                                                         .Select(a => new OutSiteLanguageCache
                                                         {
                                                             Id = a.Id.ToString(),
                                                             Abbr = a.Abbr,
                                                             Code = a.Code,
                                                             IsRtl = a.IsRtl,
                                                             Name = a.Name,
                                                             NativeName = a.NativeName,
                                                             FlagUrl = ""
                                                         }).ToListAsync();
            }
        }

        public async Task<List<OutSiteLanguageCache>> GetAllLanguageSiteLangAsync()
        {
            await LoadCacheAsync();
            return _SiteLangCache;
        }
        public async Task<bool?> IsValidAbbrForSiteLangAsync(InpIsValidAbbrForSiteLang input)
        {
            try
            {
                #region Validations
                input.CheckModelState(_ServiceProvider);
                #endregion Validations

                return await _languagesRepository.Get.AnyAsync(a => a.Abbr.Equals(input.Abbr));
            }
            catch (ArgumentInvalidException e)
            {
                _Logger.Debug(e);
                return null;
            }
            catch (Exception e)
            {
                _Logger.Error(e);
                return null;
            }
        }
    }
}
