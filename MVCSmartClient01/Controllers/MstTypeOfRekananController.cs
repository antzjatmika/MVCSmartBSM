using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using System.Web;
//using Microsoft.IdentityModel.Claims;
//using System.Security.Claims.ClaimsPrincipal;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
//using MVCSmartClient01.Components;


namespace MVCSmartClient01.Controllers
{
    using ApiHelper.Client;
    using ApiInfrastructure;
    using ApiInfrastructure.Client;
    using Attributes;
    using System.Configuration;
    //[Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    [Authentication]
    //[Authorize]
    public class MstTypeOfRekananController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/MstTypeOfRekanan";
        string url = string.Empty; 
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter

        public MstTypeOfRekananController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstTypeOfRekanan", SmartAPIUrl);

            var contextWrapper = new TokenContainer();
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (contextWrapper.ApiToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", contextWrapper.ApiToken.ToString());
            }
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            var contextWrapper = new TokenContainer();
            if (contextWrapper.ApiToken != null)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var Employees = JsonConvert.DeserializeObject<List<mstTypeOfRekanan>>(responseData);

                    return View(Employees);
                }
                return View("Error");
            }
            return null;
        }
 
        public ActionResult Create()
        {
            return View(new mstTypeOfRekanan());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(mstTypeOfRekanan Emp)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, Emp);
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

                var Employee = JsonConvert.DeserializeObject<mstTypeOfRekanan>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, mstTypeOfRekanan Emp)
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

                var Employee = JsonConvert.DeserializeObject<mstTypeOfRekanan>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, mstTypeOfRekanan Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}
