using Framework.Application.Services.Email;
using Framework.Infrastructure;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace YasShop.Infrastructure.Services.Email
{
    public class GmailSender : IEmailSender
    {
        private readonly string _SenderTitle;
        private readonly string _UserName;
        private readonly string _Password;
        private readonly int _Port;
        private readonly bool _UseSsl;

        private readonly ILogger _Logger;
        public GmailSender(ILogger logger)
        {
            _SenderTitle = "YasShop";//SenderName
            _UserName = "sinaalipour77@gmail.com";
            _Password = "123sinaSINA";
            _Port = 587;
            _UseSsl = true;

            _Logger = logger;
        }

        public bool Send(string _To, string _Subject, string _Message)
        {
            try
            {
                MailMessage _MailMessage = new();
                _MailMessage.From = new MailAddress(_UserName, _SenderTitle, Encoding.UTF8);
                _MailMessage.To.Add(new MailAddress(_To));
                _MailMessage.Subject = _Subject;
                _MailMessage.Body = _Message;
                _MailMessage.IsBodyHtml = true;
                _MailMessage.BodyEncoding = Encoding.UTF8;
                _MailMessage.Priority = MailPriority.Normal;

                //send proccess
                SmtpClient smtp = new("smtp.gmail.com", _Port);
                smtp.EnableSsl = _UseSsl;
                smtp.Credentials = new NetworkCredential(_UserName, _Password);

                smtp.Send(_MailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return false;
            }
        }

        public async Task SendAsync(string _To, string _Subject, string _Message)
        {
            try
            {
                MailMessage _MailMessage = new();
                _MailMessage.From = new MailAddress(_UserName, _SenderTitle, Encoding.UTF8);
                _MailMessage.To.Add(new MailAddress(_To));
                _MailMessage.Subject = _Subject;
                _MailMessage.Body = _Message;
                _MailMessage.IsBodyHtml = true;
                _MailMessage.BodyEncoding = Encoding.UTF8;
                _MailMessage.Priority = MailPriority.Normal;

                // send proccess
                SmtpClient smtp = new("smtp.gmail.com", _Port);
                smtp.EnableSsl = _UseSsl;
                smtp.Credentials = new NetworkCredential(_UserName, _Password);

                smtp.SendCompleted += new SendCompletedEventHandler(SendCompeletedCallBack);

                smtp.SendAsync(_MailMessage, _To);

            }
            catch (Exception ex)
            {
                _Logger.Error(ex);

            }
        }

        private void SendCompeletedCallBack(object Sender, AsyncCompletedEventArgs args)
        {
            try
            {
                string tokan = (string)args.UserState ?? "";
                if (args.Cancelled)
                {

                }
                else if (args.Error is not null)
                {
                    throw new Exception($"Tokan: [{tokan}], Errors[{args.Error.Message}]");
                }
                else
                {
                    //success

                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
    }
}
