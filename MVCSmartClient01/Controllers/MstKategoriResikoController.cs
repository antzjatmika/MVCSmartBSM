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
    public class MstKategoriResikoController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/MstKategoriResiko";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public MstKategoriResikoController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstKategoriResiko", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var Employees = JsonConvert.DeserializeObject<List<mstKategoriResiko>>(responseData);
                return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByIdTypeOfRekanan(int IdTypeOfRekanan = 4)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetByIdTypeOfRekanan/{1}", url, IdTypeOfRekanan));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstKategoriResiko>>(responseData);
                ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
                return View("GetByIdTypeOfRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByIdTypeOfRekanan(int IdTypeOfRekanan = 4)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetByIdTypeOfRekanan/{1}", url, IdTypeOfRekanan));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstKategoriResiko>>(responseData);
                ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
                return PartialView("_GetByIdTypeOfRekanan", myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UpdateLineNilai(mstKategoriResiko myData)
        {
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + myData.IdMstKategoriResiko, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage2 = await client.GetAsync(string.Format("{0}/GetByIdTypeOfRekanan/{1}", url, myData.IdTypeOfRekanan));
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    var myDataColls = JsonConvert.DeserializeObject<List<mstKategoriResiko>>(responseData);
                    ViewBag.IdTypeOfRekanan = myData.IdTypeOfRekanan;
                    return PartialView("_GetByIdTypeOfRekanan", myDataColls);
                }
            }
            return RedirectToAction("Error");
        }
    }
}
