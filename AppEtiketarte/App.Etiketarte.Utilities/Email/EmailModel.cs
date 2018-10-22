using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace App.Etiketarte.Utilities.Email
{
    public class EmailModel
    {
        public MailAddress From { get; set; }
        public List<MailAddress> To { get; set; }
        public List<MailAddress> CC { get; set; }
        public List<MailAddress> BCC { get; set; }
        public string Subject { get; set; }
        public Encoding SubjectEncoding { get; set; }
        public string Body { get; set; }
        public Encoding BodyEncoding { get; set; }
        public MailPriority Priority { get; set; }
        public List<HttpPostedFileBase> Attachments { get; set; }

        public EmailModel()
        {
            To = new List<MailAddress>();
            CC = new List<MailAddress>();
            BCC = new List<MailAddress>();
            SubjectEncoding = Encoding.UTF8;
            BodyEncoding = Encoding.UTF8;
            Attachments = new List<HttpPostedFileBase>();
            Priority = MailPriority.Normal;
        }
    }
}