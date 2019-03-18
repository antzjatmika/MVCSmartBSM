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
using System.Text;
using System.Web;
using System.Globalization;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxManagementController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxManagement";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxManagementController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxManagement", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _ManagementByRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return PartialView("_ManagementByRek", myData);
            }
            return View("Error");
        }

        public ActionResult Create()
        {
            return View(new mstContact());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxManagement Emp)
        {
            Emp.CreatedDate = DateTime.Today;
            Emp.CreatedUser = tokenContainer.UserId.ToString();
            Emp.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var Employee = JsonConvert.DeserializeObject<trxManagement>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxManagement Emp)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + id, Emp);
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
                var Employee = JsonConvert.DeserializeObject<trxManagement>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxManagement Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> GetManagementPCP()
        {
            return View();
        }
        public ActionResult _RekManagemenBL()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetRekManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwRekManagemenBL>>(responseData);
                return PartialView("_RekManagemenBL",myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _ManagemenBL()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                //return PartialView("_ManagemenBL", myData);
                return View("_ManagemenBL", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _ManagemenBLPart()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return PartialView("_ManagemenBL", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> BLByRekanan()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetRekManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwRekManagemenBL>>(responseData);
                return View("BLByRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _BLByRekanan()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetRekManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwRekManagemenBL>>(responseData);
                return PartialView("BLByRekanan", myData);
            }
            return View("Error");
        }

        public async Task<ActionResult> BLByPartner()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _BLByPartner()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetManagemenBL").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return PartialView(myData);
            }
            return View("Error");
        }
        public string Encode(string content)
        {
            byte[] encodedBytes = UTF8Encoding.UTF8.GetBytes(content);
            string externalIdEncoded = HttpServerUtility.UrlTokenEncode(encodedBytes);

            return externalIdEncoded;
        }
        public async Task<ActionResult> BlacklistPartnerById(int IdManagemen, string Catatan, string AkhirBlacklist)
        {
            DateTime dtmAkhirBL;
            DateTime.TryParseExact(AkhirBlacklist, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out dtmAkhirBL);
            Catatan = Encode(Catatan);
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/BlacklistPartnerById/{1}/{2}/{3}/{4}"
                , url, IdManagemen.ToString(), "1", Catatan, AkhirBlacklist)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                //var myData = JsonConvert.DeserializeObject<List<vwRekManagemenBL>>(responseData);
                var myData = new List<vwRekManagemenBL>();
                return PartialView("_RekManagemenBL", myData);
                //return RedirectToAction("Index");
            }
            return View("Error");
        }

        public async Task<ActionResult> ReleaseBLPartnerById(int IdManagemen, string Catatan, string AkhirBlacklist)
        {
            Catatan = Encode(Catatan);
            AkhirBlacklist = DateTime.Today.ToString("yyyy-MM-dd");
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/BlacklistPartnerById/{1}/{2}/{3}/{4}"
                , url, IdManagemen.ToString(), "0", Catatan, AkhirBlacklist)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                //var myData = JsonConvert.DeserializeObject<List<vwRekManagemenBL>>(responseData);
                var myData = new List<vwRekManagemenBL>();
                return PartialView("_RekManagemenBL", myData);
                //return RedirectToAction("Index");
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdateManagemenBL(trxManagement myData)
        {
            bool bolSaveOK = false;

            if (myData.IdManagemen > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/AddUpdateManagemenBL", myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    bolSaveOK = true;
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url + "/AddUpdateManagemenBL", myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    bolSaveOK = true;
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            if (bolSaveOK)
            {
                HttpResponseMessage responseMessage1 = await client.GetAsync(url + "/GetManagemenBL");
                if (responseMessage1.IsSuccessStatusCode)
                {
                    var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                    var myData1 = JsonConvert.DeserializeObject<List<trxManagement>>(responseData1);

                    ViewBag.SimpanVisi = "visible";
                    ViewBag.EditVisi = "hidden";
                    ViewBag.IsReadOnly = false;

                    return PartialView("BLByPartner", myData1);
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }
    }
}
