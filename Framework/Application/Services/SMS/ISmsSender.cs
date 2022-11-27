namespace Framework.Application.Services.SMS
{
    public interface ISmsSender
    {
        public bool Send(string PhoneNumber, string Message);
        bool SendLoginCode(string PhoneNumber, string Code);
    }
}
