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
    public class MstSubRegionController : Controller
    {
        HttpClient client;
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public MstSubRegionController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstSubRegion", SmartAPIUrl);

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
                var myDataList = JsonConvert.DeserializeObject<mstSubRegionMulti>(responseData);
                return View(myDataList);
            }
            return View("Error");
        }
        public async Task<ActionResult> _Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var Employees = JsonConvert.DeserializeObject<mstSubRegionMulti>(responseData);
                return PartialView("Index", Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> CreateEdit(int IdData = -1)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<mstSubRegionForm>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        //The Post method
        [HttpPost]
        public async Task<ActionResult> CreateEdit(mstSubRegion myData)
        {
            if (myData.IdSubRegion > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdSubRegion.ToString(), myData);
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error");
            }
        }
        //The DELETE method
        public async Task<ActionResult> Delete(int IdData, mstSubRegion Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}
