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
    public class MstRekananController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/MstRekanan";
        string url = string.Empty;
 
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public MstRekananController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstRekanan", SmartAPIUrl);

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

               var myData = JsonConvert.DeserializeObject<mstRekananMulti>(responseData);

               return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> CreateEdit_Read(string flgEdit = "IsReadOnly")
        {
            string GuidNull = System.Guid.Empty.ToString();
            if ((System.Guid)tokenContainer.IdRekananContact != System.Guid.Empty)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    //ViewBag.IsEditable = "Editable";
                    //ViewBag.IsEditable = "IsReadOnly";
                    ViewBag.IsEditable = flgEdit;
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;

                    #region Populate Phone Fax Composit

                    if (!string.IsNullOrEmpty(myData.Phone1) && myData.Phone1.Contains("-"))
                    {
                        var arrReturn = myData.Phone1.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Phone1_1 = arrReturn[0];
                            myData.Phone1_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Phone1_1 = arrReturn[0];
                        }
                    }

                    if (!string.IsNullOrEmpty(myData.Phone2) && myData.Phone2.Contains("-"))
                    {
                        var arrReturn = myData.Phone2.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Phone2_1 = arrReturn[0];
                            myData.Phone2_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Phone2_1 = arrReturn[0];
                        }
                    }

                    if (!string.IsNullOrEmpty(myData.Phone3) && myData.Phone3.Contains("-"))
                    {
                        var arrReturn = myData.Phone3.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Phone3_1 = arrReturn[0];
                            myData.Phone3_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Phone3_1 = arrReturn[0];
                        }
                    }

                    if (!string.IsNullOrEmpty(myData.Fax1) && myData.Fax1.Contains("-"))
                    {
                        var arrReturn = myData.Fax1.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Fax1_1 = arrReturn[0];
                            myData.Fax1_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Fax1_1 = arrReturn[0];
                        }
                    }
                    if (!string.IsNullOrEmpty(myData.Fax2) && myData.Fax2.Contains("-"))
                    {
                        var arrReturn = myData.Fax2.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Fax2_1 = arrReturn[0];
                            myData.Fax2_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Fax2_1 = arrReturn[0];
                        }
                    }

                    #endregion


                    //PERCOBAAAN
                    //myData.Fax2_1 = ImageContainer.Instance.ImageScanned;
                    //myData.Fax2_2 = ImageContainerUpd.ImageScanned;
                    
                    //if (HttpContext.Session["Fax2_2"] != null)
                    //{
                    //    //myData.Fax2_2 = HttpContext.Session["Fax2_2"].ToString();
                    //    myData.Fax2_2 = System.Web.HttpContext.Current.Session["Fax2_2"].ToString();
                    //}

                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return View("CreateEdit_ReadAJ", myData);
                        case 6:
                            return View("CreateEdit_ReadBL", myData);
                        default:
                            return View("CreateEdit_Read", myData);
                    }
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + GuidNull);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;
                    //return View(myData);
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return View("CreateEdit_ReadAJ", myData);
                        case 6:
                            return View("CreateEdit_ReadBL", myData);
                        default:
                            return View("CreateEdit_Read", myData);
                    }
                }
                return View("Error");
            }
        }

        public ActionResult CreateEdit_ReadTab(string flgEdit = "IsReadOnly")
        {
            string GuidNull = System.Guid.Empty.ToString();
            if ((System.Guid)tokenContainer.IdRekananContact != System.Guid.Empty)
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/" + tokenContainer.IdRekananContact.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //ViewBag.IsEditable = "Editable";
                    //ViewBag.IsEditable = "IsReadOnly";
                    ViewBag.IsEditable = flgEdit;
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;

                    #region Populate Phone Fax Composit

                    if (!string.IsNullOrEmpty(myData.Phone1) && myData.Phone1.Contains("-"))
                    {
                        var arrReturn = myData.Phone1.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Phone1_1 = arrReturn[0];
                            myData.Phone1_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Phone1_1 = arrReturn[0];
                        }
                    }

                    if (!string.IsNullOrEmpty(myData.Phone2) && myData.Phone2.Contains("-"))
                    {
                        var arrReturn = myData.Phone2.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Phone2_1 = arrReturn[0];
                            myData.Phone2_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Phone2_1 = arrReturn[0];
                        }
                    }

                    if (!string.IsNullOrEmpty(myData.Phone3) && myData.Phone3.Contains("-"))
                    {
                        var arrReturn = myData.Phone3.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Phone3_1 = arrReturn[0];
                            myData.Phone3_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Phone3_1 = arrReturn[0];
                        }
                    }

                    if (!string.IsNullOrEmpty(myData.Fax1) && myData.Fax1.Contains("-"))
                    {
                        var arrReturn = myData.Fax1.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Fax1_1 = arrReturn[0];
                            myData.Fax1_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Fax1_1 = arrReturn[0];
                        }
                    }
                    if (!string.IsNullOrEmpty(myData.Fax2) && myData.Fax2.Contains("-"))
                    {
                        var arrReturn = myData.Fax2.Split('-');
                        if (arrReturn.Length == 2)
                        {
                            myData.Fax2_1 = arrReturn[0];
                            myData.Fax2_2 = arrReturn[1];
                        }
                        else if (arrReturn.Length == 1)
                        {
                            myData.Fax2_1 = arrReturn[0];
                        }
                    }

                    #endregion
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return PartialView("CreateEdit_ReadAJ", myData);
                        case 6:
                            return PartialView("CreateEdit_ReadBL", myData);
                        default:
                            return PartialView("CreateEdit_Read", myData);
                    }
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = client.GetAsync(url + "/" + GuidNull).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;
                    //return View(myData);
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return PartialView("CreateEdit_ReadAJ", myData);
                        case 6:
                            return PartialView("CreateEdit_ReadBL", myData);
                        default:
                            return PartialView("CreateEdit_Read", myData);
                    }
                }
                return View("Error");
            }
        }
        private mstRekananForm PopulatePhoneFax(mstRekananForm myDataForm)
        {
            mstRekananForm myDataReturn = new mstRekananForm();
            myDataReturn.InjectFrom(myDataForm);
            if (!string.IsNullOrEmpty(myDataReturn.Phone1) && myDataReturn.Phone1.Contains("-"))
            {
                var arrReturn = myDataReturn.Phone1.Split('-');
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

            if (!string.IsNullOrEmpty(myDataReturn.Phone2) && myDataReturn.Phone2.Contains("-"))
            {
                var arrReturn = myDataReturn.Phone2.Split('-');
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

            if (!string.IsNullOrEmpty(myDataReturn.Phone3) && myDataReturn.Phone3.Contains("-"))
            {
                var arrReturn = myDataReturn.Phone3.Split('-');
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
        public async Task<ActionResult> CreateEdit_Edit(string flgEdit = "IsReadOnly")
        {
            string GuidNull = System.Guid.Empty.ToString();
            if ((System.Guid)tokenContainer.IdRekananContact != System.Guid.Empty)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    //ViewBag.IsEditable = "Editable";
                    //ViewBag.IsEditable = "IsReadOnly";
                    ViewBag.IsEditable = flgEdit;
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;

                    #region Populate Phone Fax Composit

                    var myDataReturn = PopulatePhoneFax(myData);

                    #endregion                    
                    //return View(myData);
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return View("CreateEdit_EditAJ", myDataReturn);
                        case 6:
                            return View("CreateEdit_EditBL", myDataReturn);
                        default:
                            return View("CreateEdit_Edit", myDataReturn);
                    }
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + GuidNull);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;
                    //return View(myData);
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return View("CreateEdit_EditAJ", myData);
                        case 6:
                            return View("CreateEdit_EditBL", myData);
                        default:
                            return View("CreateEdit_Edit", myData);
                    }
                }
                return View("Error");
            }
        }
        public async Task<ActionResult> CreateEdit_EditTab(string flgEdit = "IsReadOnly")
        {
            string GuidNull = System.Guid.Empty.ToString();
            if ((System.Guid)tokenContainer.IdRekananContact != System.Guid.Empty)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + tokenContainer.IdRekananContact.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    //ViewBag.IsEditable = "Editable";
                    //ViewBag.IsEditable = "IsReadOnly";
                    ViewBag.IsEditable = flgEdit;
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;

                    #region Populate Phone Fax Composit

                    var myDataReturn = PopulatePhoneFax(myData);

                    #endregion
                    //return View(myData);
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return View("CreateEdit_EditAJ", myData);
                        case 6:
                            return View("CreateEdit_EditBL", myData);
                        default:
                            return View("CreateEdit_Edit", myData);
                    }
                }
                return View("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + GuidNull);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                    myData.LbgPemeringkatColls = DataMasterProvider.Instance.LbgPemeringkatColls;
                    //return View(myData);
                    switch ((int)tokenContainer.IdTypeOfRekanan)
                    {
                        case 4:
                            return View("CreateEdit_EditAJ", myData);
                        case 6:
                            return View("CreateEdit_EditBL", myData);
                        default:
                            return View("CreateEdit_Edit", myData);
                    }
                }
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEdit(mstRekananForm myDataForm)
        {

            myDataForm.CreatedDate = DateTime.Today;
            myDataForm.CreatedUser = tokenContainer.UserId.ToString();

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
            myDataForm.Phone1 = strPhone1;

            string strPhone2 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone2_1))
            {
                strPhone2 = myDataForm.Phone2_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone2_2))
            {
                strPhone2 += "-" + myDataForm.Phone2_2;
            }
            myDataForm.Phone2 = strPhone2;

            string strPhone3 = string.Empty;
            if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            {
                strPhone3 = myDataForm.Phone3_1;
            }
            if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            {
                strPhone3 += "-" + myDataForm.Phone3_2;
            }
            myDataForm.Phone3 = strPhone3;

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


            //PERCOBAAAN SINGLETON
            //ImageContainer.Instance.ImageScanned = myDataForm.Fax2_1 + "zz";
            //HttpContext.Session["Fax2_2"] = myDataForm.Fax2_2;
            //ImageContainerUpd.ImageScanned = myDataForm.Fax2_2 + "yy";

            if ((System.Guid)tokenContainer.IdRekananContact != System.Guid.Empty)
            {
                HttpResponseMessage responseMessage1 = await client.PutAsJsonAsync(url + "/" + tokenContainer.IdRekananContact.ToString(), myDataForm);
                if (responseMessage1.IsSuccessStatusCode)
                {
                    //ViewBag.IsEditable = "IsReadOnly";
                    //return View(Emp);
                    HttpResponseMessage responseMessage2 = await client.GetAsync(url + "/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage2.IsSuccessStatusCode)
                    {
                        ViewBag.IsEditable = "IsReadOnly";
                        var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);

                        var myDataReturn = PopulatePhoneFax(myData);
                        //return View("CreateEdit_Read", myData);
                        switch ((int)tokenContainer.IdTypeOfRekanan)
                        {
                            case 4:
                                return View("CreateEdit_ReadAJ", myDataReturn);
                            case 6:
                                return View("CreateEdit_ReadBL", myDataReturn);
                            default:
                                return View("CreateEdit_Read", myDataReturn);
                        }
                    }
                }
                return RedirectToAction("Error");
            }
            else
            {
                myDataForm.IdRekanan = System.Guid.NewGuid();
                myDataForm.UserName = tokenContainer.UserName.ToString();
                myDataForm.UserEmail = tokenContainer.UserEmail.ToString();
                HttpResponseMessage responseMessage1 = await client.PostAsJsonAsync(url, myDataForm);
                if (responseMessage1.IsSuccessStatusCode)
                {
                    //untuk digunakan disesi selanjutnya
                    tokenContainer.IdRekananContact = myDataForm.IdRekanan;
                    HttpResponseMessage responseMessage3 = await client.GetAsync(url + "/" + myDataForm.IdRekanan.ToString());
                    if (responseMessage3.IsSuccessStatusCode)
                    {
                        ViewBag.IsEditable = "IsReadOnly";
                        var responseData = responseMessage3.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                        var myDataReturn = PopulatePhoneFax(myData);
                        //return View("CreateEdit_Read", myData);
                        switch ((int)tokenContainer.IdTypeOfRekanan)
                        {
                            case 4:
                                return View("CreateEdit_ReadAJ", myDataReturn);
                            case 6:
                                return View("CreateEdit_ReadBL", myDataReturn);
                            default:
                                return View("CreateEdit_Read", myDataReturn);
                        }
                    }
                }
                return RedirectToAction("Error");
            }
        }

        public async Task<ActionResult> Create()
        {
            string GuidNull = System.Guid.Empty.ToString();
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + GuidNull);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);
                return View(myData);
            }
            return View("Error");
            //return View(new mstRekanan());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(mstRekananForm Emp)
        {
           HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(url,Emp);
           if (responseMessage.IsSuccessStatusCode)
           {
               return RedirectToAction("Index");
           }
           return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Edit(Guid id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url+"/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);

                return View(myData);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, mstRekananForm Emp)
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

                var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);

                return View(myData);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, mstRekananForm Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> GetBySupervisorId()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetBySupervisorId/{1}", url, tokenContainer.SupervisorId.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<mstRekananMulti>(responseData);

                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetManagementRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetManagementRekanan", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fManagementRekanan_Result>>(responseData);

                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetManagementP2PK(int idTypeOfRekanan, bool isActive)
        {
            ViewBag.IdTypeOfRekanan = idTypeOfRekanan;
            ViewBag.IsActive = isActive;
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetManagementP2PK/{1}/{2}", url, idTypeOfRekanan.ToString(), isActive.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<trxManagementP2PK>>(responseData);

                return View("GetManagementP2PK", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GridViewPartial()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetBySupervisorId/{1}", url, tokenContainer.SupervisorId.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<mstRekananMulti>(responseData);

                return PartialView("GridViewPartial", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetManagementRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetManagementRekanan", url));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fManagementRekanan_Result>>(responseData);

                return PartialView("_GetManagementRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetManagementP2PK(int idTypeOfRekanan, bool isActive)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetManagementP2PK/{1}/{2}", url, idTypeOfRekanan.ToString(), isActive.ToString()));
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

        public async Task<ActionResult> RekananDetailedInfoOld(Guid IdRekanan, int IdTypeOfRekanan)
        {
            //assign current IdRekanan with selected id rekanan 
            tokenContainer.IdRekananContact = IdRekanan;
            tokenContainer.IdTypeOfRekanan = IdTypeOfRekanan;
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdRekanan.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                //ViewBag.IsEditable = "Editable";
                //ViewBag.IsEditable = "IsReadOnly";
                ViewBag.IsEditable = false;
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<mstRekananForm>(responseData);

                #region Populate Phone Fax Composit

                if (!string.IsNullOrEmpty(myData.Phone1) && myData.Phone1.Contains("-"))
                {
                    var arrReturn = myData.Phone1.Split('-');
                    if (arrReturn.Length == 2)
                    {
                        myData.Phone1_1 = arrReturn[0];
                        myData.Phone1_2 = arrReturn[1];
                    }
                    else if (arrReturn.Length == 1)
                    {
                        myData.Phone1_1 = arrReturn[0];
                    }
                }

                if (!string.IsNullOrEmpty(myData.Phone2) && myData.Phone2.Contains("-"))
                {
                    var arrReturn = myData.Phone2.Split('-');
                    if (arrReturn.Length == 2)
                    {
                        myData.Phone2_1 = arrReturn[0];
                        myData.Phone2_2 = arrReturn[1];
                    }
                    else if (arrReturn.Length == 1)
                    {
                        myData.Phone2_1 = arrReturn[0];
                    }
                }

                if (!string.IsNullOrEmpty(myData.Phone3) && myData.Phone3.Contains("-"))
                {
                    var arrReturn = myData.Phone3.Split('-');
                    if (arrReturn.Length == 2)
                    {
                        myData.Phone3_1 = arrReturn[0];
                        myData.Phone3_2 = arrReturn[1];
                    }
                    else if (arrReturn.Length == 1)
                    {
                        myData.Phone3_1 = arrReturn[0];
                    }
                }

                if (!string.IsNullOrEmpty(myData.Fax1) && myData.Fax1.Contains("-"))
                {
                    var arrReturn = myData.Fax1.Split('-');
                    if (arrReturn.Length == 2)
                    {
                        myData.Fax1_1 = arrReturn[0];
                        myData.Fax1_2 = arrReturn[1];
                    }
                    else if (arrReturn.Length == 1)
                    {
                        myData.Fax1_1 = arrReturn[0];
                    }
                }
                if (!string.IsNullOrEmpty(myData.Fax2) && myData.Fax2.Contains("-"))
                {
                    var arrReturn = myData.Fax2.Split('-');
                    if (arrReturn.Length == 2)
                    {
                        myData.Fax2_1 = arrReturn[0];
                        myData.Fax2_2 = arrReturn[1];
                    }
                    else if (arrReturn.Length == 1)
                    {
                        myData.Fax2_1 = arrReturn[0];
                    }
                }

                #endregion

                return View(myData);
                //return View(myData);
                //if ((int)tokenContainer.IdTypeOfRekanan == 6)
                //{
                //    return View("CreateEdit_ReadBL", myData);
                //}
                //else
                //{
                //    return View("CreateEdit_Read", myData);
                //}
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
        public async Task<ActionResult> RekananDetailedInfo_Edit0()
        {
            ViewBag.TabActive = 0;
            ViewBag.CurrentTab0 = "CreateEdit_EditTab";
            return View();
        }
    }
}
