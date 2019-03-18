using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSmartClient01.Models;

using System.Net.Http;
using ApiHelper;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class HomeRekananController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        string url = string.Empty;

        public HomeRekananController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstRekanan", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Rekanan";
            if (Convert.ToInt16(tokenContainer.IdTypeOfRekanan) >= 1 && Convert.ToInt16(tokenContainer.IdTypeOfRekanan) <= 7)
            {
                HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetPengumumanByTypeOfRekanan/{1}", url, tokenContainer.IdTypeOfRekanan.ToString())).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<fGetPengumumanByTypeOfRekanan_Result>(responseData);

                    return View(myData);
                }
            }
            else
            {
                return View();
            }

            return View("Error");
            //
            //ViewBag.Title = "Rekanan";
            //return View();
        }
    }
}
