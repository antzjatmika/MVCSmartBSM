using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using ApiHelper;
using System.Configuration;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class DashboardController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/Dashboard";
        string url = string.Empty; 

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public DashboardController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/Dashboard", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        //public ActionResult Index(int IdTypeOfRekanan)
        //{
        //    ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;

        //    switch (IdTypeOfRekanan)
        //    {
        //        case 1:
        //            ViewBag.PieName = "pieKJP";
        //            ViewBag.PiePekerjaan = "piePekerjaanKJP";
        //            ViewBag.MapName = "mapKJP";
        //            break;
        //        case 2:
        //            ViewBag.PieName = "pieKAP";
        //            ViewBag.PiePekerjaan = "piePekerjaanKAP";
        //            ViewBag.MapName = "mapKAP";
        //            break;
        //        case 3:
        //            ViewBag.PieName = "pieKMG";
        //            ViewBag.PiePekerjaan = "piePekerjaanKMG";
        //            ViewBag.MapName = "mapKMG";
        //            break;
        //        case 4:
        //            ViewBag.PieName = "pieASJ";
        //            ViewBag.PiePekerjaan = "piePekerjaanASJ";
        //            ViewBag.MapName = "mapASJ";
        //            break;
        //        case 5:
        //            ViewBag.PieName = "pieASK";
        //            ViewBag.PiePekerjaan = "piePekerjaanASK";
        //            ViewBag.MapName = "mapASK";
        //            break;
        //        case 6:
        //            ViewBag.PieName = "pieBLG";
        //            ViewBag.PiePekerjaan = "piePekerjaanBLG";
        //            ViewBag.MapName = "mapBLG";
        //            break;
        //        case 7:
        //            ViewBag.PieName = "pieNOT";
        //            ViewBag.PiePekerjaan = "piePekerjaanNOT";
        //            ViewBag.MapName = "mapNOT";
        //            break;
        //        default:
        //            break;
        //    }
        //    return View();
        //}
        public ActionResult Index(int IdTypeOfRekanan, int IdTypeOfSummary, int MyYear)
        {
            ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
            ViewBag.IdTypeOfSummary = IdTypeOfSummary;
            ViewBag.MyYear = MyYear;

            switch (IdTypeOfRekanan)
            {
                case 1:
                    ViewBag.PieName = "pieKJP";
                    ViewBag.PiePekerjaan = "piePekerjaanKJP";
                    ViewBag.MapName = "mapKJP";
                    break;
                case 2:
                    ViewBag.PieName = "pieKAP";
                    ViewBag.PiePekerjaan = "piePekerjaanKAP";
                    ViewBag.MapName = "mapKAP";
                    break;
                case 3:
                    ViewBag.PieName = "pieKMG";
                    ViewBag.PiePekerjaan = "piePekerjaanKMG";
                    ViewBag.MapName = "mapKMG";
                    break;
                case 4:
                    ViewBag.PieName = "pieASJ";
                    ViewBag.PiePekerjaan = "piePekerjaanASJ";
                    ViewBag.MapName = "mapASJ";
                    break;
                case 5:
                    ViewBag.PieName = "pieASK";
                    ViewBag.PiePekerjaan = "piePekerjaanASK";
                    ViewBag.MapName = "mapASK";
                    break;
                case 6:
                    ViewBag.PieName = "pieBLG";
                    ViewBag.PiePekerjaan = "piePekerjaanBLG";
                    ViewBag.MapName = "mapBLG";
                    break;
                case 7:
                    ViewBag.PieName = "pieNOT";
                    ViewBag.PiePekerjaan = "piePekerjaanNOT";
                    ViewBag.MapName = "mapNOT";
                    break;
                default:
                    break;
            }
            return View();
        }
        public JsonResult Data_FeeByRekananKJP()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(1);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananKAP()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(2);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananKMG()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(3);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananASJ()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(4);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananASK()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(5);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananBLG()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(6);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananNOT()
        {
            JsonResult myDataJason = new JsonResult();
            myDataJason = Data_FeeByRekanan(7);
            return myDataJason;
        }
        public JsonResult Data_FeeByRekanan(int IdTypeOfRekanan)
        {
            JsonResult myDataJason = new JsonResult();
            string UrlAddress = string.Format("{0}/FeeByRekanan/{1}/{2}/{3}", url, "2017", "3", IdTypeOfRekanan.ToString());
            HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<dashFeeByRekanan>>(responseData);
                myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
            }
            return myDataJason;
        }
        public JsonResult Data_FeeByRekananYear(int IdTypeOfRekanan, int myYear)
        {
            JsonResult myDataJason = new JsonResult();
            string UrlAddress = string.Format("{0}/FeeByRekanan/{1}/{2}/{3}", url, myYear, "3", IdTypeOfRekanan.ToString());
            HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<dashFeeByRekanan>>(responseData);
                myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
            }
            return myDataJason;
        }
        public JsonResult Data_FeeByRekanan11(int IdTypeOfRekanan)
        {
            int IdTypeOfSummary = 1;
            string UrlAddress = string.Empty;
            JsonResult myDataJason = new JsonResult();
            if (IdTypeOfSummary == 1)
            {
                UrlAddress = string.Format("{0}/PekerjaanByRekanan/{1}/{2}/{3}", url, "2017", "3", IdTypeOfRekanan.ToString());
                HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<List<dashPekerjaanByRekanan>>(responseData);
                    myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                UrlAddress = string.Format("{0}/FeeByRekanan/{1}/{2}/{3}", url, "2017", "3", IdTypeOfRekanan.ToString());
                HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<List<dashFeeByRekanan>>(responseData);
                    myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
                }
            }
            return myDataJason;
        }
        public JsonResult Data_PekerjaanByRekanan(int IdTypeOfRekanan)
        {
            JsonResult myDataJason = new JsonResult();
            string UrlAddress = string.Format("{0}/PekerjaanByRekanan/{1}/{2}/{3}", url, "2017", "3", IdTypeOfRekanan.ToString());
            HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<dashPekerjaanByRekanan>>(responseData);
                myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
            }
            return myDataJason;
        }
        public JsonResult Data_PekerjaanByRekananYear(int IdTypeOfRekanan, int myYear)
        {
            JsonResult myDataJason = new JsonResult();
            string UrlAddress = string.Format("{0}/PekerjaanByRekanan/{1}/{2}/{3}", url, myYear, "3", IdTypeOfRekanan.ToString());
            HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<dashPekerjaanByRekanan>>(responseData);
                myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
            }
            return myDataJason;
        }
        public JsonResult Data_LatLongByRekanan(int IdTypeOfRekanan)
        {
            JsonResult myDataJason = new JsonResult();
            string UrlAddress = string.Format("{0}/LatLongByRekanan/{1}", url, IdTypeOfRekanan.ToString());
            HttpResponseMessage responseMessage = client.GetAsync(UrlAddress).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstRekananMap>>(responseData);
                myDataJason = Json(myData, JsonRequestBehavior.AllowGet);
            }
            return myDataJason;
        }

        public ActionResult _FeeByRekananKJP(int IdTypeOfRekanan)
        {
            ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
            ViewBag.PieName = "chartdivKAP";
            return PartialView("_FeeByRekananKJP");
        }
        public ActionResult _FeeByRekananKAP()
        {
            ViewBag.IdTypeOfRekanan = 2;
            return PartialView("_FeeByRekananKAP");
        }
        public ActionResult _FeeByRekananKMG()
        {
            ViewBag.IdTypeOfRekanan = 3;
            return PartialView("_FeeByRekanan");
        }
        public ActionResult _FeeByRekananASJ()
        {
            ViewBag.IdTypeOfRekanan = 4;
            return PartialView("_FeeByRekanan");
        }
        public ActionResult _FeeByRekananASK()
        {
            ViewBag.IdTypeOfRekanan = 5;
            return PartialView("_FeeByRekanan");
        }
        public ActionResult _FeeByRekananBLG()
        {
            ViewBag.IdTypeOfRekanan = 6;
            return PartialView("_FeeByRekanan");
        }
        public ActionResult _FeeByRekananNOT()
        {
            ViewBag.IdTypeOfRekanan = 7;
            return PartialView("_FeeByRekanan");
        }

    }
}
