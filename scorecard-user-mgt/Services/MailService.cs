using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using scorecard_user_mgt.Interfaces;
using scorecard_user_mgt.Models;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            using var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequest.Subject,
            };

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            var builder = new BodyBuilder();

            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;

            email.Body = builder.ToMessageBody();
            /*using (var smtp1 = new System.Net.Mail.SmtpClient())
            {
                smtp1.Host = "smtp.gmail.com";
                smtp1.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                smtp1.UseDefaultCredentials = true;
                smtp1.Credentials = NetworkCred;
                smtp1.Port = 587;
                *//*var email = new *//*
                smtp1.Send(email);
            }*/

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            GetSecurityProtocol();
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public string GetEmailTemplate(string templateName)
        {
            var baseDir = Directory.GetCurrentDirectory();
            string folderName = "/StaticFiles/";
            var path = Path.Combine(baseDir + folderName, templateName);
            return File.ReadAllText(path);
        }

        private static void GetSecurityProtocol()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }
    }
}
