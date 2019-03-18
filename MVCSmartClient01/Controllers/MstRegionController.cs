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
    public class MstRegionController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/mstRegionAdmin";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public MstRegionController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstRegion", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<mstRegionAdmin>>(responseData);
                return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> _Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<List<mstRegionAdmin>>(responseData);
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public async Task<ActionResult> CreateEdit(Guid IdData)
        {
            if (IdData != null && IdData != Guid.Empty)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRegionAdmin>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                return View(new mstRegionAdmin());
            }
        }
        //The Post method
        [HttpPost]
        public async Task<ActionResult> CreateEdit(mstRegionAdmin myData)
        {
            if (myData.IdRegionAdmin != Guid.Empty)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdRegionAdmin.ToString(), myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = "admin";
                myData.IdRegionAdmin = Guid.NewGuid();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error");
            }
        }
        //The DELETE method
        public async Task<ActionResult> Delete(Guid IdData, mstRegionAdmin Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdData.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}
