using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Extensions.Constant
{
    public class AppConstants
    {
        #region References poster consts

        public const string sessionPosterDayAlias = "day";
        public const string sessionPosterDayVal = "Дневной";
        public const string sessionPosterNightAlias = "night";
        public const string sessionPosterNightDayVal = "Вечерний";

        #endregion


        #region uploads directory path

        public const string directoryOrgLogo = "/uploads/avatars/org/";
        public const string directoryProfileAvatar = "/uploads/avatars/profile/";
        public const string dirNewsPreview = "/uploads/news/pr/";
        public const string dirNewsPics = "/uploads/news/c";

        #endregion

        #region news tags

        public const string _NewsTags = "news_tags";

        #endregion


        #region action log constants

        public const string _ActionView = "act_view";
        public const string _ActionEdit = "act_edit";
        public const string _ActionLogIn = "act_sign_in";
        public const string _ActionLogOut = "act_logout";

        #endregion
    }
}