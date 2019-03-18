using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSmartClient01.Models;
using ApiHelper;
using MVCSmartClient01.ApiInfrastructure;
using System.Threading.Tasks;
using log4net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;

namespace MVCSmartClient01.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        private ILog _auditer = LogManager.GetLogger("Audit");
        private string url = string.Empty;
        public HomeController()
        {
            LogicalThreadContext.Properties["UserName"] = "Kampret";
            LogicalThreadContext.Properties["ActionType"] = "GetHome";
            _auditer.Info("Test Aja Dari Home");
            tokenContainer = new TokenContainer();
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/MstRekanan", SmartAPIUrl);
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        private List<fInfo2LastModifiedByRek_Result> GetInfoLastUpdate(string pstrIdRekanan)
        {
            List<fInfo2LastModifiedByRek_Result> myData = new List<fInfo2LastModifiedByRek_Result>();
            HttpResponseMessage responseMessage2 = client.GetAsync(string.Format("{0}/GetInfo2LastModifiedByRek/{1}", url, pstrIdRekanan)).Result;
            if (responseMessage2.IsSuccessStatusCode)
            {
                var responseData = responseMessage2.Content.ReadAsStringAsync().Result;
                myData = JsonConvert.DeserializeObject<IEnumerable<fInfo2LastModifiedByRek_Result>>(responseData).ToList();
            }
            return myData;
        }
        [ChildActionOnly]
        public ActionResult _Header()
        {
            List<fInfo2LastModifiedByRek_Result> myData = new List<fInfo2LastModifiedByRek_Result>();
            string strRoleName = "LoginDefault";
            if(tokenContainer.RoleName != null)
            {
                strRoleName = tokenContainer.RoleName.ToString();
                myData = GetInfoLastUpdate(tokenContainer.IdRekananContact.ToString());
            }
            //TEMPAT UPDATE INFORMASI LAST UPDATE
            ViewBag.LastUpdatedProfile = "";
            ViewBag.LastUpdatedPekerjaan = "";
            if(myData.Count == 2)
            {
                ViewBag.LastUpdatedProfile = String.Format("{0} {1}","last upd", myData[0].LastModified);
                ViewBag.LastUpdatedPekerjaan = String.Format("{0} {1}", "last upd", myData[1].LastModified);
            }
            var model = new HeaderViewModel();
            model.RoleName = strRoleName;
            return View(model);
        }
        public ActionResult GetSideMenuByCode(string pstrSideMenuCode)
        {
            ViewBag.TestRole = pstrSideMenuCode;
            return PartialView("_SideMenu");
        }

    }
}
