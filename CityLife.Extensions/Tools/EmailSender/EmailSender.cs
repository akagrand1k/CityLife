using CityLife.Extensions.Tools.EmailSender.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Extensions.Tools.EmailSender
{
    public class EmailSender
    {

        private SmtpClient client;

        public void Send(EmailSenderOptions options)
        {
            if (options == null)
            {
                throw new NullReferenceException("options");
            }

            client = new SmtpClient(options.Host, options.Port);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.UseDefaultCredentials = options.UseDefaultCredentials;

            client.Credentials = new NetworkCredential(options.From, options.Password);

            client.EnableSsl = options.EnableSSL;

            MailMessage mail = new MailMessage(options.From, options.To);

            mail.Subject = options.Subject;
            mail.Body = options.Body;
            mail.IsBodyHtml = options.IsBodyHtml;

            if (options.Attachment)
            {
                var attach = new Attachment(options.AttachmentPath);

                attach.ContentDisposition.Inline = true;
                attach.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                attach.ContentId = options.CotnentId;
                attach.ContentType.MediaType = options.MediaType;
                attach.ContentType.Name = Path.GetFileName(options.AttachmentPath);

                mail.Attachments.Add(attach);
            }

            client.Send(mail);

        }

    }
}
