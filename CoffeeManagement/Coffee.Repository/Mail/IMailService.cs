using Coffee.Application.Mail.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
