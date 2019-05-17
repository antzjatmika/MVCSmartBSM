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
    public class TrxKonsolidasiController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/trxKonsolidasi";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxKonsolidasiController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxKonsolidasi", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<trxKonsolidasi>>(responseData);
                return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetKonsoByPeriode(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKonsoByPeriode/{1}/{2}", url, IdRekanan, TahunBulan));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.TahunBulan = TahunBulan;
                return View("GetKonsoByPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetKonsoByPeriode(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKonsoByPeriode/{1}/{2}", url, IdRekanan, TahunBulan));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.TahunBulan = TahunBulan;
                return PartialView("_GetKonsoByPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetKonsoResumeByPeriode(Guid IdRekanan, int TahunBulan, int TipeUraian)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetKonsoResumeByPeriode/{1}/{2}/{3}", url, IdRekanan, TahunBulan, TipeUraian)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoResumeByPeriode_Result>>(responseData);
                ViewBag.TahunBulan = TahunBulan;
                ViewBag.TipeUraian = TipeUraian;

                return View("GetKonsoResumeByPeriode", myData);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> UpdateKonsolidasiPair(int IdCur, fKonsoPairByParam_Result Emp)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/UpdateKonsolidasiPair", Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                //return RedirectToAction("GetKonsoPairByParam", new { IdRekanan = tokenContainer.IdRekananContact, TahunBulan = Emp.TahunBulanCurOri });
                HttpResponseMessage responseMessage1 = client.GetAsync(String.Format("{0}/GetKonsoPairByParam/{1}/{2}", url, tokenContainer.IdRekananContact
                    , Emp.TahunBulanCurOri)).Result;
                if (responseMessage1.IsSuccessStatusCode)
                {
                    var responseData = responseMessage1.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<List<fKonsoPairByParam_Result>>(responseData);
                    ViewBag.IdRekanan = tokenContainer.IdRekananContact;
                    ViewBag.TahunBulan = Emp.TahunBulanCurOri;
                    ViewBag.IsEditable = true;
                    return PartialView("_GetKonsoPairByParam", myData);
                }
            }
            //return RedirectToAction("Error");
            return View("Error");
        }
        public async Task<ActionResult> _GetKonsoResumeByPeriode(Guid IdRekanan, int TahunBulan, int TipeUraian)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetKonsoResumeByPeriode/{1}/{2}/{3}", url, IdRekanan, TahunBulan, TipeUraian)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoResumeByPeriode_Result>>(responseData);
                ViewBag.TahunBulan = TahunBulan;
                ViewBag.TipeUraian = TipeUraian;
                ViewBag.PeriodeAkhir = (TahunBulan > 0) ? TahunBulan.ToString() : "";
                ViewBag.PeriodeAwal = (TahunBulan > 0) ? (TahunBulan - 100).ToString() : "";
                return PartialView("_GetKonsoResumeByPeriode", myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UpdateLineNilaiKonso(fKonsoByPeriode_Result myData)
        {
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + myData.IdTrxKonsolidasi, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage2 = await client.GetAsync(string.Format("{0}/GetKonsoByPeriode/{1}/{2}", url, myData.IdRekanan, myData.TahunBulan));
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    var myDataColls = JsonConvert.DeserializeObject<List<fKonsoByPeriode_Result>>(responseData);
                    ViewBag.IdTypeOfGroup = myData.IdRekanan;
                    ViewBag.IdPenilai = myData.TahunBulan;
                    return PartialView("_GetKonsoByPeriode", myDataColls);
                }
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> GetKategoriResikoByPenambah(decimal decPenambah)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKategoriResikoByPenambah/{1}", url, decPenambah));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKategoriResiko_Result>>(responseData);
                ViewBag.DecPenambah = decPenambah;
                return View("GetKategoriResikoByPenambah", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetKategoriResikoByPenambah(decimal decPenambah)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKategoriResikoByPenambah/{1}", url, decPenambah));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKategoriResiko_Result>>(responseData);
                ViewBag.DecPenambah = decPenambah;
                return PartialView("_GetKategoriResikoByPenambah", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetScoringByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetScoringByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fScoringByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return View("GetScoringByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetScoringByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetScoringByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fScoringByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return PartialView("_GetScoringByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetScoringResumeByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetScoringResumeByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fScoringResumeByRek_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return PartialView("_GetScoringResumeByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetScoringMultiByRekPeriode(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetScoringMultiByRekPeriode/{1}/{2}", url, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<scoringResumeMulti>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.Periode = TahunBulan;
                return View("GetScoringMultiByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetScoringMultiByRekPeriode(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetScoringMultiByRekPeriode/{1}/{2}", url, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<scoringResumeMulti>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.Periode = TahunBulan;
                return PartialView("_GetScoringMultiByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetResumeRoaByRekPeriode(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetResumeRoaByRekPeriode/{1}/{2}", url, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fResumeRoaByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.TahunBulan = TahunBulan;
                return View("GetResumeRoaByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetResumeRoaByRekPeriode(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetResumeRoaByRekPeriode/{1}/{2}", url, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fResumeRoaByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.TahunBulan = TahunBulan;
                return PartialView("_GetResumeRoaByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetKonsoPairByParam(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetKonsoPairByParam/{1}/{2}", url, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoPairByParam_Result>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.TahunBulan = TahunBulan;
                return View("_GetKonsoPairByParam", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetKonsoPairByParam(Guid IdRekanan, int TahunBulan, bool IsEditable = true)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetKonsoPairByParam/{1}/{2}", url, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoPairByParam_Result>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.TahunBulan = TahunBulan;
                ViewBag.IsEditable = IsEditable;
                return PartialView("_GetKonsoPairByParam", myData);
            }
            return View("Error");
        }
    }
}
