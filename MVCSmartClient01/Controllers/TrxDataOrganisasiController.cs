using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using ApiHelper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.Configuration;
using System.IO;
using Omu.ValueInjecter;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxDataOrganisasiController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxDataOrganisasi";
        //string urlManagement = "http://localhost:2070/api/TrxManagement";
        //string urlBranch = "http://localhost:2070/api/TrxBranchOffice";
        //string urlOwnership = "http://localhost:2070/api/TrxOwnership";
        //string urlAsisten = "http://localhost:2070/api/TrxAsisten";
        //string urlTAhli = "http://localhost:2070/api/TrxTenagaAhli";
        //string urlContact = "http://localhost:2070/api/MstContact";
        //string urlTPendukung = "http://localhost:2070/api/TrxTenagaPendukung";

        string url = string.Empty;
        string urlManagement = string.Empty;
        string urlBranch = string.Empty;
        string urlOwnership = string.Empty;
        string urlAsisten = string.Empty;
        string urlTAhliHeader = string.Empty;
        string urlContact = string.Empty;
        string urlTPendukung = string.Empty;
        string urlNotarisItem = string.Empty; 

        public TrxDataOrganisasiController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxDataOrganisasi", SmartAPIUrl);
            urlManagement = string.Format("{0}/api/TrxManagement", SmartAPIUrl);
            urlBranch = string.Format("{0}/api/TrxBranchOffice", SmartAPIUrl);
            urlOwnership = string.Format("{0}/api/TrxOwnership", SmartAPIUrl);
            urlAsisten = string.Format("{0}/api/TrxAsisten", SmartAPIUrl);
            urlTAhliHeader = string.Format("{0}/api/TrxTenagaAhliHeader", SmartAPIUrl);
            urlContact = string.Format("{0}/api/MstContact", SmartAPIUrl);
            urlTPendukung = string.Format("{0}/api/TrxTenagaPendukung", SmartAPIUrl);
            urlNotarisItem = string.Format("{0}/api/TrxNotarisItem", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            //HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetDataOrganisasiByRek/" + tokenContainer.IdRekananContact.ToString());
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetDataOrganisasiByRek_Type/{1}/{2}"
                , url, tokenContainer.IdRekananContact.ToString(), tokenContainer.IdTypeOfRekanan.ToString()));

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fDataOrganisasiByRek_Type_Result>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan()
        {
            //HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetDataOrganisasiByRek/" + tokenContainer.IdRekananContact.ToString());
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetDataOrganisasiByRek_Type/{1}/{2}"
                , url, tokenContainer.IdRekananContact.ToString(), tokenContainer.IdTypeOfRekanan.ToString()));

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fDataOrganisasiByRek_Type_Result>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public ActionResult GetByRekananTab()
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format("{0}/GetDataOrganisasiByRek_Type/{1}/{2}"
                , url, tokenContainer.IdRekananContact.ToString(), tokenContainer.IdTypeOfRekanan.ToString())).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fDataOrganisasiByRek_Type_Result>>(responseData);
                return PartialView("GetByRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekanan()
        {
            //HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetDataOrganisasiByRek/" + tokenContainer.IdRekananContact.ToString());
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetDataOrganisasiByRek_Type/{1}/{2}"
                , url, tokenContainer.IdRekananContact.ToString(), tokenContainer.IdTypeOfRekanan.ToString()));

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fDataOrganisasiByRek_Type_Result>>(responseData);
                return PartialView("_GetByRekanan", myData);
            }
            return View("Error");
        }
        #region CRUD Management

        public async Task<ActionResult> _ManagementByRek(string JudulForm)
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlManagement + "/GetByRekananActive/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                ViewBag.JudulForm = JudulForm;
                return PartialView("_ManagementByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditManagement(int IdData = -1, string JudulForm = "")
        {
            trxManagementForm myDataForm = new trxManagementForm();
            ViewBag.JudulForm = JudulForm;
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlManagement + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxManagement>(responseData);
                    myDataForm.InjectFrom(myData);
                    myDataForm.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;

                    myDataForm = PopulatePhoneFax(myDataForm);

                    ImageContainerUpd.ImageBaseName = myData.ImageBaseName;

                    ImageContainerUpd.ImageExtensionKTP = myData.FileExtKTP;
                    ImageContainerUpd.ImageExtensionNPWP = myData.FileExtNPWP;
                    ImageContainerUpd.ImageExtensionIAPI = myData.FileExtIAPI;
                    ImageContainerUpd.ImageExtensionCV = myData.FileExtCV;
                    ImageContainerUpd.ImageExtensionIzin = myData.FileExtIzin;
                    ImageContainerUpd.ImageExtensionGelar = myData.FileExtGelar;

                    ViewBag.ImageBaseName = "NamaFileYangAda.jpg";

                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 1 :
                            return View("_AddEditManagementKJP", myDataForm);
                        case 2 :
                            return View("_AddEditManagementKAP", myDataForm);
                        case 4:
                        case 5:
                            return View("_AddEditManagementAS", myDataForm);
                        default:
                            return View("_AddEditManagementMin", myDataForm);
                    }
                }
                return View("Error");
            }
            else
            {
                Guid myImageBaseName = Guid.NewGuid();
                trxManagement myData = new trxManagement();
                myData.ImageBaseName = myImageBaseName;
                ImageContainerUpd.ImageBaseName = myImageBaseName;
                myDataForm.InjectFrom(myData);
                myDataForm.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                myDataForm.IsTTDLaporan = false;

                switch ((int)tokenContainer.IdTypeOfRekanan)
                {
                    case 1:
                        return View("_AddEditManagementKJP", myDataForm);
                    case 2:
                        return View("_AddEditManagementKAP", myDataForm);
                    case 4:
                    case 5:
                        return View("_AddEditManagementAS", myDataForm);
                    default:
                        return View("_AddEditManagementMin", myDataForm);
                }
            }
        }
        public ActionResult _DaftarFile()
        {
            //ViewBag.ImageBaseName = ImageContainerUpd.ImageFileKTP;
            return PartialView("_DaftarFile");
        }
        public JsonResult DeleteFile(string id)
        {
            //ImageContainerUpd.ImageFileKTP = "HanyaITUUU";
            try
            {
                var path = Path.Combine(Server.MapPath("~/Content/DocumentImages/"), "1234" + ".jpg");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private trxManagementForm PopulatePhoneFax(trxManagementForm myDataForm)
        {
            trxManagementForm myDataReturn = new trxManagementForm();
            myDataReturn.InjectFrom(myDataForm);
            if (!string.IsNullOrEmpty(myDataReturn.Telephone1) && myDataReturn.Telephone1.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                    myDataReturn.Phone1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone2) && myDataReturn.Telephone2.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                    myDataReturn.Phone2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone3) && myDataReturn.Telephone3.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone3.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                    myDataReturn.Phone3_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Fax1) && myDataReturn.Fax1.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                    myDataReturn.Fax1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                }
            }
            if (!string.IsNullOrEmpty(myDataReturn.Fax2) && myDataReturn.Fax2.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                    myDataReturn.Fax2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                }
            }

            return myDataReturn;
        }

        private trxOwnershipForm PopulatePhoneFax(trxOwnershipForm myDataForm)
        {
            trxOwnershipForm myDataReturn = new trxOwnershipForm();
            myDataReturn.InjectFrom(myDataForm);
            if (!string.IsNullOrEmpty(myDataReturn.Telephone1) && myDataReturn.Telephone1.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                    myDataReturn.Phone1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone2) && myDataReturn.Telephone2.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                    myDataReturn.Phone2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone3) && myDataReturn.Telephone3.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone3.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                    myDataReturn.Phone3_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Fax1) && myDataReturn.Fax1.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                    myDataReturn.Fax1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                }
            }
            if (!string.IsNullOrEmpty(myDataReturn.Fax2) && myDataReturn.Fax2.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                    myDataReturn.Fax2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                }
            }

            return myDataReturn;
        }

        private mstContactForm PopulatePhoneFax(mstContactForm myDataForm)
        {
            mstContactForm myDataReturn = new mstContactForm();
            myDataReturn.InjectFrom(myDataForm);
            if (!string.IsNullOrEmpty(myDataReturn.Telephone1) && myDataReturn.Telephone1.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                    myDataReturn.Phone1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone2) && myDataReturn.Telephone2.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                    myDataReturn.Phone2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone3) && myDataReturn.Telephone3.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone3.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                    myDataReturn.Phone3_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Fax1) && myDataReturn.Fax1.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                    myDataReturn.Fax1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                }
            }
            if (!string.IsNullOrEmpty(myDataReturn.Fax2) && myDataReturn.Fax2.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                    myDataReturn.Fax2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                }
            }

            return myDataReturn;
        }

        private trxAsistenForm PopulatePhoneFax(trxAsistenForm myDataForm)
        {
            trxAsistenForm myDataReturn = new trxAsistenForm();
            myDataReturn.InjectFrom(myDataForm);
            if (!string.IsNullOrEmpty(myDataReturn.Telephone1) && myDataReturn.Telephone1.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                    myDataReturn.Phone1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone2) && myDataReturn.Telephone2.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                    myDataReturn.Phone2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone3) && myDataReturn.Telephone3.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone3.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                    myDataReturn.Phone3_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Fax1) && myDataReturn.Fax1.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                    myDataReturn.Fax1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                }
            }
            if (!string.IsNullOrEmpty(myDataReturn.Fax2) && myDataReturn.Fax2.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                    myDataReturn.Fax2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                }
            }

            return myDataReturn;
        }

        private trxNotarisItemForm PopulatePhoneFax(trxNotarisItemForm myDataForm)
        {
            trxNotarisItemForm myDataReturn = new trxNotarisItemForm();
            myDataReturn.InjectFrom(myDataForm);
            if (!string.IsNullOrEmpty(myDataReturn.Telephone1) && myDataReturn.Telephone1.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                    myDataReturn.Phone1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone1_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone2) && myDataReturn.Telephone2.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                    myDataReturn.Phone2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone2_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Telephone3) && myDataReturn.Telephone3.Contains("-"))
            {
                var arrReturn = myDataReturn.Telephone3.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                    myDataReturn.Phone3_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Phone3_1 = arrReturn[0];
                }
            }

            if (!string.IsNullOrEmpty(myDataReturn.Fax1) && myDataReturn.Fax1.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax1.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                    myDataReturn.Fax1_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax1_1 = arrReturn[0];
                }
            }
            if (!string.IsNullOrEmpty(myDataReturn.Fax2) && myDataReturn.Fax2.Contains("-"))
            {
                var arrReturn = myDataReturn.Fax2.Split('-');
                if (arrReturn.Length == 2)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                    myDataReturn.Fax2_2 = arrReturn[1];
                }
                else if (arrReturn.Length == 1)
                {
                    myDataReturn.Fax2_1 = arrReturn[0];
                }
            }

            return myDataReturn;
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditManagement(trxManagementForm myDataForm)
        {

            #region Agregasi Field Phone Fax

            string strPhone1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone1_1))
            {
                strPhone1 = myDataForm.Phone1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone1_2))
            {
                strPhone1 += "-" + myDataForm.Phone1_2;
            }
            myDataForm.Telephone1 = strPhone1;

            string strPhone2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
            {
                strPhone2 = myDataForm.Phone2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
            {
                strPhone2 += "-" + myDataForm.Phone2_2;
            }
            myDataForm.Telephone2 = strPhone2;

            string strPhone3 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            {
                strPhone3 = myDataForm.Phone3_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            {
                strPhone3 += "-" + myDataForm.Phone3_2;
            }
            myDataForm.Telephone3 = strPhone3;

            string strFax1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax1_1))
            {
                strFax1 = myDataForm.Fax1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax1_2))
            {
                strFax1 += "-" + myDataForm.Fax1_2;
            }
            myDataForm.Fax1 = strFax1;

            string strFax2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax2_1))
            {
                strFax2 = myDataForm.Fax2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax2_2))
            {
                strFax2 += "-" + myDataForm.Fax2_2;
            }
            myDataForm.Fax2 = strFax2;

            #endregion

            trxManagement myData = new trxManagement();
            if (myDataForm.IdManagemen > 0)
            {
                myData.InjectFrom(myDataForm);
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlManagement + "/" + myData.IdManagemen, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.InjectFrom(myDataForm);
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.IsActive = true;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlManagement, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> _DeleteManagement(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlManagement + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _DeleteManagementByRek(int IdData)
        {
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/DeleteManagementByRek/{1}", urlManagement, IdData.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }


        public ActionResult ImageUpload(string UploaderCtl = "uploadControl")
        {
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles("uploadControl", UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);

            ImageContainerUpd.ImageScanned = "ASUUUUUU";
            //Data Organisasi - Management
            switch (UploaderCtl)
	            {
                case "uploadControlKTP":
                        ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_KTP";
                    break;
                case "uploadControlNPWP":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_NPWP";
                    break;
                case "uploadControlIAPI":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_IAPI";
                    break;
                case "uploadControlSOLV":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_SOLV";
                    break;
                default:
                    break;
	            }
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles(UploaderCtl, UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);
            //TempData["ContentType"] = arrUploaded[0].ContentType;
            return null;
        }

        #endregion
        public async Task<ActionResult> _BranchByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlBranch + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxBranchOffice>>(responseData);
                return PartialView("_BranchByRek", myData);
            }
            return View("Error");
        }

        #region CRUD Cabang

        //public async Task<ActionResult> _AddEditCabang(int IdData = -1)
        //{
        //    trxBranchOfficeForm myDataForm = new trxBranchOfficeForm();
        //    if (IdData > 0)
        //    {
        //        HttpResponseMessage responseMessage = await client.GetAsync(urlBranch + "/" + IdData);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //            var myData = JsonConvert.DeserializeObject<trxBranchOffice>(responseData);
        //            myDataForm.InjectFrom(myData);

        //            myDataForm = PopulatePhoneFax(myDataForm);

        //            return View(myDataForm);
        //        }
        //        return View("Error");
        //    }
        //    else
        //    {
        //        return View(myDataForm);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> _AddEditCabang(trxBranchOfficeForm myDataForm)
        //{
        //    #region Agregasi Field Phone Fax

        //    string strPhone1 = string.Empty;
        //    if (!string.IsNullOrEmpty(myDataForm.Phone1_1))
        //    {
        //        strPhone1 = myDataForm.Phone1_1;
        //    }
        //    if (!string.IsNullOrEmpty(myDataForm.Phone1_2))
        //    {
        //        strPhone1 += "-" + myDataForm.Phone1_2;
        //    }
        //    myDataForm.Telephone1 = strPhone1;

        //    string strPhone2 = string.Empty;
        //    if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
        //    {
        //        strPhone2 = myDataForm.Phone2_1;
        //    }
        //    if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
        //    {
        //        strPhone2 += "-" + myDataForm.Phone2_2;
        //    }
        //    myDataForm.Telephone2 = strPhone2;

        //    string strPhone3 = string.Empty;
        //    if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
        //    {
        //        strPhone3 = myDataForm.Phone3_1;
        //    }
        //    if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
        //    {
        //        strPhone3 += "-" + myDataForm.Phone3_2;
        //    }
        //    myDataForm.Telephone3 = strPhone3;

        //    string strFax1 = string.Empty;
        //    if (!string.IsNullOrEmpty(myDataForm.Fax1_1))
        //    {
        //        strFax1 = myDataForm.Fax1_1;
        //    }
        //    if (!string.IsNullOrEmpty(myDataForm.Fax1_2))
        //    {
        //        strFax1 += "-" + myDataForm.Fax1_2;
        //    }
        //    myDataForm.Fax1 = strFax1;

        //    string strFax2 = string.Empty;
        //    if (!string.IsNullOrEmpty(myDataForm.Fax2_1))
        //    {
        //        strFax2 = myDataForm.Fax2_1;
        //    }
        //    if (!string.IsNullOrEmpty(myDataForm.Fax2_2))
        //    {
        //        strFax2 += "-" + myDataForm.Fax2_2;
        //    }
        //    myDataForm.Fax2 = strFax2;

        //    #endregion

        //    trxBranchOffice myData = new trxBranchOffice();

        //    if (myDataForm.IdCabang > 0)
        //    {
        //        myData.InjectFrom(myDataForm);
        //        HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlBranch + "/" + myData.IdCabang, myData);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetByRekanan");
        //        }
        //        return RedirectToAction("Error");
        //    }
        //    else
        //    {
        //        myData.InjectFrom(myDataForm);
        //        myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
        //        myData.CreatedDate = DateTime.Today;
        //        myData.CreatedUser = tokenContainer.UserId.ToString();
        //        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlBranch, myData);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetByRekanan");
        //        }
        //        return RedirectToAction("Error");
        //    }
        //}

        //public async Task<ActionResult> _DeleteCabang(int IdData)
        //{
        //    HttpResponseMessage responseMessage = await client.DeleteAsync(urlBranch + "/" + IdData);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("GetByRekanan");
        //    }
        //    return RedirectToAction("Error");
        //}

        #endregion

        public async Task<ActionResult> _NotarisItemByRek(string JudulForm)
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlNotarisItem + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxNotarisItem>>(responseData);
                ViewBag.JudulForm = JudulForm;
                return PartialView("_NotarisItemByRek", myData);
            }
            return View("Error");
        }

        #region CRUD NotarisItem

        public async Task<ActionResult> _AddEditNotarisItem(int IdData = -1, string JudulForm = "")
        {
            trxNotarisItemForm myDataForm = new trxNotarisItemForm();
            ViewBag.JudulForm = JudulForm;
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlNotarisItem + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxNotarisItem>(responseData);
                    myDataForm.InjectFrom(myData);
                    myDataForm.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;

                    myDataForm = PopulatePhoneFax(myDataForm);

                    ImageContainerUpd.ImageBaseName = myData.ImageBaseName;

                    ImageContainerUpd.ImageExtensionKTP = myData.FileExtKTP;
                    ImageContainerUpd.ImageExtensionNPWP = myData.FileExtNPWP;


                    ViewBag.ImageBaseName = "NamaFileYangAda.jpg";

                    return View("_AddEditNotarisItem", myDataForm);
                }
                return View("Error");
            }
            else
            {
                Guid myImageBaseName = Guid.NewGuid();
                trxNotarisItem myData = new trxNotarisItem();
                myData.ImageBaseName = myImageBaseName;
                ImageContainerUpd.ImageBaseName = myImageBaseName;
                myDataForm.InjectFrom(myData);
                myDataForm.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;

                return View("_AddEditNotarisItem", myDataForm);
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditNotarisItem(trxNotarisItemForm myDataForm)
        {

            #region Agregasi Field Phone Fax

            string strPhone1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone1_1))
            {
                strPhone1 = myDataForm.Phone1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone1_2))
            {
                strPhone1 += "-" + myDataForm.Phone1_2;
            }
            myDataForm.Telephone1 = strPhone1;

            string strPhone2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
            {
                strPhone2 = myDataForm.Phone2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
            {
                strPhone2 += "-" + myDataForm.Phone2_2;
            }
            myDataForm.Telephone2 = strPhone2;

            string strPhone3 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            {
                strPhone3 = myDataForm.Phone3_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            {
                strPhone3 += "-" + myDataForm.Phone3_2;
            }
            myDataForm.Telephone3 = strPhone3;

            string strFax1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax1_1))
            {
                strFax1 = myDataForm.Fax1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax1_2))
            {
                strFax1 += "-" + myDataForm.Fax1_2;
            }
            myDataForm.Fax1 = strFax1;

            string strFax2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax2_1))
            {
                strFax2 = myDataForm.Fax2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax2_2))
            {
                strFax2 += "-" + myDataForm.Fax2_2;
            }
            myDataForm.Fax2 = strFax2;

            #endregion

            trxNotarisItem myData = new trxNotarisItem();
            if (myDataForm.IdNotarisItem > 0)
            {
                myData.InjectFrom(myDataForm);

                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlNotarisItem + "/" + myData.IdNotarisItem, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.InjectFrom(myDataForm);
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;

                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlNotarisItem, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> _DeleteNotarisItem(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlNotarisItem + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        #endregion

        public async Task<ActionResult> _OwnershipByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlOwnership + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxOwnership>>(responseData);
                return PartialView("_OwnershipByRek", myData);
            }
            return View("Error");
        }
        #region CRUD Ownership

        public async Task<ActionResult> _AddEditOwnership(int IdData = -1)
        {
            trxOwnershipForm myDataForm = new trxOwnershipForm();
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlOwnership + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxOwnership>(responseData);
                    myDataForm.InjectFrom(myData);

                    myDataForm = PopulatePhoneFax(myDataForm);

                    return View(myDataForm);
                }
                return View("Error");
            }
            else
            {
                return View(new trxOwnershipForm());
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditOwnership(trxOwnershipForm myDataForm)
        {
            #region Agregasi Field Phone Fax

            string strPhone1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone1_1))
            {
                strPhone1 = myDataForm.Phone1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone1_2))
            {
                strPhone1 += "-" + myDataForm.Phone1_2;
            }
            myDataForm.Telephone1 = strPhone1;

            string strPhone2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
            {
                strPhone2 = myDataForm.Phone2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
            {
                strPhone2 += "-" + myDataForm.Phone2_2;
            }
            myDataForm.Telephone2 = strPhone2;

            string strPhone3 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            {
                strPhone3 = myDataForm.Phone3_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            {
                strPhone3 += "-" + myDataForm.Phone3_2;
            }
            myDataForm.Telephone3 = strPhone3;

            string strFax1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax1_1))
            {
                strFax1 = myDataForm.Fax1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax1_2))
            {
                strFax1 += "-" + myDataForm.Fax1_2;
            }
            myDataForm.Fax1 = strFax1;

            string strFax2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax2_1))
            {
                strFax2 = myDataForm.Fax2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax2_2))
            {
                strFax2 += "-" + myDataForm.Fax2_2;
            }
            myDataForm.Fax2 = strFax2;

            #endregion

            trxOwnership myData = new trxOwnership();
            if (myDataForm.IdOwnership > 0)
            {
                myData.InjectFrom(myDataForm);
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlOwnership + "/" + myData.IdOwnership, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.InjectFrom(myDataForm);
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlOwnership, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> _DeleteOwnership(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlOwnership + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }


        #endregion

        
        public async Task<ActionResult> _AsistenByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlAsisten + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxAsisten>>(responseData);
                return PartialView("_AsistenByRek", myData);
            }
            return View("Error");
        }
        #region CRUD Asisten

        public async Task<ActionResult> _AddEditAsisten(int IdData = -1)
        {
            trxAsistenForm myDataForm = new trxAsistenForm();
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlAsisten + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxAsisten>(responseData);
                    myDataForm.InjectFrom(myData);

                    myDataForm = PopulatePhoneFax(myDataForm);

                    return View(myDataForm);
                }
                return View("Error");
            }
            else
            {
                return View(myDataForm);
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditAsisten(trxAsistenForm myDataForm)
        {
            #region Agregasi Field Phone Fax

            string strPhone1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone1_1))
            {
                strPhone1 = myDataForm.Phone1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone1_2))
            {
                strPhone1 += "-" + myDataForm.Phone1_2;
            }
            myDataForm.Telephone1 = strPhone1;

            string strPhone2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
            {
                strPhone2 = myDataForm.Phone2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
            {
                strPhone2 += "-" + myDataForm.Phone2_2;
            }
            myDataForm.Telephone2 = strPhone2;

            string strPhone3 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            {
                strPhone3 = myDataForm.Phone3_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            {
                strPhone3 += "-" + myDataForm.Phone3_2;
            }
            myDataForm.Telephone3 = strPhone3;

            string strFax1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax1_1))
            {
                strFax1 = myDataForm.Fax1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax1_2))
            {
                strFax1 += "-" + myDataForm.Fax1_2;
            }
            myDataForm.Fax1 = strFax1;

            string strFax2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax2_1))
            {
                strFax2 = myDataForm.Fax2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax2_2))
            {
                strFax2 += "-" + myDataForm.Fax2_2;
            }
            myDataForm.Fax2 = strFax2;

            #endregion

            trxAsisten myData = new trxAsisten();
            if (myDataForm.IdAsisten > 0)
            {
                myData.InjectFrom(myDataForm);
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlAsisten + "/" + myData.IdAsisten, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.InjectFrom(myDataForm);
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                myData.IsActive = true;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlAsisten, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }
        public async Task<ActionResult> _DeleteAsisten(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlAsisten + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }


        #endregion

        //public async Task<ActionResult> _TenagaAhliByRek()
        //{
        //    HttpResponseMessage responseMessage = client.GetAsync(urlTAhliHeader + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
        //        return PartialView("_TenagaAhliHeaderByRek", myData);
        //    }
        //    return View("Error");
        //}
        #region CRUD Tenaga Ahli

        public async Task<ActionResult> _AddEditTAhliHeader(int IdData = -1)
        {
            //clear setting of XLS guid pointer sebagai penanda pemindah data dari temp ke fix table saat klik OK.
            //tokenContainer.XLSPointer = null;
            if (IdData > 0)
            {
                //HttpResponseMessage responseMessage = await client.GetAsync(urlTAhliHeader + "/" + IdData);
                HttpResponseMessage responseMessage = await client.GetAsync(urlTAhliHeader + "/GetByGuidHeader/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxTenagaAhliHeaderForm>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                return View(new trxTenagaAhliHeader());
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> _AddEditTAhliHeader(trxTenagaAhliHeader myData)
        //{
        //    //pindahkan data temporary import XLS ke fix table berdasarkan XLS pointer guid
        //    string strReqToExecTransData = string.Format("{0}/ExecTransDataTA/{1}/{2}/{3}"
        //        , url, myData.IdTenagaAhliHeader.ToString(), tokenContainer.IdRekananContact.ToString(), tokenContainer.XLSPointer.ToString());
        //    HttpResponseMessage responseMessageXLS = await client.GetAsync(strReqToExecTransData);

        //    if (!responseMessageXLS.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Error");
        //    }

        //    if (myData.IdTenagaAhliHeader > 0)
        //    {
        //        HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlTAhliHeader + "/" + myData.IdTenagaAhliHeader, myData);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetByRekanan");
        //        }
        //        return RedirectToAction("Error");
        //    }
        //    else
        //    {
        //        myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
        //        myData.CreatedDate = DateTime.Today;
        //        myData.CreatedUser = tokenContainer.UserId.ToString();
        //        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlTAhliHeader, myData);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetByRekanan");
        //        }
        //        return RedirectToAction("Error");
        //    }
        //}

        public async Task<ActionResult> _DeleteTAhli(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlTAhliHeader + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        #endregion
        public async Task<ActionResult> _ContactByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlContact + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstContact>>(responseData);
                return PartialView("_ContactByRek", myData);
            }
            return View("Error");
        }
        #region CRUD CONTACT

        public async Task<ActionResult> _AddEditContact(int IdData = -1)
        {
            mstContactForm myDataForm = new mstContactForm();
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlContact + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstContact>(responseData);
                    myDataForm.InjectFrom(myData);

                    myDataForm = PopulatePhoneFax(myDataForm);

                    return View(myDataForm);
                }
                return View("Error");
            }
            else
            {
                return View(myDataForm);
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditContact(mstContactForm myDataForm)
        {
            #region Agregasi Field Phone Fax

            string strPhone1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone1_1))
            {
                strPhone1 = myDataForm.Phone1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone1_2))
            {
                strPhone1 += "-" + myDataForm.Phone1_2;
            }
            myDataForm.Telephone1 = strPhone1;

            string strPhone2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
            {
                strPhone2 = myDataForm.Phone2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
            {
                strPhone2 += "-" + myDataForm.Phone2_2;
            }
            myDataForm.Telephone2 = strPhone2;

            string strPhone3 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            {
                strPhone3 = myDataForm.Phone3_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            {
                strPhone3 += "-" + myDataForm.Phone3_2;
            }
            myDataForm.Telephone3 = strPhone3;

            string strFax1 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax1_1))
            {
                strFax1 = myDataForm.Fax1_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax1_2))
            {
                strFax1 += "-" + myDataForm.Fax1_2;
            }
            myDataForm.Fax1 = strFax1;

            string strFax2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Fax2_1))
            {
                strFax2 = myDataForm.Fax2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Fax2_2))
            {
                strFax2 += "-" + myDataForm.Fax2_2;
            }
            myDataForm.Fax2 = strFax2;

            #endregion

            mstContact myData = new mstContact();
            if (myDataForm.IdContact > 0)
            {
                myData.InjectFrom(myDataForm);
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlContact + "/" + myData.IdContact, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.InjectFrom(myDataForm);
                myData.UserId = tokenContainer.UserId.ToString();
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlContact, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }
        public async Task<ActionResult> _DeleteContact(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlContact + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        #endregion
        public async Task<ActionResult> _PendukungByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlTPendukung + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaPendukung>>(responseData);
                return PartialView("_PendukungByRek", myData);
            }
            return View("Error");
        }

        #region CRUD PENDUKUNG

        public async Task<ActionResult> _AddEditPendukung(int IdData = -1)
        {
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlTPendukung + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxTenagaPendukung>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                return View(new trxTenagaPendukung());
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditPendukung(trxTenagaPendukung myData)
        {
            if (myData.IdTenagaPendukung > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlTPendukung + "/" + myData.IdTenagaPendukung, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlTPendukung, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }
        public async Task<ActionResult> _DeletePendukung(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlTPendukung + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        #endregion
        public async Task<ActionResult> _DataManagement()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetDataOrganisasiByRek/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fDataOrganisasiByRek_Result>>(responseData);
                return PartialView("_DataOrganisasiByRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> CreateEdit()
        {
            //string GuidNull = System.Guid.Empty.ToString();
            if ((System.Int32)tokenContainer.IdOrganisasi > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + tokenContainer.IdOrganisasi.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.IsEditable = "Editable";
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxDataOrganisasiForm>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/0");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxDataOrganisasiForm>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateEdit(trxDataOrganisasiForm Emp)
        {
            Emp.CreatedDate = DateTime.Today;
            Emp.CreatedUser = tokenContainer.UserId.ToString();
            //SUDAH ADA SEBELUMNYA
            if ((System.Int32)tokenContainer.IdOrganisasi > 0)
            {
                HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + tokenContainer.IdOrganisasi.ToString(), Emp);
                if (responseMessage1.IsSuccessStatusCode)
                {
                    ViewBag.IsEditable = "IsReadOnly";
                    return View(Emp);
                    //HttpResponseMessage responseMessage2 = await client.GetAsync(url + "/" + tokenContainer.IdOrganisasi.ToString());
                    //if (responseMessage2.IsSuccessStatusCode)
                    //{
                    //    ViewBag.IsEditable = "IsReadOnly";
                    //    var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                    //    var myData = JsonConvert.DeserializeObject<trxDataOrganisasiForm>(responseData);
                    //    return View(myData);
                    //}
                }
                return RedirectToAction("Error");
            }
            else //DATA BARU
            {
                Emp.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, Emp);
                if (responseMessage.IsSuccessStatusCode)
                {
                    HttpResponseMessage responseMessage2 = await client.GetAsync(url + "/GetByRekananId/" + Emp.IdRekanan.ToString());
                    if (responseMessage2.IsSuccessStatusCode)
                    {
                        ViewBag.IsEditable = "IsReadOnly";
                        var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<trxDataOrganisasiForm>(responseData);
                        //untuk digunakan disesi selanjutnya
                        tokenContainer.IdOrganisasi = myData.IdOrganisasi;
                        return View(myData);
                    }
                }
                return RedirectToAction("Error");
            }
        }
        public ActionResult Create()
        {
            return View(new trxDataOrganisasi());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxDataOrganisasi Emp)
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

                var myData = JsonConvert.DeserializeObject<trxDataOrganisasiForm>(responseData);
 
                return View(myData);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxDataOrganisasiForm Emp)
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

                var myData = JsonConvert.DeserializeObject<trxDataOrganisasiForm>(responseData);

                return View(myData);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxDataOrganisasiForm Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }
    }
}
