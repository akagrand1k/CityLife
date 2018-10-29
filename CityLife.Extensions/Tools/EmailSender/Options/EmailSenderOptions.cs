using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Extensions.Tools.EmailSender.Options
{
    public class EmailSenderOptions
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string To { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSSL { get; set; }
        public bool IsBodyHtml { get; set;}

        public bool Attachment { get; set; }
        /// <summary>
        /// This field for attachment files, such as logo, or other files.
        /// </summary>
        public string AttachmentPath { get; set; }
        public string CotnentId { get; set; }
        public string MediaType { get; set; }





    }
}
