using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
namespace CityLife.WebApp.Infrastructure.ExternalLogin
{
    internal class ApiTransactions
    {

        public static dynamic GetExtraAuthData(string uri)
        {
            dynamic result = null;

            using (var client = new HttpClient())
            {               
                var response = client.GetAsync(uri).Result;
                var t = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject(t);
            }

            return result;
        }

    }
}