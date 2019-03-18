using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using System.Threading.Tasks;
using System.Configuration;

namespace MVCSmartClient01.Controllers
{
    public class MstReferenceController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/MstReference";
        string url = string.Empty; 
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public MstReferenceController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstReference", SmartAPIUrl);

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
               var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;

               var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

               return View(myData);
            }
            return View("Error");
        }
 
        public ActionResult Create()
        {
            return View(new mstReference());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(mstReference Emp)
        {
           HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(url,Emp);
           if (responseMessage.IsSuccessStatusCode)
           {
               return RedirectToAction("Index");
           }
           return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url+"/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<mstReference>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, mstReference Emp)
        {
 
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url+"/" +id, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<mstReference>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, mstReference Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> GetEmailConfig()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByType/EmailConfig", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetEmailConfig()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByType/EmailConfig", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                return PartialView("_GetEmailConfig", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetDashboard01Config()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByType/DshBoard01", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetDashboard01Config()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByType/DshBoard01", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                return PartialView("_GetDashboard01Config", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetReport01Config()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByType/RptAsuransi01", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetReport01Config()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByType/RptAsuransi01", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                return PartialView("_GetReport01Config", myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> EmailConfigUpdatePartial(mstReference myData)
        {
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + myData.IdRef, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage2 = await client.GetAsync(string.Format("{0}/GetByType/EmailConfig", url));
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    var myDataColls = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                    return PartialView("_GetEmailConfig", myDataColls);
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> DshBoard01UpdatePartial(mstReference myData)
        {
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + myData.IdRef, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage2 = await client.GetAsync(string.Format("{0}/GetByType/DshBoard01", url));
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    var myDataColls = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                    return PartialView("_GetDashboard01Config", myDataColls);
                }
            }
            return RedirectToAction("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> RptAsuransi01UpdatePartial(mstReference myData)
        {
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + myData.IdRef, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage2 = await client.GetAsync(string.Format("{0}/GetByType/RptAsuransi01", url));
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    var myDataColls = JsonConvert.DeserializeObject<List<mstReference>>(responseData);

                    return PartialView("_GetReport01Config", myDataColls);
                }
            }
            return RedirectToAction("Error");
        }
    }
}
