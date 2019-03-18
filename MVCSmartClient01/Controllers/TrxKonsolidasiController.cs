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
        public async Task<ActionResult> GetKonsoByPeriode(int intPeriode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKonsoByPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), intPeriode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.IntPeriode = intPeriode;
                return View("GetKonsoByPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetKonsoByPeriode(int intPeriode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKonsoByPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), intPeriode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.IntPeriode = intPeriode;
                return PartialView("_GetKonsoByPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetKonsoResumeByPeriode(int PeriodeAwal, int PeriodeAkhir, int TipeUraian)
        {
            Guid IdRekanan = new Guid();
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKonsoResumeByPeriode/{1}/{2}/{3}/{4}", url, IdRekanan, PeriodeAwal, PeriodeAkhir, TipeUraian));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoResumeByPeriode_Result>>(responseData);
                ViewBag.PeriodeAwal = PeriodeAwal;
                ViewBag.PeriodeAkhir = PeriodeAkhir;
                ViewBag.TipeUraian = TipeUraian;

                return View("GetKonsoResumeByPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetKonsoResumeByPeriode(int PeriodeAwal, int PeriodeAkhir, int TipeUraian)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetKonsoResumeByPeriode/{1}/{2}/{3}/{4}", url, tokenContainer.IdRekananContact.ToString(), PeriodeAwal, PeriodeAkhir, TipeUraian));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fKonsoResumeByPeriode_Result>>(responseData);
                ViewBag.PeriodeAwal = PeriodeAwal;
                ViewBag.PeriodeAkhir = PeriodeAkhir;
                ViewBag.TipeUraian = TipeUraian;

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
        public async Task<ActionResult> GetScoringMultiByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetScoringMultiByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<scoringResumeMulti>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return View("GetScoringMultiByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetScoringMultiByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetScoringMultiByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<scoringResumeMulti>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return PartialView("GetScoringMultiByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetResumeRoaByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetResumeRoaByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fResumeRoaByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return View("GetResumeRoaByRekPeriode", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetResumeRoaByRekPeriode(int Periode)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetResumeRoaByRekPeriode/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), Periode));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fResumeRoaByPeriode_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.Periode = Periode;
                return PartialView("_GetResumeRoaByRekPeriode", myData);
            }
            return View("Error");
        }
    }
}
