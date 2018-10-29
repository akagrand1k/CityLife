using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Extensions.Constant
{
    public class SysConstants
    {

        #region EmailGate

        public const string EmailGate_SmtpServer = "EmailGate_SmtpServer";
        public const string EmailGate_SmtpPort = "EmailGate_SmtpPort";
        public const string EmailGate_Login = "EmailGate_Login";
        public const string EmailGate_Password = "EmailGate_Password";
        public const string EmailGate_Ssl = "EmailGate_Ssl";
        public const string EmailGate_From = "EmailGate_From";

        #endregion

        #region login providers

        public const string LoginProvider_Google = "google";
        public const string LoginProvider_Vk = "vkontakte";
        public const string LoginProvider_Facebook = "facebook";
        

        #endregion

        #region external identity credentials

        public const string External_GoogleClinetId = "963645827072-pamokn0plnp2jekdn1f730pe5g5522ja.apps.googleusercontent.com";
        public const string External_GoogleSecret = "DjDFVYHbgO50Ay-vAeuA69Y8";
        public const string External_FacebookClientId = "";
        public const string External_FacebookSecret = "";
        public const string External_VkClientId = "6104818";
        public const string External_VkSecret = "X09nQfSKxqA7C9toiM1r";

        #endregion

        #region identity access token keys

        public const string AccessToken_Google = "g_t";
        public const string AccessToken_GoogleRefresh = "g_r_t";
        public const string AccessToken_Facebook = "fb_t";
        public const string AccessToken_FacebookRefresh = "fb_r_t";        
        public const string AccessToken_Vk = "v_t";
        public const string AccessToken_VkRefresh = "v_r_t";

        #endregion

        #region identity cookies keys

        public const string Identity_CookieName = "_kcl_c";
        public const string Identity_SessionName = "_kcl_s";

        #endregion

        #region exceptions keys

        public const string AppException_CityNews = "citynews_log";
        public const string AppException_Classifier = "classifier_log";
        public const string AppException_Organization = "org_log";
        public const string AppException_References = "references_log";
        public const string AppException_Section = "section_log";
        public const string AppException_UserProfile = "userprofile_log";

        #endregion

    }
}
