﻿using System;
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
    public class EmlNotificationLogController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/EmlNotificationLog";
        string url = string.Empty;
         
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public EmlNotificationLogController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/EmlNotificationLog", SmartAPIUrl);
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

               var Employees = JsonConvert.DeserializeObject<List<emlNotificationLog>>(responseData); 
 
                 return View(Employees);
            }
            return View("Error");
        }
 
        public ActionResult Create()
        {
            return View(new emlNotificationLog());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(emlNotificationLog Emp)
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

                var Employee = JsonConvert.DeserializeObject<emlNotificationLog>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, emlNotificationLog Emp)
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

                var Employee = JsonConvert.DeserializeObject<emlNotificationLog>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, emlNotificationLog Emp)
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