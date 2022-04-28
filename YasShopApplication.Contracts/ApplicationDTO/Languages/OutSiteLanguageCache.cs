using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.ApplicationDTO.Languages
{
    public class OutSiteLanguageCache
    {
        public string Id { get; set; }
        public string FlagUrl { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }

    }
}
