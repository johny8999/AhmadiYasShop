namespace YasShop.Application.Contracts.ApplicationDTO.Languages
{
    public class InpAddLanguage
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }
        public bool IsActive { get; set; }
        public bool UseForSideLanguage { get; set; }

    }
}
