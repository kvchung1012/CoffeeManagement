using Coffee.Application.Mail.Dto;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Coffee.Application
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IHostingEnvironment _env;
        public MailService(IOptions<MailSettings> mailSettings, IHostingEnvironment env)
        {
            _mailSettings = mailSettings.Value;
            _env = env;

        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            // config người gửi
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            // config người nhận
            foreach (var item in mailRequest.ToEmail)
            {
                email.To.Add(MailboxAddress.Parse(item));
            }
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = GetMailTemplate(mailRequest);
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        private string GetMailTemplate(MailRequest mailRequest)
        {
            // lấy đường dãn file
            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + mailRequest.TemplateMail+".html";
            // đọc mail html
            var template = "";
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                template = SourceReader.ReadToEnd();
            }

            // replace short code
            foreach (var item in mailRequest.ShortCode)
            {
                template = template.Replace(item.Key, item.Value);
            }
            return template;
        }
    }
}
