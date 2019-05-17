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
    public class TrxPertanyaanNilaiController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxPertanyaanNilai";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxPertanyaanNilaiController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxPertanyaanNilai", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<trxPertanyaanNilai>>(responseData);
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
                var myData = JsonConvert.DeserializeObject<List<trxPertanyaanNilai>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AsistenByRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxPertanyaanNilai>>(responseData);
                return PartialView("_AsistenByRek", myData);
            }
            return View("Error");
        }

        public ActionResult Create()
        {
            return View(new trxPertanyaanNilai());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxPertanyaanNilai Emp)
        {
            Emp.CreatedDate = DateTime.Today;
            Emp.CreatedUser = tokenContainer.UserId.ToString();
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
                var Employee = JsonConvert.DeserializeObject<trxPertanyaanNilai>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxPertanyaanNilai Emp)
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
                var Employee = JsonConvert.DeserializeObject<trxPertanyaanNilai>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxPertanyaanNilai Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> GetNilaiByPenilai(int IdTypeOfGroup, int IdPenilai)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiByPenilai/{1}/{2}/{3}", url, IdTypeOfGroup, IdPenilai, tokenContainer.IdRekananContact.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwPertanyaanNilai>>(responseData);
                PertanyaanNilaiMulti myDataMulti = new PertanyaanNilaiMulti();
                myDataMulti.PertanyaanColls = myData;
                myDataMulti.PilihanScore = new List<SimpleRef> 
                { 
                    new SimpleRef { RefId = 1, RefDescription = "1" }
                    , new SimpleRef { RefId = 2, RefDescription = "2" }
                    , new SimpleRef { RefId = 3, RefDescription = "3" }
                    , new SimpleRef { RefId = 4, RefDescription = "4" }
                    , new SimpleRef { RefId = 5, RefDescription = "5" } };
                ViewBag.IdTypeOfGroup = IdTypeOfGroup;
                ViewBag.IdPenilai = IdPenilai;
                return View("GetNilaiByPenilai", myDataMulti);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetNilaiByPenilai(int IdTypeOfGroup, int IdPenilai)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiByPenilai/{1}/{2}/{3}", url, IdTypeOfGroup, IdPenilai, tokenContainer.IdRekananContact.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwPertanyaanNilai>>(responseData);
                PertanyaanNilaiMulti myDataMulti = new PertanyaanNilaiMulti();
                myDataMulti.PertanyaanColls = myData;
                myDataMulti.PilihanScore = new List<SimpleRef> 
                { 
                    new SimpleRef { RefId = 1, RefDescription = "1" }
                    , new SimpleRef { RefId = 2, RefDescription = "2" }
                    , new SimpleRef { RefId = 3, RefDescription = "3" }
                    , new SimpleRef { RefId = 4, RefDescription = "4" }
                    , new SimpleRef { RefId = 5, RefDescription = "5" } };
                ViewBag.IdTypeOfGroup = IdTypeOfGroup;
                ViewBag.IdPenilai = IdPenilai;
                return PartialView("_GetNilaiByPenilai", myDataMulti);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetNilaiAkhir()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiAkhir/{1}", url, tokenContainer.IdRekananContact.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwPertanyaanNilaiAkhir>>(responseData);
                return View("GetNilaiAkhir", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetNilaiByParam(Guid IdRekanan, int Periode, int IdTypeOfGroup, int IdPenilai)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiByParam/{1}/{2}/{3}/{4}", url, IdRekanan, Periode, IdTypeOfGroup, IdPenilai ));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fGetPertanyaanNilaiByParam_Result>>(responseData);
                return View("GetNilaiByParam", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetNilaiByParam(Guid IdRekanan, int Periode, int IdTypeOfGroup, int IdPenilai)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiByParam/{1}/{2}/{3}/{4}", url, IdRekanan, Periode, IdTypeOfGroup, IdPenilai));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fGetPertanyaanNilaiByParam_Result>>(responseData);
                return PartialView("_GetNilaiByParam", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetNilaiAkhir()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiAkhir/{1}", url, tokenContainer.IdRekananContact.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwPertanyaanNilaiAkhir>>(responseData);
                return PartialView("_GetNilaiAkhir", myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UpdateLineNilai(vwPertanyaanNilai myData)
        {
            int RdoSelected = DevExpress.Web.Mvc.RadioButtonListExtension.GetValue<int>("rdo" + myData.IdTNPertanyaan.ToString());
            myData.Nilai = RdoSelected;
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + myData.IdTNPertanyaan, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage2 = await client.GetAsync(string.Format("{0}/GetNilaiByPenilai/{1}/{2}/{3}", url, myData.IdTypeOfGroup, myData.IdPenilai, tokenContainer.IdRekananContact.ToString()));
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    var myDataColls = JsonConvert.DeserializeObject<List<vwPertanyaanNilai>>(responseData);
                    PertanyaanNilaiMulti myDataMulti = new PertanyaanNilaiMulti();
                    myDataMulti.PertanyaanColls = myDataColls;
                    myDataMulti.PilihanScore = new List<SimpleRef> 
                { 
                    new SimpleRef { RefId = 1, RefDescription = "1" }
                    , new SimpleRef { RefId = 2, RefDescription = "2" }
                    , new SimpleRef { RefId = 3, RefDescription = "3" }
                    , new SimpleRef { RefId = 4, RefDescription = "4" }
                    , new SimpleRef { RefId = 5, RefDescription = "5" } };
                    ViewBag.IdTypeOfGroup = myData.IdTypeOfGroup;
                    ViewBag.IdPenilai = myData.IdPenilai;
                    return PartialView("_GetNilaiByPenilai", myDataMulti);
                }
            }
            return RedirectToAction("Error");
        }

    }
}
