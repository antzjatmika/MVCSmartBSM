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
using Omu.ValueInjecter;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxBranchOfficeController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxBranchOffice";
        string url = string.Empty;
 
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxBranchOfficeController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxBranchOffice", SmartAPIUrl);
            
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
        public async Task<ActionResult> GetByIdOrganisasi()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByIdOrganisasi/" + tokenContainer.IdOrganisasi.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
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
        public async Task<ActionResult> _BranchByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<trxBranchOffice>>(responseData);
                return PartialView("_BranchByRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _BranchByIdOrganisasi()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByIdOrganisasi/" + tokenContainer.IdOrganisasi.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxBranchOfficeMulti>(responseData);
                return PartialView("_BranchByIdOrganisasi", myData);
            }
            return View("Error");
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

            //if (!string.IsNullOrEmpty(myDataReturn.Telephone3) && myDataReturn.Telephone3.Contains("-"))
            //{
            //    var arrReturn = myDataReturn.Telephone3.Split('-');
            //    if (arrReturn.Length == 2)
            //    {
            //        myDataReturn.Phone3_1 = arrReturn[0];
            //        myDataReturn.Phone3_2 = arrReturn[1];
            //    }
            //    else if (arrReturn.Length == 1)
            //    {
            //        myDataReturn.Phone3_1 = arrReturn[0];
            //    }
            //}

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


        public async Task<ActionResult> _AddEditBranch(int IdData = -1)
        {
            trxBranchOfficeForm myDataForm = new trxBranchOfficeForm();
            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdData);
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
        public async Task<ActionResult> _AddEditBranch(trxBranchOfficeForm myDataForm)
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

            //string strPhone3 = string.Empty;
            //if (!string.IsNullOrEmpty(myDataForm.Phone3_1))
            //{
            //    strPhone3 = myDataForm.Phone3_1;
            //}
            //if (!string.IsNullOrEmpty(myDataForm.Phone3_2))
            //{
            //    strPhone3 += "-" + myDataForm.Phone3_2;
            //}
            //myDataForm.Telephone3 = strPhone3;

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
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdCabang, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.InjectFrom(myDataForm);
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
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



        public async Task<ActionResult> Create()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/0");
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.IsEditable = "IsReadOnly";
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxBranchOfficeForm>(responseData);
                return View(myData);
            }
            return View("Error");
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxBranchOfficeForm Emp)
        {
            Emp.IdOrganisasi = (Int32)tokenContainer.IdOrganisasi;
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

                var myData = JsonConvert.DeserializeObject<trxBranchOfficeForm>(responseData);
 
                return View(myData);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxBranchOfficeForm Emp)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url+"/" +id, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int IdData)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myData = JsonConvert.DeserializeObject<trxBranchOfficeForm>(responseData);
 
                return View(myData);
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

    }
}
