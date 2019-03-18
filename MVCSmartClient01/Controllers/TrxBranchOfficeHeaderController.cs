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

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxBranchOfficeHeaderController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxBranchOffice";
        string url = string.Empty;
        string urlBranch = string.Empty;
        string urlBranchTMP = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxBranchOfficeHeaderController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxBranchOfficeHeader", SmartAPIUrl);
            urlBranch = string.Format("{0}/api/TrxBranchOffice", SmartAPIUrl);
            urlBranchTMP = string.Format("{0}/api/TrxBranchOfficeTMP", SmartAPIUrl);

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
               var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;

               var myData = JsonConvert.DeserializeObject<trxBranchOfficeMulti>(responseData);

               return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdOrganisasi.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxBranchOfficeMulti>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        public async Task<ActionResult> AddEditBranchHeader(Guid GuidHeader)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByGuidHeader/" + GuidHeader.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxBranchOfficeHeaderForm>(responseData);
                ViewData["GuidHeader"] = GuidHeader;
                return View(myData);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> AddEditBranchHeader(trxBranchOfficeHeaderForm myDataForm)
        {
            string strXLSPointer = myDataForm.GuidHeader.ToString();
            string strReqToExecTransData = string.Empty;
            trxBranchOfficeHeader myData = new trxBranchOfficeHeader();
            myData.InjectFrom(myDataForm);
            //pindahkan data temporary import XLS ke fix table berdasarkan XLS pointer guid
            if (strXLSPointer != string.Empty)
            {
                strReqToExecTransData = string.Format("{0}/ExecTransBranch/{1}", url, strXLSPointer);
                HttpResponseMessage responseMessageXLS = await client.GetAsync(strReqToExecTransData);

                if (!responseMessageXLS.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error");
                }
            }
            myData.JudulDokumen = string.Format("Diupload tanggal : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            if (myData.IdBranchHeader > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdBranchHeader, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> _BranchByGuidHeader(Guid GuidHeader)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlBranch + "/GetByGuidHeader/" + GuidHeader.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<trxBranchOffice>>(responseData);
                return PartialView(myData);
            }
            return View("Error");
        }
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int IdData, trxBranchOffice myData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _DeleteBranch(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdData.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxRekananDocument");
            }
            return RedirectToAction("Error");
        }

        public ActionResult _PopulateBranchFromFile(HttpPostedFileBase file, int IdBranchHeader)
        {
            bool bolImported = false;
            int intBarisMulaiData = 0;
            int intKolomMulaiData = 0;

            try
            {
                intBarisMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["BarisDataXLS_Branch"]);
            }
            catch (Exception ex)
            { }
            try
            {
                intKolomMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["KolomDataXLS_Branch"]);
            }
            catch (Exception ex)
            { }
            Guid gdPointer = Guid.NewGuid();

            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                string strFileName = gdPointer.ToString() + fileExtension;
                string fileLocation = Server.MapPath("~/Content/FileUpload/") + strFileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["file"].SaveAs(fileLocation);

                bolImported = ImportXLSHelper.ImportXLS(6, gdPointer, (Guid)tokenContainer.IdRekananContact, intBarisMulaiData, intKolomMulaiData, strFileName, fileExtension, fileLocation);
                ViewBag.Message = string.Format("Proses import {0}", "Berhasil");
                //populate form header and grid from temp table
                trxBranchOfficeHeaderForm myDataForm = new trxBranchOfficeHeaderForm();

                //populate grid data
                List<trxBranchOffice> myDataList = new List<trxBranchOffice>();
                HttpResponseMessage responseMessage = client.GetAsync(urlBranchTMP + "/GetByGuidHeader/" + gdPointer.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataList = JsonConvert.DeserializeObject<List<trxBranchOffice>>(responseData);
                }
                myDataForm.BranchOfficeColls = myDataList;

                myDataForm.IdBranchHeader = IdBranchHeader;
                myDataForm.GuidHeader = gdPointer;
                myDataForm.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                ViewBag.GuidHeader = gdPointer;
                return View("AddEditBranchHeader", myDataForm);
            }
            return View("Error");
        }

        public FilePathResult ListCabang(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }

    }
}
