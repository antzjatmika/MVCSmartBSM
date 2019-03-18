using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using System.Configuration;
using ApiHelper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using Omu.ValueInjecter;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxTenagaAhliUploadController : Controller
    {
        private readonly ITokenContainer tokenContainer;
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxTenagaAhliUpload";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxTenagaAhliUploadController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxTenagaAhliUpload", SmartAPIUrl);

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
                var myData = JsonConvert.DeserializeObject<List<TrxTenagaAhliUpload>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetTenagaAhliByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTenagaAhliByRek/" + tokenContainer.IdRekananContact).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<TrxTenagaAhliUpload>>(responseData);
                return PartialView(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditTAUpload(int IdTAUpload = -1)
        {
            if (IdTAUpload > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdTAUpload);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<TrxTenagaAhliUpload>(responseData);
                    ImageContainerUpd.ImageBaseName = myData.ImageBaseName;
                    ImageContainerUpd.ImageExtension = myData.FileExt;
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                Guid myImageBaseName = Guid.NewGuid();
                TrxTenagaAhliUpload myData = new TrxTenagaAhliUpload();
                ImageContainerUpd.ImageBaseName = myImageBaseName;
                myData.ImageBaseName = myImageBaseName;
                ImageContainerUpd.ImageExtension = myData.FileExt;
                return View(myData);
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditTAUpload(TrxTenagaAhliUpload myData)
        {
            if (myData.IdTAUpload > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdTAUpload, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekananTab", "TrxTenagaAhliUpload");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekananTab", "TrxTenagaAhliUpload");
                }
                return RedirectToAction("Error");
            }
        }

        public ActionResult Create()
        {
            Guid myImageBaseName = Guid.NewGuid();
            TrxTenagaAhliUpload myData = new TrxTenagaAhliUpload();
            ImageContainerUpd.ImageBaseName = myImageBaseName;
            myData.ImageBaseName = myImageBaseName;
            return View(myData);
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(TrxTenagaAhliUpload myData)
        {
            myData.CreatedDate = DateTime.Today;
            myData.CreatedUser = tokenContainer.UserId.ToString();
            myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int IdDetDocument)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdDetDocument);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<TrxTenagaAhliUpload>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int IdDetDocument, TrxTenagaAhliUpload Emp)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + IdDetDocument, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxRekananDocument");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int IdDetDocument)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdDetDocument.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxRekananDocument");
            }
            return RedirectToAction("Error");
        }

        public ActionResult ImageUpload(string UploaderCtl = "uploadControl")
        {
            ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_1";
            return null;
        }

    }
}
