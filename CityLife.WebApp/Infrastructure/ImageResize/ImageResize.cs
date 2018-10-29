using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace CityLife.WebApp.Infrastructure.ImageResize
{
    public static class ImageResize
    {
        public static string Resize(HttpPostedFileBase file, string directory, int width,int height,bool crop=false)
        {
            List<string> resultPaths = new List<string>();
            var fileGuid = Guid.NewGuid().ToString();
            string mode = crop ? "crop" : "max";
            var image = new ImageJob(file, "~" + directory + fileGuid + ".<ext>",new Instructions($"maxwidth={width}&maxheight={height}&mode={mode}"));
            image.CreateParentDirectory = true;
            image.Build();

            var path = fileGuid + ImageResizer.Util.PathUtils.GetExtension(image.FinalPath);

            return path;
        }
    }
}