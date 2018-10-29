using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Infrastructure.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UploadAttribute : ValidationAttribute
    {
        private const string AllowFormat = "Разрешенные форматы:";
        private const string AllowSize = "Максимальный размер файла:";


        private IEnumerable<string> _ValidTypes { get; set; }
        private int _MaxSize { get; set; }

        public UploadAttribute(string validTypes,int maxSizeMb)
        {
            _ValidTypes = validTypes.Split(',').Select(s => s.Trim().ToLower());
            _MaxSize = maxSizeMb;
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {

                if (file != null && !_ValidTypes.Any(e => file.FileName.EndsWith(e)))
                {
                    return new ValidationResult(AllowFormat + string.Join(" или ", _ValidTypes));
                }
                if (file.ContentLength > 1024 * 1024 * _MaxSize)
                {
                    return new ValidationResult(AllowSize + _MaxSize + " мб.");

                }
            }
            return ValidationResult.Success;
        }
    }
}