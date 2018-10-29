using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin;

namespace CityLife.Security.Vk
{
    public class VkAuthenticationOptions : AuthenticationOptions
    {
        IList<string> fileds;
        public VkAuthenticationOptions():base(Constants.DefaultAuthenticationType)
        {
            Caption = Constants.DefaultAuthenticationType;
            ReturnEndpointPath = "/signin-vk";
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = new List<string>();
            BackchannelTimeout = TimeSpan.FromSeconds(60);
        }

        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string ReturnEndpointPath { get; set; }
        public string UserInformationEndPoint { get; set; }
        public ICertificateValidator BackchannelCertificateValidator { get; set; }
        public TimeSpan BackchannelTimeout { get; set; }
        public string Caption { get { return Description.Caption; } set { Description.Caption = value; } }
        /// <summary>
        /// Contain end point url, for get acces token
        /// </summary>
        public PathString CallbackPath { get; set; }
        public string SignInAsAuthenticationType { get; set; }
        public IVkAuthenticationProvider Provider { get; set; }
        public HttpMessageHandler BackchannelHttpHandler {get;set;}
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }
        public IList<string> Scope { get; set; }

        public bool SendAppSecretProof { get; set; }
        public IList<string> Fileds { get { return fileds; } }

        public ICookieManager CookieManager { get; set; }
    }
}
