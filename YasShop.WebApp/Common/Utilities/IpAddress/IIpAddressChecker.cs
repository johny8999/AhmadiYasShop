namespace YasShop.WebApp.Common.Utilities.IpAddress
{
    public interface IIpAddressChecker
    {
        string CheckIp(string Ip);
        string GetLangAbbr(string Ip);
    }
}