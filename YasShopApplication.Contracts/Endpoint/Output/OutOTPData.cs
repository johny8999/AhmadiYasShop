namespace YasShop.Application.Contracts.Endpoint.Output
{
    public class OutOTPData
    {
        public string otpCode { get; set; }
        public string dateCreated { get; set; }
        public string dateExpired { get; set; }
    }
}
