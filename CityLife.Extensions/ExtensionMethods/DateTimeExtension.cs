using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Extensions.ExtensionMethods
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Get DateTime Time Format: dd.MM.yyyy
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDate(this DateTime date)
        {            
            return date.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Get Format time: H:mm
        /// </summary>
        /// <returns></returns>
        public static string GetFormatTime()
        {
            return "H:mm";
        }

        /// <summary>
        /// Get DateTime format: dd.MM.yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatDateTime(this DateTime date)
        {
            
            return date.ToString("dd.MM.yyyy H:mm:ss");
        }

        /// <summary>
        /// Get DateTime Nullable format: dd.MM.yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatDateTime(this DateTime? date)
        {
            var newDate = date.HasValue ? date.Value.ToString("dd.MM.yyyy H:mm:ss") : string.Empty;
            return newDate;
        }

        /// <summary>
        /// Calculate Age By Birthday
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static int GetAgeByBirthday(this DateTime birthday)
        {
            var today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age;
        }
    }
}