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
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Data.SqlClient;
using System.Linq;
using Omu.ValueInjecter;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Data.Filtering;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class MstPengumumanController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/MstPengumuman";
        string urlPengumuman = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public MstPengumumanController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];

            urlPengumuman = string.Format("{0}/api/MstPengumuman", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(urlPengumuman);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlPengumuman);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var dataPengumuman = JsonConvert.DeserializeObject<mstPengumumanMulti>(responseData);
                return View("GetAllPengumuman", dataPengumuman);
            }
            return View("Error");
        }

        public async Task<ActionResult> GetAllPengumuman()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlPengumuman);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var dataPengumuman = JsonConvert.DeserializeObject<mstPengumumanMulti>(responseData);
                return View("GetAllPengumuman", dataPengumuman);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetAllPengumuman()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlPengumuman);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var dataPengumuman = JsonConvert.DeserializeObject<mstPengumumanMulti>(responseData);
                return PartialView("_GetAllPengumuman", dataPengumuman);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByIdTypeOfRekanan(int idTypeOfRekanan)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlPengumuman + "/GetByIdTypeOfRekanan/" + idTypeOfRekanan);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<mstPengumuman>>(responseData);
                return PartialView("_GetByIdTypeOfRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditPengumuman(int Id)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await client.GetAsync(urlPengumuman + "/" + Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<mstPengumumanSingle>(responseData);

                if (Id < 0)
                {
                    //myData = new mstPengumumanSingle();
                    mstPengumuman _myData = new mstPengumuman();
                    myData.DataPengumuman = _myData;
                    myData.DataPengumuman.ImageName = System.Guid.NewGuid();
                    myData.DataPengumuman.CreatedUser = tokenContainer.UserId.ToString();
                    myData.DataPengumuman.CreatedDate = DateTime.Today;
                }
                return View("_AddEditPengumuman", myData);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> _AddEditPengumuman(mstPengumumanSingle myDataForm)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            mstPengumuman myData = new mstPengumuman();
            myData.InjectFrom(myDataForm.DataPengumuman);
            if (myData.Id > 0)
            {
                responseMessage = await client.PutAsJsonAsync(urlPengumuman + "/" + myData.Id, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllPengumuman");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                responseMessage = await client.PostAsJsonAsync(urlPengumuman, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllPengumuman");
                }
                return RedirectToAction("Error");
            }
        }

        //[HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlPengumuman + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllPengumuman");
            }
            return RedirectToAction("Error");
        }

    }
}
