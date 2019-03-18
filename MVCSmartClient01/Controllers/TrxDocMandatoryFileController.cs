using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using System.Configuration;

namespace MVCSmartClient01.Controllers
{
    public class TrxDocMandatoryFileController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxDocMandatoryFile";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxDocMandatoryFileController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxDocMandatoryFile", SmartAPIUrl);

            client = new HttpClient();
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
                var myData = JsonConvert.DeserializeObject<List<trxDocMandatoryFile>>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        public ActionResult Create()
        {
            return View(new trxDocMandatoryFile());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxDocMandatoryFile Emp)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int IdDocMandatoryFile)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdDocMandatoryFile);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDocMandatoryFile>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int IdDocMandatoryFile, trxDocMandatoryFile Emp)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + IdDocMandatoryFile, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxRekananDocument");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int IdDocMandatoryFile)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdDocMandatoryFile);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myData = JsonConvert.DeserializeObject<trxDocMandatoryFile>(responseData);

                return View(myData);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int IdDocMandatoryFile, trxDocMandatoryFile Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdDocMandatoryFile);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}
