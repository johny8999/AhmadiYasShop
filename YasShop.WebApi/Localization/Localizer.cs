using Framework.Application.Services.Localizer;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.WebApp.Localization.Resources;

namespace YasShop.WebApp.Localization
{
    //توضیح داده شود
    public class Localizer : ILocalizer
    {
        //public string this[string name] => Get(name);

        //fulled by property get
        public string this[string name]
        {
            get
            {
                return Get(name);
            }
        }

        public string this[string name, params object[] arguments]
        {
            get
            {
                return Get(name, arguments);
            }
        }

        private readonly IStringLocalizer _localizer;

        public Localizer(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }
        public Localizer(IStringLocalizerFactory factory)
        {
            _localizer = new FactoryLocalizer().Set(factory, typeof(SharedResources));
        }
        private string Get(string Name)
        {
            return _localizer[Name];
        }
        private string Get(string Name, params object[] arguments)
        {
            return _localizer[Name, arguments];
        }
    }
}
