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
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxNotificationHeaderController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxNotificationHeader";
        string urlHeader = string.Empty;
        string urlDetail = string.Empty;
        string urlRekanan = string.Empty;
         
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxNotificationHeaderController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            urlHeader = string.Format("{0}/api/TrxNotificationHeader", SmartAPIUrl);
            urlDetail = string.Format("{0}/api/TrxNotificationDetail", SmartAPIUrl);
            urlRekanan = string.Format("{0}/api/MstRekanan", SmartAPIUrl);
            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(urlHeader);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlHeader);
            if (responseMessage.IsSuccessStatusCode)
            {
               var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;

               var Employees = JsonConvert.DeserializeObject<List<trxNotificationHeader>>(responseData); 
 
                 return View(Employees);
            }
            return View("Error");
        }
 
        public ActionResult Create()
        {
            return View(new trxNotificationHeader());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxNotificationHeader Emp)
        {
           HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(urlHeader,Emp);
           if (responseMessage.IsSuccessStatusCode)
           {
               return RedirectToAction("Index");
           }
           return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlHeader+"/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<trxNotificationHeader>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxNotificationHeader Emp)
        {
 
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlHeader+"/" +id, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlHeader + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<trxNotificationHeader>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxNotificationHeader Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(urlHeader + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> GetNotiInboxSentDraft()
        {
            bool bolIsRekanan = true;
            if ((int)tokenContainer.SupervisorId > 0)
            {
                bolIsRekanan = false;
            }
            ViewBag.IsRekanan = bolIsRekanan;
            return View();
        }
        public ActionResult _TrayInboxCaption()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlHeader + "/GetTInboxCaptionBySubjectTo/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<inboxCaptionForm>(responseData);
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public ActionResult _ReadInfoByIdNotification(int IdNotificationDetail, bool FlgTraySent)
        {
            bool bolAllowReply = false;
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/ReadInfoByIdNotification/{1}/{2}", urlHeader, IdNotificationDetail.ToString(), FlgTraySent.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                ViewData["FlgTraySent"] = FlgTraySent;
                if ((int)tokenContainer.SupervisorId > 0 && FlgTraySent)
                {
                    bolAllowReply = true;
                }
                ViewData["AllowReply"] = bolAllowReply;
                var myData = JsonConvert.DeserializeObject<readInfoByIdNotiForm>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public ActionResult _ReadInfoByIdNotificationHead(int IdNotification)
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/ReadInfoByIdNotificationHead/{1}", urlHeader, IdNotification.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                ViewData["IsHead"] = true;
                ViewBag.IsHead = true;
                ViewData["AllowReply"] = false;
                var myData = JsonConvert.DeserializeObject<readInfoByIdNotiForm>(responseData);
                return View("_ReadInfoByIdNotification", myData);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult _ReadInfoByIdNotification(int IdNotificationDetail, readInfoByIdNotiForm myDataForm)
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlHeader + "/ReadInfoByIdNotification/" + IdNotificationDetail.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<readInfoByIdNotiForm>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public ActionResult _TraySentCaption()
        {
            //tampilkan view untuk PCP
            if ((int)tokenContainer.SupervisorId > 0)
            {
                HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetTSentCaptionBySubjectFrom/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), "1")).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myDataList = JsonConvert.DeserializeObject<sentCaptionForm>(responseData);
                    return PartialView("_TraySentCaption", myDataList);
                }
            }
            return View("Error");
        }
        public ActionResult _EmailSentCaption()
        {
            //tampilkan view untuk PCP
            if ((int)tokenContainer.SupervisorId > 0)
            {
                HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetTSentCaptionBySubjectFrom/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), "2")).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myDataList = JsonConvert.DeserializeObject<sentCaptionForm>(responseData);
                    return PartialView("_EmailSentCaption", myDataList);
                }
            }
            return View("Error");
        }
        public ActionResult _WhatsappSentCaption()
        {
            //tampilkan view untuk PCP
            if ((int)tokenContainer.SupervisorId > 0)
            {
                HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetTSentCaptionBySubjectFrom/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), "3")).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myDataList = JsonConvert.DeserializeObject<sentCaptionForm>(responseData);
                    return PartialView("_WhatsappSentCaption", myDataList);
                }
            }
            return View("Error");
        }
        public ActionResult _AlertByEmailSentCaption()
        {
            //tampilkan view untuk PCP
            if ((int)tokenContainer.SupervisorId > 0)
            {
                HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetTSentCaptionBySubjectFrom/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), "4")).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myDataList = JsonConvert.DeserializeObject<sentCaptionForm>(responseData);
                    return PartialView("_AlertByEmailSentCaption", myDataList);
                }
            }
            return View("Error");
        }
        public ActionResult _AlertByWhatsappSentCaption()
        {
            //tampilkan view untuk PCP
            if ((int)tokenContainer.SupervisorId > 0)
            {
                HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetTSentCaptionBySubjectFrom/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), "5")).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myDataList = JsonConvert.DeserializeObject<sentCaptionForm>(responseData);
                    return PartialView("_AlertByWhatsappSentCaption", myDataList);
                }
            }
            return View("Error");
        }
        public ActionResult _TraySentDetail4Rek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlHeader + "/GetTSentDetailBySubjectFrom/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<sentDetailForm>(responseData);
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public ActionResult _TraySentDetail(int IdNotification, int IdTipeSaluran)
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetSentByIdNotification/{1}/{2}", urlHeader, IdNotification.ToString(), IdTipeSaluran.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<IEnumerable<fGetSentByIdNotification_Result>>(responseData);
                ViewData["IdNotification"] = IdNotification;
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public ActionResult _TrayDraftCaption()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlHeader + "/GetTDraftCaptionBySubjectFrom/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<draftCaptionForm>(responseData);
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public ActionResult _TrayDraftDetail4Rek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlHeader + "/GetTDraftDetailBySubjectFrom/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<draftDetailForm>(responseData);
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public ActionResult _TrayDraftDetail(int IdNotification)
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlHeader + "/GetNotiByIdNotification/" + IdNotification.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<IEnumerable<fGetNotiByIdNotification_Result>>(responseData);
                ViewData["IdNotification"] = IdNotification;
                return PartialView(myDataList);
            }
            return View("Error");
        }
        public async Task<ActionResult> _SendNotiDetail(int IdNotificationDetail)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlDetail + "/SendNotiDetail/" + IdNotificationDetail.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetNotiInboxSentDraft", "TrxNotificationHeader");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _ReadNotiDetail(int IdNotificationDetail, bool FlgTraySent, bool AllowReply)
        {
            //jika flag kirim = 1, berarti posisi di inbox, perlu set flagStatusBaca dan flagTanggalBaca
            if (FlgTraySent)
            {
                if (AllowReply)
                {
                    AddEditTDraftCaptionForm myDataNew = new AddEditTDraftCaptionForm();
                    HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/ReadInfoByIdNotification/{1}/{2}", urlHeader, IdNotificationDetail.ToString(), FlgTraySent.ToString())).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        //ViewData["FlgTraySent"] = FlgTraySent;
                        var myData = JsonConvert.DeserializeObject<readInfoByIdNotiForm>(responseData);
                        //tukar pengirim menjadi penerima dan sebaliknya, reset IdNotification menjadi -1
                        myDataNew.IdNotification = -1;
                        myDataNew.SubjectFrom = myData.ReadInfoByNoti.SubjectTo;
                        myDataNew.SubjectTo = myData.ReadInfoByNoti.SubjectFrom;
                        myDataNew.CaptionInfo = "RE : " + myData.ReadInfoByNoti.CaptionInfo;
                        myDataNew.IdTipeNotification = myData.ReadInfoByNoti.IdTipeNotification;
                        myDataNew.IdLevelUrgensi = myData.ReadInfoByNoti.IdLevelUrgensi;
                        myDataNew.SubjectToStr = myData.ReadInfoByNoti.From_Name;
                        myDataNew.SubjectFromStr = myData.ReadInfoByNoti.To_Name;
                        myDataNew.TipeNotifikasiColls = myData.TipeNotifikasiColls;
                        myDataNew.LevelUrgensiColls = myData.LevelUrgensiColls;
                        myDataNew.BodyContent = Environment.NewLine + Environment.NewLine + Environment.NewLine 
                            + new string('-', 7) + " REPLY " + new string('-', 7) 
                            + Environment.NewLine + myData.ReadInfoByNoti.BodyContent;
                        myDataNew.Attachment = Guid.NewGuid();
                        ImageContainerUpd.ImageBaseName = myDataNew.Attachment;
                        ViewBag.IsRekanan = true; //agar tidak perlu pilih tujuan pengiriman

                        return View("_AddEditTDraftReply", myDataNew);
                    }
                }
                else
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(urlDetail + "/ReadNotiDetail/" + IdNotificationDetail.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetNotiInboxSentDraft", "TrxNotificationHeader");
                    }
                }
            }
            else //jika flag kirim = 0, berarti posisi di draft, tidak perlu set flagBaca
            {
                return RedirectToAction("GetNotiInboxSentDraft", "TrxNotificationHeader");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _DeleteNotiDetail(int IdNotificationDetail)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlDetail + "/" + IdNotificationDetail.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetNotiInboxSentDraft", "TrxNotificationHeader");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> GetNotificationBySubjectKritikal()
        {
            return View();
            //HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetNotificationBySubject/{1}/{2}", url, tokenContainer.RekananId.ToString(), 1));
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //    var myData = JsonConvert.DeserializeObject<List<VmNotiBySubject>>(responseData);

            //    return View(myData);
            //}
            //return View("Error");
        }
        public async Task<ActionResult> _NotificationKritikal()
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetNotificationBySubject/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), 1)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<VmNotiBySubject>>(responseData);

                return PartialView("_NotificationKritikal", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _NotificationPenting()
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetNotificationBySubject/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), 2)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<VmNotiBySubject>>(responseData);

                return PartialView("_NotificationPenting", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _NotificationNormal()
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetNotificationBySubject/{1}/{2}", urlHeader, tokenContainer.IdRekananContact.ToString(), 3)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<VmNotiBySubject>>(responseData);

                return PartialView("_NotificationNormal", myData);
            }
            return View("Error");
        }

        public async Task<ActionResult> _AddEditTDraftContainer(int IdNotification)
        {
            bool bolIsRekanan = true;
            if ((int)tokenContainer.SupervisorId > 0)
            {
                bolIsRekanan = false;
            }
            ViewBag.IsRekanan = bolIsRekanan;
            ViewBag.IdNotification = IdNotification;
            return View();
        }
        public ActionResult _AddEditTDraft(int IdNotification)
        {
            bool bolIsRekanan = true;
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetDraftCaptionForm/{1}/{2}/{3}/{4}", urlHeader, IdNotification.ToString()
                , tokenContainer.SupervisorId.ToString(), tokenContainer.IdRekananContact.ToString(), tokenContainer.UserName.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myDataForm = JsonConvert.DeserializeObject<AddEditTDraftCaptionForm>(responseData);

                if ((int)tokenContainer.SupervisorId > 0)
                {
                    bolIsRekanan = false;
                }
                if(IdNotification < 0)
                {
                    myDataForm.Attachment = Guid.NewGuid();
                    ImageContainerUpd.ImageBaseName = myDataForm.Attachment;
                }
                else
                {
                    ImageContainerUpd.ImageBaseName = myDataForm.Attachment;

                    //ImageContainerUpd.ImageFile1 = myDataForm.FileName1;
                    //ImageContainerUpd.ImageFile2 = myDataForm.FileName2;
                    //ImageContainerUpd.ImageFile3 = myDataForm.FileName3;

                    ImageContainerUpd.ImageExtension1 = myDataForm.FileExt1;
                    ImageContainerUpd.ImageExtension2 = myDataForm.FileExt2;
                    ImageContainerUpd.ImageExtension3 = myDataForm.FileExt3;
                }
                ViewBag.IsRekanan = bolIsRekanan;

                if (bolIsRekanan)
                {
                    return PartialView("_AddEditTDraft", myDataForm);
                }
                else
                {
                    return PartialView("_AddEditTDraftPCP", myDataForm);
                }
                
            }
            return View("Error");
        }

        public ActionResult _GridSubjectTo()
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetBySupervisorId/{1}", urlRekanan, tokenContainer.SupervisorId.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myDataForm = JsonConvert.DeserializeObject<mstRekananMulti>(responseData);

                return PartialView(myDataForm);
            }
            return View("Error");
        }
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> _AddEditTDraftContainer(int IdNotification, AddEditTDraftCaptionForm myDataForm)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlHeader + "/" + IdNotification, myDataForm);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("_AddEditTDraftContainer");
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public async Task<ActionResult> _AddEditTDraft(int IdNotification, AddEditTDraftCaptionForm myDataForm, string customValue, string RecipientList)
        {
            bool IsRekanan = true;
            bool flgKirim = false;
            if(customValue =="K")
            {
                flgKirim = true;
            }
            myDataForm.FlagKirim = flgKirim;

            if ((int)tokenContainer.SupervisorId > 0)
            {
                IsRekanan = false;
            }
            myDataForm.IsRekanan = IsRekanan;
            myDataForm.SubjectToStr = RecipientList;

            //myDataForm.FileName1 = ImageContainerUpd.ImageFile1;
            //myDataForm.FileName2 = ImageContainerUpd.ImageFile2;
            //myDataForm.FileName3 = ImageContainerUpd.ImageFile3;

            //myDataForm.FileExt1 = ImageContainerUpd.ImageExtension1;
            //myDataForm.FileExt2 = ImageContainerUpd.ImageExtension2;
            //myDataForm.FileExt3 = ImageContainerUpd.ImageExtension3;
            
            trxNotificationHeader myDataOri = new trxNotificationHeader();
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlHeader + "/" + IdNotification, myDataForm);
            //HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + IdNotification, myDataOri);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetNotiInboxSentDraft");
            }
            return RedirectToAction("Error");
        }
        public ActionResult ImageUpload(string UploaderCtl = "uploadControl")
        {
            switch (UploaderCtl)
            {
                case "uploadControl1":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_1";
                    break;
                case "uploadControl2":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_2";
                    break;
                case "uploadControl3":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_3";
                    break;
                default:
                    break;
            }
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles(UploaderCtl, UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);
            return null;
        }

    }
}
