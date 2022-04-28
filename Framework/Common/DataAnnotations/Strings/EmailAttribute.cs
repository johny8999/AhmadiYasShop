using Framework.Application.Services.Localizer;
using Framework.Common.ExMethods;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.DataAnnotations.Strings
{
    public class EmailAttribute: ValidationAttribute
    {
        private ILocalizer _Localizer;
        public EmailAttribute()
        {
            if (ErrorMessage is null)
            {
                ErrorMessage = "EmailMsg";
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value is null)
                return ValidationResult.Success;

            if (value is not string)
                throw new ArgumentException("Email only work on string datatype");

            var _serviceProvider = validationContext.GetService<IServiceProvider>();
            _Localizer = _serviceProvider.GetService<ILocalizer>();

            string Email = value.ToString();
            if (Email.IsMatch(@"^([a-zA-Z0-9_\-\.]+)[@]([a-zA-Z0-9_\-\.]+)[\.]([a-zA-Z]{2,5})$"))
                return ValidationResult.Success;

            else
                return new ValidationResult(GetMessage(validationContext));
        }
        private string GetMessage(ValidationContext validationContext)
        {

            if (_Localizer is null)
                return ErrorMessage.Replace("{0}", validationContext.DisplayName);
            else
                return _Localizer[ErrorMessage, validationContext.DisplayName];
        }
    }
 
}
