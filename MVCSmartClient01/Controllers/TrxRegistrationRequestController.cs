using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using System.Configuration;
using ApiHelper;
using System.Text;
using System.Web;
//using System.Globalization;

namespace MVCSmartClient01.Controllers
{
    using ApiHelper;
    using ApiHelper.Client;
    using ApiInfrastructure;
    using ApiInfrastructure.Client;
    public class TrxRegistrationRequestController : BaseController
    {
        HttpClient client;
        private readonly ILoginClient loginClient;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxRegistrationRequest";
        string url = string.Empty;
        string urlAcc = string.Empty;
        string urlHeader = string.Empty;
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxRegistrationRequestController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            urlHeader = string.Format("{0}/api/TrxDetailPekerjaanHeader", SmartAPIUrl);
            url = string.Format("{0}/api/TrxRegistrationRequest", SmartAPIUrl);
            urlAcc = string.Format("{0}/api/Account", SmartAPIUrl);
            client = new HttpClient();
            tokenContainer = new TokenContainer();
            var apiClient = new ApiClient(HttpClientInstance.Instance, tokenContainer);
            loginClient = new LoginClient(apiClient);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            //trxRegistrationRequestMulti myData = new trxRegistrationRequestMulti();
            //HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetView", url));
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //    var myDataRegistration = JsonConvert.DeserializeObject<List<trxRegistrationRequest>>(responseData);
            //    myData.RegistrationColls = myDataRegistration;
            //    myData.TypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
            //    myData.StatusRekananColls = DataMasterProvider.Instance.StatusRekananColls;

            //   return View(myData);
            //}
            //return View("Error");
            return View("Index");
        }
        public async Task<ActionResult> _Index()
        {
            //trxRegistrationRequestMulti myData = new trxRegistrationRequestMulti();
            //HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetView", url));
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //    var myDataRegistration = JsonConvert.DeserializeObject<List<trxRegistrationRequest>>(responseData);
            //    myData.RegistrationColls = myDataRegistration;
            //    myData.TypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
            //    myData.StatusRekananColls = DataMasterProvider.Instance.StatusRekananColls;

            //    return PartialView(myData);
            //}
            //return View("Error");
            return PartialView("_Index");
        }
        public ActionResult _IndexIsActive(Byte IsActive)
        {
            trxRegistrationRequestMulti myData = new trxRegistrationRequestMulti();
            HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetViewIsActive/{1}", url, IsActive.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataRegistration = JsonConvert.DeserializeObject<List<trxRegistrationRequest>>(responseData);
                myData.RegistrationColls = myDataRegistration;
                myData.TypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
                myData.StatusRekananColls = DataMasterProvider.Instance.StatusRekananColls;

                ViewBag.IsActive = IsActive;
                return PartialView("_Index", myData);
            }
            return View("Error");
        }
 
        public ActionResult Create()
        {
            return View(new trxRegistrationRequest());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxRegistrationRequest Emp)
        {
           HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(url,Emp);
           if (responseMessage.IsSuccessStatusCode)
           {
               return RedirectToAction("Index");
           }
           return RedirectToAction("Error");
        }
        private RegisterBindingModel RegisterModel(trxRegistrationRequest myData)
        {
            string strNPWP = Guid.NewGuid().ToString();
            string strBarePassword = GenerateKataKunci();
            string strPassKey = string.Concat(strBarePassword, "@Pcp");
            RegisterBindingModel model = new RegisterBindingModel();
            model.NamaRekanan = myData.NamaLengkap;
            model.Email = strNPWP + "@pcp.com";
            model.NomorNPWP = strNPWP;
            model.IdTypeOfRekanan = myData.IdTypeOfRekanan;
            model.Password = strPassKey;
            model.ConfirmPassword = strPassKey;
            model.BarePassword = MaskPasskey(strBarePassword, true);
            model.IsActive = 1;

            return model;
        }
        //REGISTER CALON REKANAN LEWAT ADMIN
        [HttpPost]
        public async Task<ActionResult> AddUpdateRegRekanan(trxRegistrationRequest myData)
        {
            bool bolSaveOK = false;
            if (myData.IdRegRequest > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(String.Format("{0}/{1}", url, myData.IdRegRequest), myData);
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
                //REGISTER CALON REKANAN LEWAT ADMIN
                RegisterBindingModel model = RegisterModel(myData);
                var response = await loginClient.RegisterByAdmin(model);
                if (response.StatusIsSuccessful)
                {
                    bolSaveOK = true;
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            if (bolSaveOK)
            {
                return Redirect("Index");
                //trxRegistrationRequestMulti myDataMulti = new trxRegistrationRequestMulti();
                ////HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/GetView", url));
                //HttpResponseMessage responseMessage = client.GetAsync(String.Format("{0}/GetViewIsActive/{1}", url, "2")).Result;
                //if (responseMessage.IsSuccessStatusCode)
                //{
                //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                //    var myDataRegistration = JsonConvert.DeserializeObject<List<trxRegistrationRequest>>(responseData);
                //    myDataMulti.RegistrationColls = myDataRegistration;
                //    myDataMulti.TypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
                //    ViewBag.IsActive = 2;
                //    //RedirectToAction("Index");
                //    return PartialView("_Index", myDataMulti);
                //}
                //return View("Error");
            }
            //else
            //{
            return RedirectToAction("Error");
            //}
        }
        public string Encode(string content)
        {
            byte[] encodedBytes = UTF8Encoding.UTF8.GetBytes(content);
            string externalIdEncoded = HttpServerUtility.UrlTokenEncode(encodedBytes);

            return externalIdEncoded;
        }

        [HttpGet]
        public async Task<ActionResult> ApprovalWithReason(string myUserName, byte IsActive, string Catatan)
        {
            string strCreatedUser = tokenContainer.UserName.ToString();
            Catatan = Encode(Catatan);
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/ApprovalWithReason/{1}/{2}/{3}/{4}", url, myUserName, IsActive.ToString(), Catatan, strCreatedUser));
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

                var Employee = JsonConvert.DeserializeObject<trxRegistrationRequest>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxRegistrationRequest Emp)
        {
 
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url+"/" +id, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _ResetPasswordDefault(string myUserName)
        {
            string BarePassword = GenerateKataKunci();
            //Password User Name
            string myNewPassword = string.Concat(BarePassword, "@Pcp");
            //Password Kode yang disimpan di database
            string myBarePassword = MaskPasskey(BarePassword, true);

            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/ChangePasswordByUserName/{1}/{2}", urlAcc, myUserName, myNewPassword));
            if (responseMessage.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessageReg = await client.GetAsync(String.Format("{0}/ChangePasswordByUserName/{1}/{2}", url, myUserName, BarePassword));
                if (responseMessageReg.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<trxRegistrationRequest>(responseData);
 
                return View(Employee);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxRegistrationRequest Emp)
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
