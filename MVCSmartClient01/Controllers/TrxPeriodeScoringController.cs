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
    public class TrxPeriodeScoringController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/trxPeriodeScoring";
        string url = string.Empty;
        string urlTanyaNilai = string.Empty;
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxPeriodeScoringController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/trxPeriodeScoring", SmartAPIUrl);
            urlTanyaNilai = string.Format("{0}/api/TrxPertanyaanNilai", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<trxPeriodeScoring>>(responseData);
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
                var myData = JsonConvert.DeserializeObject<List<fPeriodeScoringByRekanan_Result>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> PeriodeScoringByCurRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPeriodeScoringByRekanan_Result>>(responseData);
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();
                ViewBag.RoleName = tokenContainer.RoleName.ToString().Substring(0,7);
                return View("PeriodeScoringByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> PeriodeScoringByRek(Guid IdRekanan)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + IdRekanan);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPeriodeScoringByRekanan_Result>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.RoleName = tokenContainer.RoleName.ToString();
                return View("PeriodeScoringByRek", myData);
            }
            return View("Error");
        }
        private void SetPairGroupScoring(int IdTypeOfRekanan)
        {
            switch (IdTypeOfRekanan)
            {
                case 2:
                    ViewBag.Grup1 = "PPG";
                    ViewBag.Grup2 = "CCR";
                    break;
                default:
                    ViewBag.Grup1 = "PPG";
                    ViewBag.Grup2 = "FOG";
                    break;
            }
        }
        public async Task<ActionResult> DetailScoringByPeriode(Guid IdRekanan, int TahunBulan, int IdTypeOfRekanan)
        {
            ViewBag.IdRekanan = IdRekanan;
            ViewBag.TahunBulan = TahunBulan;
            SetPairGroupScoring(IdTypeOfRekanan);
            return View("DetailScoringByPeriode");
        }
        public async Task<ActionResult> DetailScoringByPeriodeRek(Guid IdRekanan, int TahunBulan, int IdTypeOfRekanan)
        {
            ViewBag.IdRekanan = IdRekanan;
            ViewBag.TahunBulan = TahunBulan;
            SetPairGroupScoring(IdTypeOfRekanan);
            return View("DetailScoringByPeriodeRek");
        }
        public async Task<ActionResult> _DetailScoringByPeriode(Guid IdRekanan, int TahunBulan, int IdTypeOfGroup, int IdPenilai)
        {
            string strJudulGroup = string.Empty;
            switch (IdTypeOfGroup)
            {
                case 1 :
                    strJudulGroup = "Wawancara";
                    break;
                case 2 :
                    strJudulGroup = "Seleksi Administrasi";
                    break;
                case 3 :
                    strJudulGroup = "Survey OTS";
                    break;
            }
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetNilaiByParam/{1}/{2}/{3}/{4}"
                , urlTanyaNilai, IdRekanan, TahunBulan, IdTypeOfGroup, IdPenilai)).Result;
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
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.TahunBulan = TahunBulan;
                ViewBag.IdTypeOfGroup = IdTypeOfGroup;
                ViewBag.IdPenilai = IdPenilai;
                ViewBag.JudulGroup = strJudulGroup;
                return PartialView("_DetailScoringByPeriode", myDataMulti);
            }
            return View("Error");
        }
        //public async Task<ActionResult> GetNilaiAkhir(Guid IdRekanan, int TahunBulan)
        //{
        //    HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetNilaiAkhir/{1}/{2}", urlTanyaNilai, IdRekanan, TahunBulan));
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var myData = JsonConvert.DeserializeObject<List<vwPertanyaanNilaiAkhir>>(responseData);
        //        ViewBag.IdRekanan = IdRekanan;
        //        ViewBag.TahunBulan = TahunBulan;

        //        return View("GetNilaiAkhir", myData);
        //    }
        //    return View("Error");
        //}
        public async Task<ActionResult> _GetNilaiAkhir(Guid IdRekanan, int TahunBulan)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetNilaiAkhir/{1}/{2}", urlTanyaNilai, IdRekanan, TahunBulan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwPertanyaanNilaiAkhir>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                ViewBag.TahunBulan = TahunBulan;
               
                return PartialView("_GetNilaiAkhir", myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UpdateLineNilai(vwPertanyaanNilai myData)
        {
            string strJudulGroup = string.Empty;
            int RdoSelected = DevExpress.Web.Mvc.RadioButtonListExtension.GetValue<int>("rdo" + myData.IdTNPertanyaan.ToString());
            myData.Nilai = RdoSelected;
            HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(urlTanyaNilai + "/" + myData.IdTNPertanyaan, myData);
            if (responseMessage1.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetNilaiByParam/{1}/{2}/{3}/{4}"
                    , urlTanyaNilai, myData.IdRekanan, myData.TahunBulan, myData.IdTypeOfGroup, myData.IdPenilai)).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
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
                    switch (myData.IdTypeOfGroup)
                    {
                        case 1:
                            strJudulGroup = "Wawancara";
                            break;
                        case 2:
                            strJudulGroup = "Seleksi Administrasi";
                            break;
                        case 3:
                            strJudulGroup = "Survey OTS";
                            break;
                    }

                    ViewBag.IdRekanan = myData.IdRekanan;
                    ViewBag.TahunBulan = myData.TahunBulan;
                    ViewBag.IdTypeOfGroup = myData.IdTypeOfGroup;
                    ViewBag.IdPenilai = myData.IdPenilai;
                    ViewBag.JudulGroup = strJudulGroup;
                    return PartialView("_DetailScoringByPeriode", myDataMulti);
                }
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> _PeriodeScoringByRek(Guid IdRekanan)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + IdRekanan);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPeriodeScoringByRekanan_Result>>(responseData);
                ViewBag.IdRekanan = IdRekanan;
                return PartialView("_PeriodeScoringByRek", myData);
            }
            return View("Error");
        }

        public ActionResult AddPeriode(Guid IdRekanan)
        {
            return View(new trxPeriodeScoring(IdRekanan));
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> AddPeriode(trxPeriodeScoring Emp)
        {
            Emp.CreatedDate = DateTime.Today;
            Emp.CreatedUser = tokenContainer.UserId.ToString();
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("PeriodeScoringByRek", "TrxPeriodeScoring", new { IdRekanan = Emp.IdRekanan });
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> EditPeriode(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var Employee = JsonConvert.DeserializeObject<trxPeriodeScoring>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> EditPeriode(int id, trxPeriodeScoring Emp)
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
                var Employee = JsonConvert.DeserializeObject<trxPeriodeScoring>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxPeriodeScoring Emp)
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
