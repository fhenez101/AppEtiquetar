using System;
using System.Net.Mail;

namespace App.Etiketarte.Utilities.Email
{
    public class EmailSender
    {
        public void Send(EmailModel emailModel)
        {
            using (var mail = new MailMessage())
            {
                emailModel.To.ForEach(x => mail.To.Add(x));
                emailModel.CC.ForEach(x => mail.CC.Add(x));
                emailModel.BCC.ForEach(x => mail.Bcc.Add(x));
                emailModel.Attachments.ForEach(x => mail.Attachments.Add(new Attachment(x.InputStream, x.FileName)));
                mail.Subject = emailModel.Subject;
                mail.SubjectEncoding = emailModel.SubjectEncoding;
                mail.Body = emailModel.Body;
                mail.BodyEncoding = emailModel.BodyEncoding;
                mail.Priority = emailModel.Priority;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(mail);
                }
            }
        }
    }
}