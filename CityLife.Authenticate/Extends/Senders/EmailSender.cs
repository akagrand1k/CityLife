using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.Extensions.Constant;
using CityLife.Extensions.Tools.EmailSender;
using CityLife.Extensions.Tools.EmailSender.Options;
using CityLife.BusinessLogic.SysReferencesService;
using CityLife.DALImplementation;
using CityLife.Entities.Models.References;
using CityLife.DALImplementation.UOW;
using CityLife.DALImplementation.Context;

namespace CityLife.Authenticate.Extends.Senders
{
    public class EmailSenderIdentity : IIdentityMessageService
    {
        private ISysReferencesService sysReferences;

        private EmailSender sender;
        private string contentId;
        private string attachFileName;
        public EmailSenderIdentity()
        {

        }

        public EmailSenderIdentity(string contentId)
        {
            this.contentId = contentId;
        }

        public EmailSenderIdentity(string contentId, string attachFileName)
        {
            this.contentId = contentId;
            this.attachFileName = attachFileName;
        }

        public Task SendAsync(IdentityMessage message)
        {
            sysReferences = new SysReferencesService(new UnitOfWork(new AppContext()));

            sender = new EmailSender();

            var sendOptions = new EmailSenderOptions();
            int port = 0;
            if (int.TryParse(sysReferences.GetValueByKey(SysConstants.EmailGate_SmtpPort), out port)) sendOptions.Port = port;
            else throw new InvalidCastException("Error parse email port!");
            sendOptions.Body = message.Body;
            sendOptions.Subject = message.Subject;
            sendOptions.From = sysReferences.GetValueByKey(SysConstants.EmailGate_Login);
            sendOptions.To = message.Destination;     
            sendOptions.Host = sysReferences.GetValueByKey(SysConstants.EmailGate_SmtpServer);
            sendOptions.EnableSSL = true;
            sendOptions.IsBodyHtml = true;
            sendOptions.Attachment = false;
            sendOptions.UseDefaultCredentials = true;
           
            sendOptions.Password = sysReferences.GetValueByKey(SysConstants.EmailGate_Password);
                                   

            sender.Send(sendOptions);

            return Task.FromResult(0);
        }

        private Task SendMailAsync(IdentityMessageExtend message)
        {
            return Task.FromResult(0);
        }
    }

    public class IdentityMessageExtend
    {
        public string ContentId { get; set; }
    }


}
