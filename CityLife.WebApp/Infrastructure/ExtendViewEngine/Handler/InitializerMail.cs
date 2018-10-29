using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Infrastructure.ExtendViewEngine.Handler
{
    internal class InitializerMail
    {
        public static string GetHtmlTemplateString<TViewModel>(string pathToView, TViewModel vmodel)
            where TViewModel : class
        {

            if (string.IsNullOrEmpty(pathToView))
            {
                throw new NullReferenceException("pathToView");
            }

            if (vmodel == null)
            {
                throw new NullReferenceException("vmodel");
            }

            pathToView = HttpContext.Current.Server.MapPath(pathToView);

            string template = File.ReadAllText(pathToView);

            var config = new TemplateServiceConfiguration();

            config.Language = Language.CSharp;
            config.EncodedStringFactory = new RawStringFactory();
            config.EncodedStringFactory = new HtmlEncodedStringFactory();
            var service = RazorEngineService.Create(config);

            Engine.Razor = service;

            var output = Engine.Razor.RunCompile(new LoadedTemplateSource(template, pathToView), "email_template", null, vmodel);

            return output;
        }


        public static void SendMail<TViewModel>(string pathToView, TViewModel vmodel)
            where TViewModel:class
        {
            if (string.IsNullOrEmpty(pathToView))
            {
                throw new NullReferenceException("pathToView");
            }

            if (vmodel == null)
            {
                throw new NullReferenceException("vmodel");
            }



        }
    }
}