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

        public async Task<ActionResult> _ManagementByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlManagement + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxManagement>>(responseData);
                return PartialView("_ManagementByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditManagement(int IdData = -1)
        {
            trxManagementForm myDataForm = new trxManagementForm();
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlManagement + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxManagement>(responseData);
                    myDataForm.InjectFrom(myData);

                    myDataForm = PopulatePhoneFax(myDataForm);

                    ImageContainer.Instance.ImageBaseName = myData.ImageBaseName;
                    ImageContainer.Instance.ImageExtensionKTP = myData.FileExtKTP;
                    ImageContainer.Instance.ImageExtensionNPWP = myData.FileExtNPWP;
                    ImageContainer.Instance.ImageExtensionIAPI = myData.FileExtIAPI;
                    return View(myDataForm);
                }
                return View("Error");
            }
            else
            {
                Guid myImageBaseName = Guid.NewGuid();
                trxManagement myData = new trxManagement();
                myData.ImageBaseName = myImageBaseName;
                ImageContainer.Instance.ImageBaseName = myImageBaseName;
                myDataForm.InjectFrom(myData);
                return View(myDataForm);
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

        private trxBranchOfficeForm PopulatePhoneFax(trxBranchOfficeForm myDataForm)
        {
            trxBranchOfficeForm myDataReturn = new trxBranchOfficeForm();
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
                myData.FileExtKTP = ImageContainer.Instance.ImageExtensionKTP;
                myData.FileExtNPWP = ImageContainer.Instance.ImageExtensionNPWP;
                myData.FileExtIAPI = ImageContainer.Instance.ImageExtensionIAPI;
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
                myData.FileExtKTP = ImageContainer.Instance.ImageExtensionKTP;
                myData.FileExtNPWP = ImageContainer.Instance.ImageExtensionNPWP;
                myData.FileExtIAPI = ImageContainer.Instance.ImageExtensionIAPI;

                //myData.FileExtKTP = ".xyz1";
                //myData.FileExtNPWP = ".xyz2";
                //myData.FileExtIAPI = ".xyz3";

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


        public ActionResult ImageUpload(string UploaderCtl = "uploadControl")
        {
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles("uploadControl", UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);

            //Data Organisasi - Management
            switch (UploaderCtl)
	            {
                case "uploadControlKTP":
                        ImageContainer.Instance.ImageScanned = ImageContainer.Instance.ImageBaseName + "_KTP";
                    break;
                case "uploadControlNPWP":
                    ImageContainer.Instance.ImageScanned = ImageContainer.Instance.ImageBaseName + "_NPWP";
                    break;
                case "uploadControlIAPI":
                    ImageContainer.Instance.ImageScanned = ImageContainer.Instance.ImageBaseName + "_IAPI";
                    break;
                default:
                    break;
	            }
            UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles(UploaderCtl, UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);
            TempData["ContentType"] = arrUploaded[0].ContentType;
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

        public async Task<ActionResult> _AddEditCabang(int IdData = -1)
        {
            trxBranchOfficeForm myDataForm = new trxBranchOfficeForm();
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlBranch + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxBranchOffice>(responseData);
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
        public async Task<ActionResult> _AddEditCabang(trxBranchOfficeForm myDataForm)
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

            trxBranchOffice myData = new trxBranchOffice();

            if (myDataForm.IdCabang > 0)
            {
                myData.InjectFrom(myDataForm);
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlBranch + "/" + myData.IdCabang, myData);
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlBranch, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> _DeleteCabang(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlBranch + "/" + IdData);
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
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlOwnership + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxOwnership>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                return View(new trxOwnership());
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditOwnership(trxOwnership myData)
        {
            if (myData.IdOwnership > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlOwnership + "/" + myData.IdOwnership, myData);
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
        public async Task<ActionResult> _TenagaAhliByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(urlTAhliHeader + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhliHeader>>(responseData);
                return PartialView("_TenagaAhliHeaderByRek", myData);
            }
            return View("Error");
        }
        #region CRUD Tenaga Ahli

        public async Task<ActionResult> _AddEditTAhliHeader(int IdData = -1)
        {
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlTAhliHeader + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxTenagaAhliHeader>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                return View(new trxTenagaAhliHeader());
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditTAhli(trxTenagaAhli myData)
        {
            if (myData.IdTenagaAhli > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlTAhliHeader + "/" + myData.IdTenagaAhli, myData);
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlTAhliHeader, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }
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

    public sealed class ImageContainer
    {
        private static ImageContainer single = new ImageContainer();
        public static ImageContainer Instance
        {
            get { return single; }
        }
        public Guid? ImageBaseName { get; set; }
        public string ImageScanned { get; set; }
        public string ImageExtension { get; set; }
        public string ImageExtensionKTP { get; set; }
        public string ImageExtensionNPWP { get; set; }
        public string ImageExtensionIAPI { get; set; }
    }
    public class UploadControlHelper
    {
        //private readonly ITokenContainer tokenContainer;
        public static readonly DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings =
            new DevExpress.Web.ASPxUploadControl.ValidationSettings
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".png", ".pdf" },
                MaxFileSize = 4000000
            };

        public static void uploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            //string strFileNameBase = ImageContainer.Instance.ImageBaseName.ToString();
            string strUpload = ((DevExpress.Web.Mvc.MVCxUploadControl)sender).ClientID;
            string strFileNameBase = ImageContainer.Instance.ImageScanned.ToString();
            string strImageExtension = Path.GetExtension(e.UploadedFile.FileName);
            ImageContainer.Instance.ImageExtension = strImageExtension;

            switch (strUpload)
            {
                case "uploadControlKTP":
                    ImageContainer.Instance.ImageExtensionKTP = strImageExtension;
                    break;
                case "uploadControlNPWP":
                    ImageContainer.Instance.ImageExtensionNPWP = strImageExtension;
                    break;
                case "uploadControlIAPI":
                    ImageContainer.Instance.ImageExtensionIAPI = strImageExtension;
                    break;
                default:
                    ImageContainer.Instance.ImageExtension = strImageExtension;
                    break;
            }

            string resultFilePath = "~/Content/DocumentImages/" + string.Format("{0}{1}", strFileNameBase, strImageExtension);
            //string resultFilePath = "~/Content/DocumentImages/" + string.Format("{0}{1}", strFileNameBase, ".jpg");

            e.UploadedFile.SaveAs(HttpContext.Current.Request.MapPath(resultFilePath));

            IUrlResolutionService urlResolver = sender as IUrlResolutionService;
            if (urlResolver != null)
                e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath) + "?refresh=" + Guid.NewGuid().ToString();
        }
    }

}
