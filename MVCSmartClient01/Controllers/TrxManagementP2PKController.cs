using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using System.Web;

using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using ApiHelper;
using System.Configuration;
using Omu.ValueInjecter;
using DevExpress.Data.Filtering;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.Mvc;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxManagementP2PKController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/MstRekanan";
        string url = string.Empty;
 
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxManagementP2PKController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstRekanan", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ActionResult GetManagementP2PK(int IdTypeOfRekanan, bool IsActive)
        {
            ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
            ViewBag.IsActive = IsActive;
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetManagementP2PK/{1}/{2}", url, IdTypeOfRekanan.ToString(), IsActive.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<trxManagementP2PK>>(responseData);

                return View("GetManagementP2PK", myData);
            }
            return View("Error");
        }
        public ActionResult _GetManagementP2PK(int IdTypeOfRekanan, bool IsActive)
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetManagementP2PK/{1}/{2}", url, IdTypeOfRekanan.ToString(), IsActive.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<trxManagementP2PK>>(responseData);

                return PartialView("_GetManagementP2PK", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListRekanan(string strFilterExpression)
        {
            string strFilterExp = string.Empty;
            if (string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExp = "1 = 1";
            }
            else
            {
                strFilterExp = strFilterExpression;
            }
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_RekByIdSupervisor/{1}/{2}", url, tokenContainer.SupervisorId.ToString(), strFilterExp));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fXLS_RekByIdSupervisor_Result>>(responseData);

                XlsExportOptions xlsOption = new XlsExportOptions();

                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarRekanan(strFilterExp), myData, "XLSRekanan", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListManagementRekanan(string strFilterExpression)
        {
            string strFilterExp = string.Empty;
            if (string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExp = "1 = 1";
            }
            else
            {
                strFilterExp = strFilterExpression;
            }
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_ManagementRekanan/{1}", url, strFilterExp));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fManagementRekanan_Result>>(responseData);

                XlsExportOptions xlsOption = new XlsExportOptions();

                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_ManagementRek(strFilterExp), myData, "XLSManagementRekanan", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public ActionResult _InfoLastModifiedByRek(Guid IdRekanan, int IdTypeOfRekanan, string RegistrationNumber)
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetInfoLastModifiedByRek/{1}", url, IdRekanan.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fInfoLastModifiedByRek_Result>>(responseData);

                ViewBag.IdRekanan = IdRekanan;
                ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
                ViewBag.RegistrationNumber = RegistrationNumber;
                return PartialView(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RekananDetailedInfo(Guid IdRekanan, int IdTypeOfRekanan, string RegistrationNumber)
        {
            //assign current IdRekanan with selected id rekanan 
            tokenContainer.IdRekananContact = IdRekanan;
            tokenContainer.IdTypeOfRekanan = IdTypeOfRekanan;
            ViewBag.CurrentTab0 = "CreateEdit_ReadTab";
            ViewBag.IdTypeOfRekanan = IdTypeOfRekanan;
            ViewBag.InfoRekanan = String.Format("Informasi Detail Rekanan ({0})", RegistrationNumber);
            return View();
        }
    }
}
