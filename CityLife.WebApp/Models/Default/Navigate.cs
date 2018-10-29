using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Default
{
    /// <summary>
    /// Предназначен для отображения виджетов меню, 
    /// отображения допустимых ссылок и их стилизации по текущему Url.
    /// </summary>
    public class Navigate
    {
        public string CurrentUrl { get; set; }
        public string CurrentRole { get; set; }
        public bool IsAuth { get; set; }
        public string UserName { get; set; }
        public string PathAvatar { get; set; }
    }
}