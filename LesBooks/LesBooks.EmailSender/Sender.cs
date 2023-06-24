using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;

namespace LesBooks.EmailSender
{
    public  class Sender : ISender
    {
        SmtpSettings _smtpSettings = new SmtpSettings();

        public Sender()
        {

        }
        public void SendEmail(string toAddress, string subject, string clientName, string orderNumber)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toAddress));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = EmailTemplate.GetHtmlTemplate(clientName,orderNumber);

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
                client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }

   
}
