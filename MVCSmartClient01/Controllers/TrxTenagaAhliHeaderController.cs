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
    public class TrxTenagaAhliHeaderController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/trxTenagaAhliHeader";
        string url = string.Empty;
        string urlTA = string.Empty;
        string urlTATMP = string.Empty;
        string urlTPTMP = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxTenagaAhliHeaderController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxTenagaAhliHeader", SmartAPIUrl);
            urlTA = string.Format("{0}/api/TrxTenagaAhli", SmartAPIUrl);
            urlTATMP = string.Format("{0}/api/TrxTenagaAhliTMP", SmartAPIUrl);
            urlTPTMP = string.Format("{0}/api/TrxTenagaPendukungTMP", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
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
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _TAhliHeaderByRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                return PartialView("_TAhliHeaderByRek", myData);
            }
            return View("Error");
        }

        public ActionResult Create()
        {
            return View(new trxTenagaAhliHeader());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxTenagaAhliHeader Emp)
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
                var myData = JsonConvert.DeserializeObject<trxTenagaAhliHeader>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxTenagaAhliHeader Emp)
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
                var Employee = JsonConvert.DeserializeObject<trxTenagaAhliHeader>(responseData);
                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxTenagaAhliHeader Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _TenagaByRek(int IdTenaga = -1)
        {
            string strUrlResponse = string.Format("{0}/GetByRekanan/{1}/{2}", url, IdTenaga.ToString(), tokenContainer.IdRekananContact.ToString());
            //HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/1/" + tokenContainer.IdRekananContact.ToString()).Result;
            HttpResponseMessage responseMessage = client.GetAsync(strUrlResponse).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                return PartialView("_TenagaByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _TATetapByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/1/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                ViewBag.IdTenaga = 1;
                return PartialView("_TenagaByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _TATidakTetapByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/2/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                ViewBag.IdTenaga = 2;
                return PartialView("_TenagaByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _TPendukungByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/3/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                ViewBag.IdTenaga = 3;
                return PartialView("_TenagaByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _TPendukungUploadByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetUploadByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<TrxTenagaAhliUpload>>(responseData);
                return PartialView("_TenagaUploadByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditTAUpload(int Id)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await client.GetAsync(url + "/GetTAUpload/" + Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<TrxTenagaAhliUpload>(responseData);

                if (Id < 0)
                {
                    myData = new TrxTenagaAhliUpload();
                    myData.ImageBaseName = System.Guid.NewGuid();
                    myData.CreatedUser = tokenContainer.UserId.ToString();
                    myData.CreatedDate = DateTime.Today;
                }
                return View("_AddEditTAUpload", myData);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> _AddEditTAUpload(TrxTenagaAhliUpload myDataForm)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            TrxTenagaAhliUpload myData = new TrxTenagaAhliUpload();

            myData.InjectFrom(myDataForm);
            if (myData.IdTAUpload > 0)
            {
                responseMessage = await client.PutAsJsonAsync(url + "/PutTAUpload/" + myData.IdTAUpload, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    //return RedirectToAction("_TPendukungUploadByRek");
                    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                responseMessage = await client.PostAsJsonAsync(url + "/PostTAUpload/", myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> DeleteTAUpload(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/DeleteTAUpload/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
            }
            return View("Error");
        }

        public ActionResult _AddEditTAHeader(Guid GuidHeader)
        {
            if(GuidHeader == null)
            {
                GuidHeader = Guid.Empty;
            }
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByGuidHeader/" + GuidHeader.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataForm = JsonConvert.DeserializeObject<trxTenagaAhliHeaderForm>(responseData);
                ViewBag.GuidHeader = GuidHeader;

                switch (myDataForm.IdTipeTenaga)
                {
                    case 1:
                    case 2:
                        return PartialView("_AddEditTAHeader", myDataForm.TenagaAhliColls);
                    case 3:
                        return PartialView("_AddEditTPHeader", myDataForm.TenagaPendukungColls);
                    default:
                        break;
                }
            }
            return View("Error");

        }
        public async Task<ActionResult> AddEditTAHeader(int IdTenagaAhliHeader, Guid GuidHeader, int IdTenaga = -1)
        {
            trxTenagaAhliHeaderForm myDataForm = new trxTenagaAhliHeaderForm();
            //if (GuidHeader != null && GuidHeader != Guid.Empty && IdTenagaAhliHeader > 0)
            if (IdTenagaAhliHeader > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByGuidHeader/" + GuidHeader.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataForm = JsonConvert.DeserializeObject<trxTenagaAhliHeaderForm>(responseData);
                    ViewBag.GuidHeader = GuidHeader;
                    ViewData["GuidHeader"] = GuidHeader;
                    switch (myDataForm.IdTipeTenaga)
                    {
                        case 1:
                            ViewBag.JudulDaftarTenaga = "Data Tenaga Ahli Tetap";
                            ViewBag.Tetap = true;
                            return View("AddEditTAHeader", myDataForm);
                        case 2:
                            ViewBag.JudulDaftarTenaga = "Data Tenaga Ahli Tidak Tetap";
                            ViewBag.Tetap = false;
                            return View("AddEditTAHeader", myDataForm);
                        case 3:
                            ViewBag.JudulDaftarTenaga = "Data Tenaga Pendukung";
                            return View("AddEditTPHeader", myDataForm);
                        default:
                            break;
                    }
                }
            }
            else
            {
                myDataForm.IdTenagaAhliHeader = -1;
                ViewBag.GuidHeader = Guid.Empty;
                switch (IdTenaga)
                {
                    case 1:
                        myDataForm.IdTipeTenaga = 1;
                        ViewBag.JudulDaftarTenaga = "Data Tenaga Ahli Tetap";
                        ViewBag.Tetap = true;
                       return View("AddEditTAHeader", myDataForm);
                    case 2:
                        myDataForm.IdTipeTenaga = 2;
                        ViewBag.JudulDaftarTenaga = "Data Tenaga Ahli Tidak Tetap";
                        ViewBag.Tetap = false;
                        return View("AddEditTAHeader", myDataForm);
                    case 3:
                        myDataForm.IdTipeTenaga = 3;
                        ViewBag.JudulDaftarTenaga = "Data Tenaga Pendukung";
                        return View("AddEditTPHeader", myDataForm);
                    default:
                        break;
                }
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> AddEditTAHeader(trxTenagaAhliHeaderForm myDataForm)
        {
            string strXLSPointer = myDataForm.GuidHeader.ToString();
            string strReqToExecTransData = string.Empty;
            trxTenagaAhliHeader myData = new trxTenagaAhliHeader();
            myData.InjectFrom(myDataForm);
            myData.IdRekanan = (Guid) tokenContainer.IdRekananContact;
            switch (myData.IdTipeTenaga)
            {
                case 1 :
                    //strReqToExecTransData = string.Format("{0}/ExecTransDataTA/{1}/{2}/{3}"
                    //, url, myData.IdTenagaAhliHeader.ToString(), tokenContainer.IdRekananContact.ToString(), strXLSPointer);
                    strReqToExecTransData = string.Format("{0}/ExecTransDataTA/{1}", url, strXLSPointer);
                    myData.ProcInfo = "Update: Daftar Tenaga Ahli Tetap";
                    break;
                case 2:
                    //strReqToExecTransData = string.Format("{0}/ExecTransDataTATT/{1}/{2}/{3}"
                    //, url, myData.IdTenagaAhliHeader.ToString(), tokenContainer.IdRekananContact.ToString(), strXLSPointer);
                    //strReqToExecTransData = string.Format("{0}/ExecTransDataTATT/{1}", url, strXLSPointer);
                    strReqToExecTransData = string.Format("{0}/ExecTransDataTA/{1}", url, strXLSPointer);
                    myData.ProcInfo = "Update: Daftar Tenaga Ahli Tidak Tetap";
                    break;
                case 3:
                    //strReqToExecTransData = string.Format("{0}/ExecTransDataTP/{1}/{2}/{3}"
                    //, url, myData.IdTenagaAhliHeader.ToString(), tokenContainer.IdRekananContact.ToString(), strXLSPointer);
                    strReqToExecTransData = string.Format("{0}/ExecTransDataTP/{1}", url, strXLSPointer);
                    myData.ProcInfo = "Update: Daftar Tenaga Pendukung";
                    break;
            }

            //pindahkan data temporary import XLS ke fix table berdasarkan XLS pointer guid
            if (strXLSPointer != string.Empty && strXLSPointer != Guid.Empty.ToString())
            {
                HttpResponseMessage responseMessageXLS = await client.GetAsync(strReqToExecTransData);

                if (!responseMessageXLS.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error");
                }
            }
            myData.JudulDokumen = string.Format("Diupload tanggal : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            if (myData.IdTenagaAhliHeader > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdTenagaAhliHeader, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                }
                return RedirectToAction("Error");
            }
            else
            {
                //myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                }
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddEditTPHeader(trxTenagaAhliHeaderForm myDataForm)
        {
            string strXLSPointer = myDataForm.GuidHeader.ToString();
            //if (tokenContainer.XLSPointer != null)
            //{
            //    strXLSPointer = tokenContainer.XLSPointer.ToString();
            //}
            //pindahkan data temporary import XLS ke fix table berdasarkan XLS pointer guid
            //string strReqToExecTransData = string.Format("{0}/ExecTransDataTP/{1}/{2}/{3}"
            //    , url, myData.IdTenagaAhliHeader.ToString(), tokenContainer.IdRekananContact.ToString(), strXLSPointer);

            trxTenagaAhliHeader myData = new trxTenagaAhliHeader();
            myData.InjectFrom(myDataForm);
            string strReqToExecTransData = string.Format("{0}/ExecTransDataTP/{1}", url, strXLSPointer);
            if (strXLSPointer != string.Empty)
            {
                HttpResponseMessage responseMessageXLS = await client.GetAsync(strReqToExecTransData);

                if (!responseMessageXLS.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error");
                }
            }
            myData.JudulDokumen = string.Format("Diupload tanggal : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            if (myData.IdTenagaAhliHeader > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdTenagaAhliHeader, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                }
                return RedirectToAction("Error");
            }
        }

        public ActionResult _PopulateTAFromFile()
        {
            List<trxTenagaAhliTetapImp> myDataList = new List<trxTenagaAhliTetapImp>();
            return PartialView(myDataList);
        }

        [HttpPost]
        //public ActionResult _PopulateTAFromFile(HttpPostedFileBase file, string myCatatan, string yourCatatan)
        public ActionResult _PopulateTAFromFile(HttpPostedFileBase file, int IdTenagaAhliHeader, int IdTipeTenaga)
        {
            bool bolImported = false;
            int intBarisMulaiData = 0;
            int intKolomMulaiData = 0;
            //var value = HttpContext.CurrentHandler.ProcessRequest["myHiddenField"];
            try
            {
                intBarisMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["BarisDataXLS_TA"]);
            }
            catch (Exception ex)
            { }
            try
            {
                intKolomMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["KolomDataXLS_TA"]);
            }
            catch (Exception ex)
            { }
            Guid gdPointer = Guid.NewGuid();
            //tokenContainer.XLSPointer = gdPointer;

            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                //string strFileName = Request.Files["file"].FileName;
                string strFileName = gdPointer.ToString() + fileExtension;
                string fileLocation = Server.MapPath("~/Content/FileUpload/") + strFileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["file"].SaveAs(fileLocation);

                bolImported = ImportXLSHelper.ImportXLS(1, gdPointer, (Guid)tokenContainer.IdRekananContact, intBarisMulaiData, intKolomMulaiData, strFileName, fileExtension, fileLocation);
                ViewBag.Message = string.Format("Proses import {0}", "Berhasil");
                //populate form header and gid from temp table
                trxTenagaAhliHeaderForm myDataForm = new trxTenagaAhliHeaderForm();

                //populate grid data
                List<trxTenagaAhli> myDataList = new List<trxTenagaAhli>();
                HttpResponseMessage responseMessage = client.GetAsync(urlTATMP + "/GetByGuidHeader/" + gdPointer.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataList = JsonConvert.DeserializeObject<List<trxTenagaAhli>>(responseData);
                }
                myDataForm.TenagaAhliColls = myDataList;
                //if (IdTipeTenaga == 1)
                //{
                //    myDataForm.TenagaAhliColls = myDataList;
                //}
                //else
                //{
                //    myDataForm.TenagaAhliTTColls = myDataList;
                //}
                myDataForm.IdTenagaAhliHeader = IdTenagaAhliHeader;
                myDataForm.IdTipeTenaga = IdTipeTenaga;
                myDataForm.GuidHeader = gdPointer;
                myDataForm.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                ViewBag.GuidHeader = gdPointer;
                return View("AddEditTAHeader", myDataForm);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult _PopulateTPFromFile(HttpPostedFileBase file, int IdTenagaAhliHeader)
        {
            bool bolImported = false;
            int intBarisMulaiData = 0;
            int intKolomMulaiData = 0;
            try
            {
                intBarisMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["BarisDataXLS_TP"]);
            }
            catch (Exception ex)
            { }
            try
            {
                intKolomMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["KolomDataXLS_TP"]);
            }
            catch (Exception ex)
            { }
            Guid gdPointer = Guid.NewGuid();
            //tokenContainer.XLSPointer = gdPointer;

            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                //string strFileName = Request.Files["file"].FileName;
                string strFileName = gdPointer.ToString() + fileExtension;
                string fileLocation = Server.MapPath("~/Content/FileUpload/") + strFileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["file"].SaveAs(fileLocation);

                bolImported = ImportXLSHelper.ImportXLS(3, gdPointer, (Guid)tokenContainer.IdRekananContact, intBarisMulaiData, intKolomMulaiData, strFileName, fileExtension, fileLocation);
                ViewBag.Message = string.Format("Proses import {0}", "Berhasil");

                //populate form header and gid from temp table
                trxTenagaAhliHeaderForm myDataForm = new trxTenagaAhliHeaderForm();

                //populate grid data
                List<trxTenagaPendukung> myDataList = new List<trxTenagaPendukung>();
                HttpResponseMessage responseMessage = client.GetAsync(urlTPTMP + "/GetByGuidHeader/" + gdPointer.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataList = JsonConvert.DeserializeObject<List<trxTenagaPendukung>>(responseData);
                }
                myDataForm.TenagaPendukungColls = myDataList;

                myDataForm.IdTenagaAhliHeader = IdTenagaAhliHeader;
                myDataForm.IdTipeTenaga = 3;
                myDataForm.GuidHeader = gdPointer;
                myDataForm.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                ViewBag.GuidHeader = gdPointer;
                return View("AddEditTPHeader", myDataForm);
            }
            return View("Error");
        }

        public FilePathResult FileTemplate(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }


    }
}
