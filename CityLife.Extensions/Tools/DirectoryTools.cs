using CityLife.Extensions.Constant;
using System;
using System.IO;
using System.Web;

namespace CityLife.Extensions.Tools
{
    public static class DirectoryTools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void CheckDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void DeleteFile(HttpContextBase context, string filename,string relPath = null)
        {
            try
            {
                string path = context.Server.MapPath(relPath + filename);
                File.Delete(path);
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
