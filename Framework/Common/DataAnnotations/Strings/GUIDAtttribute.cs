using Framework.Application.Services.Localizer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.Strings
{
    public class GUIDAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return ValidationResult.Success;

                if (value is not string)
                    return ValidationResult.Success;

                if (value.ToString().Contains(','))
                {
                    foreach (var item in value.ToString().Split(','))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var _Guid = Guid.Parse(item);
                        }
                    }
                    return ValidationResult.Success;
                }
                else
                {
                    #region Way1
                    // Guid guid = default;
                    //var IsGuid = Guid.TryParse((string)value,out guid);
                    #endregion Way1

                    var _Guid = Guid.Parse((string)value);
                    return ValidationResult.Success;
                }
            }
            catch (Exception)
            {
                return new ValidationResult(GetMessage(validationContext));
            }
        }

        private string GetMessage(ValidationContext validationContext)
        {
            if (ErrorMessage is null)
                ErrorMessage = "GUIDMsg";
            
            var _ServiceProvider=validationContext.GetService<IServiceProvider>();
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();

            ErrorMessage = _Localizer[ErrorMessage];
            if (ErrorMessage.Contains("{0}"))
                ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

            return ErrorMessage;
        }
    }
}
