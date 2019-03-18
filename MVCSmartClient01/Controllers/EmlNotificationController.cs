using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using Microsoft.Owin.Security;

using System.Net.Http.Formatting;
using System.Web;
using System.Configuration;
//using System.Web.Mvc;

namespace MVCSmartClient01.Controllers
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }  

    public class EmlNotificationController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/EmlNotification";
        string url = string.Empty;
         
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public EmlNotificationController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/EmlNotification", SmartAPIUrl);
            client = new HttpClient();  
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //System.Web.HttpContext.Current.Session["Token"] = "";
        }

        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            Token token = new Token();
            var form = new Dictionary<string, string>    
               {    
                   {"grant_type", "password"},    
                   {"username", "Jignesh"},    
                   {"password", "user123456"},    
               };
            var tokenResponse = client.PostAsync(string.Format("{0}/oauth/token", SmartAPIUrl), new FormUrlEncodedContent(form)).Result;
            //var token = tokenResponse.Content.ReadAsStringAsync().Result;  
            token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token.AccessToken);

            System.Web.HttpContext.Current.Session["TokenGue"] = token.AccessToken;

            //AuthenticationProperties options = new AuthenticationProperties();
            //options.IsPersistent = true;
            //options.ExpiresUtc = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
            //var claims_Original = new[]
            //    {
            //        new System.Security.Claims.Claim(Microsoft.IdentityModel.Claims.ClaimTypes.Name, "Jignesh"),
            //        new System.Security.Claims.Claim("AcessToken", string.Format("Bearer {0}", token.AccessToken)),
            //    };
            //var claims = new[]{ 
            //    new System.Security.Claims.Claim("UserLogin", "Jignesh"),
            //    new System.Security.Claims.Claim("AcessToken", string.Format("Bearer {0}", token.AccessToken)),
            //};
            //var identity = new System.Security.Claims.ClaimsIdentity(claims, "ApplicationCookie");
            //Request.GetOwinContext().Authentication.SignIn(options, identity);


            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
               var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;

               var Employees = JsonConvert.DeserializeObject<List<emlNotification>>(responseData); 
 
                 return View(Employees);
            }
            return View("Error");
        }
        public ActionResult Create()
        {
            return View(new emlNotification());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(emlNotification Emp)
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

                var Employee = JsonConvert.DeserializeObject<emlNotification>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, emlNotification Emp)
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

                var Employee = JsonConvert.DeserializeObject<emlNotification>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, emlNotification Emp)
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
