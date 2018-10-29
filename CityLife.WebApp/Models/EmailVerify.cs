using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models
{
    public class EmailVerify
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string CallbackUrl { get; set; }
        /// <summary>
        /// For media files 
        /// </summary>
        public string cid { get; set; }
    }
}