using Framework.Application.Services.Localizer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Common.DataAnnotations.Strings
{
    public class MaxLengthStringAttribute : ValidationAttribute
    {
        private readonly int _maxLength;
        public MaxLengthStringAttribute(int maxLength)
        {
            _maxLength = maxLength;
            if (ErrorMessage is null)
                ErrorMessage = "MaxLengthStringMsg";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is not string)
                throw new Exception("MaxlenghString just work on String data types.");

            if (value.ToString().Length > _maxLength)
                return new ValidationResult(GetMessage(validationContext));

            return ValidationResult.Success;
        }
        private string GetMessage(ValidationContext validationContext)
        {
            var _ServiceProvider = validationContext.GetService<IServiceProvider>();
            var _Localizer = _ServiceProvider.GetService<ILocalizer>();
            if (_Localizer is null)
            {
                if (ErrorMessage.Contains("{0}"))
                {
                    ErrorMessage = ErrorMessage.Replace("{0}", validationContext.DisplayName);
                }
                if (ErrorMessage.Contains("{1}"))
                {
                    ErrorMessage = ErrorMessage.Replace("{1}",_maxLength.ToString());
                }
            }
            else
            {

                ErrorMessage = _Localizer[ErrorMessage];
                if (ErrorMessage.Contains("{0}"))
                    ErrorMessage = ErrorMessage.Replace("{0}", _Localizer[validationContext.DisplayName]);

                if (ErrorMessage.Contains("{1}"))
                    ErrorMessage = ErrorMessage.Replace("{1}",_maxLength.ToString());
            }
            return ErrorMessage;
        }
    }
}
