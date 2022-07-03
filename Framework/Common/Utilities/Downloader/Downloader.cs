using Framework.Application.Exceptions;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Downloader.Dto;
using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Downloader
{

    public class Downloader : IDownloader
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;

        public Downloader(ILogger logger, IServiceProvider serviceProvider)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<string> GetHtmlForPageAsync(InpGetHtmlForPage Input)
        {
            try
            {
                #region Validation
                {
                    Input.CheckModelState(_ServiceProvider);
                }
                #endregion Validation


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

        private string UrlEncodedParametrGenerator(object Data)
        {
            if (Data is null)
                return string.Empty;

            var _Parameter = GetModelParameter(Data);
            var UrlParameter = "?";

            foreach (var item in _Parameter)
                if (item.Value is not null)
                    UrlParameter += "&" + item.Key + "=" + item.Value.ToString();

            return UrlParameter;

        }

        private Dictionary<string, string> GetModelParameter(object Data)
        {
            if (Data is null)
                return new Dictionary<string, string>();

            Type t = Data.GetType();
            PropertyInfo[] Props = t.GetProperties();

            Dictionary<string, string> LstParameters = new();
            foreach (var Prop in Props)
            {
                object Value = Prop.GetValue(Data, new object[] { });
                if (Value is not null)
                {
                    if (Value.GetType() == typeof(string[]))
                        foreach (var item in (string[])Value)
                            LstParameters.Add(Prop.Name, item.ToString());

                    if (Value.GetType() == typeof(string))
                        LstParameters.Add(Prop.Name, Value.ToString());

                    if (Value.GetType() == typeof(int))
                        LstParameters.Add(Prop.Name, Value.ToString());

                    if (Value.GetType() == typeof(double))
                        LstParameters.Add(Prop.Name, Value.ToString());

                    if (Value.GetType() == typeof(float))
                        LstParameters.Add(Prop.Name, Value.ToString());

                    if (Value.GetType() == typeof(long))
                        LstParameters.Add(Prop.Name, Value.ToString());

                    if (Value.GetType() == typeof(bool))
                        LstParameters.Add(Prop.Name, Value.ToString());
                }
            }
            return LstParameters;
        }
    }
}
