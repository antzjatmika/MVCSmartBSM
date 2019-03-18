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
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxNotarisController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxNotaris";
        string url = string.Empty;
         
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxNotarisController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxNotaris", SmartAPIUrl);
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
                var responseData = responseMessage.Content.ReadAsStringAsync().Result ;
                var Employees = JsonConvert.DeserializeObject<List<trxNotaris>>(responseData); 
                return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> CreateEdit_ReadOld()
        {
            if ((System.Int32)tokenContainer.IdNotaris > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekananId/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);

                    ViewBag.SimpanVisi = "hidden";
                    ViewBag.EditVisi = "visible";
                    ViewBag.IsReadOnly = true;

                    return View("CreateEdit", myData);
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/0");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);

                    trxNotarisDetail myNotarisDetail = new trxNotarisDetail();
                    myNotarisDetail.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                    myNotarisDetail.ImageBaseName = Guid.NewGuid();
                    myNotarisDetail.CreatedDate = DateTime.Today;
                    myNotarisDetail.CreatedUser = tokenContainer.UserId.ToString();

                    myData.DetailNotaris = myNotarisDetail;

                    ViewBag.SimpanVisi = "hidden";
                    ViewBag.EditVisi = "visible";
                    ViewBag.IsReadOnly = true;

                    return View("CreateEdit", myData);
                }
                return View("Error");
            }
        }

        public async Task<ActionResult> CreateEdit_Read()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekananId/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);

                if (myData.DetailNotaris.IdRekanan == Guid.Empty)
                {
                    trxNotarisDetail myNotarisDetail = new trxNotarisDetail();
                    myNotarisDetail.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                    myNotarisDetail.ImageBaseName = Guid.NewGuid();
                    myNotarisDetail.CreatedDate = DateTime.Today;
                    myNotarisDetail.CreatedUser = tokenContainer.UserId.ToString();
                    myData.DetailNotaris = myNotarisDetail;
                }
                ViewData["enableTab"] = false;
                ViewBag.SimpanVisi = "hidden";
                ViewBag.EditVisi = "visible";
                ViewBag.IsReadOnly = true;

                return View("CreateEdit", myData);
            }
            return View("Error");
        }

        public async Task<ActionResult> PopulateTabular()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + tokenContainer.IdNotaris.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);

                ViewBag.SimpanVisi = "visible";
                ViewBag.EditVisi = "hidden";
                ViewBag.IsReadOnly = false;
                ViewData["enableTab"] = true;

                return View("CreateEdit", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _PopulateTabular()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/NotarisTabularGetByRek/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject <List<trxNotarisTabular>>(responseData);

                ViewBag.SimpanVisi = "visible";
                ViewBag.EditVisi = "hidden";
                ViewBag.IsReadOnly = false;
                ViewData["enableTab"] = true;

                return PartialView("CreateEditTabular", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetNotarisDetailAll()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/NotarisDetailAll");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwNotarisTabular>>(responseData);

                ViewBag.SimpanVisi = "visible";
                ViewBag.EditVisi = "hidden";
                ViewBag.IsReadOnly = false;
                ViewData["enableTab"] = true;

                return View("GetNotarisDetailAll", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetNotarisDetailAll()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/NotarisDetailAll");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<vwNotarisTabular>>(responseData);

                ViewBag.SimpanVisi = "visible";
                ViewBag.EditVisi = "hidden";
                ViewBag.IsReadOnly = false;

                return PartialView("_GetNotarisDetailAll", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> CreateEdit_EditOld()
        {
            if ((System.Int32)tokenContainer.IdNotaris > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekananId/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);

                    ImageContainerUpd.ImageBaseName = myData.DetailNotaris.ImageBaseName;
                    ImageContainerUpd.ImageExtensionKOP = myData.DetailNotaris.FileExtKoperasi;
                    ImageContainerUpd.ImageExtensionPAS = myData.DetailNotaris.FileExtPasarModal;

                    ViewBag.SimpanVisi = "visible";
                    ViewBag.EditVisi = "hidden";
                    ViewBag.IsReadOnly = false;

                    return View("CreateEdit", myData);
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/0");
                if (responseMessage.IsSuccessStatusCode)
                {
                    Guid myImageBaseName = Guid.NewGuid();

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);
                    myData.DetailNotaris.ImageBaseName = myImageBaseName;
                    ImageContainerUpd.ImageBaseName = myImageBaseName;

                    ViewBag.SimpanVisi = "visible";
                    ViewBag.EditVisi = "hidden";
                    ViewBag.IsReadOnly = false;

                    return View("CreateEdit", myData);
                }
                return View("Error");
            }
        }

        public async Task<ActionResult> CreateEdit_Edit()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekananId/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);

                if (myData.DetailNotaris.IdRekanan == Guid.Empty)
                {
                    trxNotarisDetail myNotarisDetail = new trxNotarisDetail();
                    myNotarisDetail.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                    myNotarisDetail.ImageBaseName = Guid.NewGuid();
                    myNotarisDetail.CreatedDate = DateTime.Today;
                    myNotarisDetail.CreatedUser = tokenContainer.UserId.ToString();
                    myData.DetailNotaris = myNotarisDetail;
                }

                //ImageContainerUpd.ImageBaseName = myData.DetailNotaris.ImageBaseName;
                //ImageContainerUpd.ImageExtensionKOP = myData.DetailNotaris.FileExtKoperasi;
                //ImageContainerUpd.ImageExtensionPAS = myData.DetailNotaris.FileExtPasarModal;

                ViewData["enableTab"] = true;
                ViewBag.SimpanVisi = "visible";
                ViewBag.EditVisi = "hidden";
                ViewBag.IsReadOnly = false;

                return View("CreateEdit", myData);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> CreateEditDetail(trxNotarisDetail myData)
        {
            bool bolSaveOK = false;
            myData.CreatedDate = DateTime.Today;
            myData.CreatedUser = tokenContainer.UserId.ToString();

            //myData.FileNameKoperasi = ImageContainerUpd.ImageFileKOP;
            //myData.FileExtKoperasi = ImageContainerUpd.ImageExtensionKOP;

            //myData.FileNamePasarModal = ImageContainerUpd.ImageFilePAS;
            //myData.FileExtPasarModal = ImageContainerUpd.ImageExtensionPAS;

            if (myData.IdNotarisDetail > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/AddUpdateNotarisDetail", myData);
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
                myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url + "/AddUpdateNotarisDetail", myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    bolSaveOK = true;
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            if(bolSaveOK)
            {
                return RedirectToAction("CreateEdit_Read");

                //HttpResponseMessage responseMessage1 = await client.GetAsync(url + "/NotarisDetailGetByRek/" + tokenContainer.IdRekananContact.ToString());
                //if (responseMessage1.IsSuccessStatusCode)
                //{
                //    var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                //    var myData1 = JsonConvert.DeserializeObject<trxNotarisDetail>(responseData1);

                //    ViewBag.SimpanVisi = "hidden";
                //    ViewBag.EditVisi = "visible";
                //    ViewBag.IsReadOnly = true;

                //    return PartialView("CreateEditDetail", myData1);
                //}
            }
            else
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<ActionResult> AddUpdateNotarisTabular(trxNotarisTabular myData)
        {
            bool bolSaveOK = false;

            if (myData.IdNotarisTabular > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/AddUpdateNotarisTabular", myData);
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
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url + "/AddUpdateNotarisTabular", myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    bolSaveOK = true;
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            if(bolSaveOK)
            {
                HttpResponseMessage responseMessage1 = await client.GetAsync(url + "/NotarisTabularGetByRek/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage1.IsSuccessStatusCode)
                {
                    var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                    var myData1 = JsonConvert.DeserializeObject<List<trxNotarisTabular>>(responseData1);

                    ViewBag.SimpanVisi = "visible";
                    ViewBag.EditVisi = "hidden";
                    ViewBag.IsReadOnly = false;
                    ViewData["enableTab"] = true;

                    return PartialView("CreateEditTabular", myData1);
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }
        public ActionResult Create()
        {
            return View(new trxNotaris());
        }
        public ActionResult ImageUpload(string UploaderCtl = "uploadControl")
        {
            ImageContainerUpd.ImageScanned = "Gambar";
            switch (UploaderCtl)
            {
                case "UCKeputusanMenKop":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_KOP";
                    break;
                case "UCSTTNPasarModal":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_PAS";
                    break;
                default:
                    break;
            }
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles(UploaderCtl, UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);
            //TempData["ContentType"] = arrUploaded[0].ContentType;
            return null;
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxNotaris Emp)
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

                var Employee = JsonConvert.DeserializeObject<trxNotaris>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxNotaris Emp)
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

                var Employee = JsonConvert.DeserializeObject<trxNotaris>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> DeleteNotarisTabular(int IdNotarisTabular)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/DeleteNotarisTabular/" + IdNotarisTabular.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage1 = await client.GetAsync(url + "/NotarisTabularGetByRek/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage1.IsSuccessStatusCode)
                {
                    var responseData1 = responseMessage1.Content.ReadAsStringAsync().Result;
                    var myData1 = JsonConvert.DeserializeObject<List<trxNotarisTabular>>(responseData1);

                    ViewBag.SimpanVisi = "visible";
                    ViewBag.EditVisi = "hidden";
                    ViewBag.IsReadOnly = false;
                    ViewData["enableTab"] = true;

                    return PartialView("CreateEditTabular", myData1);
                }
            }
            return View("Error");
        }
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxNotaris Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


        //public async Task<ActionResult> EditFormTemplateAddNewPartial()
        //{
        //    HttpResponseMessage responseMessage = await client.GetAsync(url + "/0");
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var myData = JsonConvert.DeserializeObject<trxNotarisForm>(responseData);

        //        ViewBag.SimpanVisi = "visible";
        //        ViewBag.EditVisi = "hidden";
        //        ViewBag.IsReadOnly = false;

        //        return PartialView("CreateEdit", myData);
        //    }
        //    return View("Error");
        //}
        public async Task<ActionResult> _NotarisTabularByRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekananId/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxNotarisFormNew>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> EditFormTemplateAddNewPartial(trxNotarisTabular myNotarisTabular)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myNotarisTabular);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["EditableProduct"] = myNotarisTabular;
            }
            HttpResponseMessage responseMessageIndex = await client.GetAsync(url);
            if (responseMessageIndex.IsSuccessStatusCode)
            {
                var responseData = responseMessageIndex.Content.ReadAsStringAsync().Result;
                var LstNotaris = JsonConvert.DeserializeObject<List<trxNotarisTabular>>(responseData);
                return PartialView("_NotarisTabularByRek", LstNotaris);
            }
            return View("Error");
        }

        public async Task<ActionResult> XLS_NotarisDetailAll(string strFilterExpression)
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
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_NotarisDetailAll/{1}", url, strFilterExp));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<vwNotarisTabular>>(responseData);

                XlsExportOptions xlsOption = new XlsExportOptions();

                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_NotarisDetailAll(strFilterExp), myData, "XLS_NotarisDetailAll", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
    }
}
