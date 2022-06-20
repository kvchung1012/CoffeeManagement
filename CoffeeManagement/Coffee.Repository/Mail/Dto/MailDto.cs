using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Mail.Dto
{
    public class MailRequest
    {
        // người nhận
        public List<string> ToEmail { get; set; }
        
        // tên template mail
        public string TemplateMail { get; set; }

        // các key để replace
        public Dictionary<string, string> ShortCode { get; set; } = new Dictionary<string, string>();

        // Tiêu đề của email
        public string Subject { get; set; }
    }

    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
