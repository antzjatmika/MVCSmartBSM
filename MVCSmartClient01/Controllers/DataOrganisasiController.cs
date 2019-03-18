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

namespace MVCSmartClient01.Controllers
{
    public class DataOrganisasiController : Controller
    {
        private readonly ITokenContainer tokenContainer;
        private ILog _auditer = LogManager.GetLogger("Audit");
        public DataOrganisasiController()
        {
            LogicalThreadContext.Properties["UserName"] = "Kampret";
            LogicalThreadContext.Properties["ActionType"] = "GetHome";
            _auditer.Info("Test Aja Dari Home");
            tokenContainer = new TokenContainer();
        }
        private List<DataOrganisasiItem> PopulateDataItem()
        {
            List<DataOrganisasiItem> lstDataOrganisasi = new List<DataOrganisasiItem>();
            lstDataOrganisasi.Add(new DataOrganisasiItem(){ItemNo = 1, ItemCaption = "Management"});
            lstDataOrganisasi.Add(new DataOrganisasiItem() { ItemNo = 2, ItemCaption = "Cabang" });
            lstDataOrganisasi.Add(new DataOrganisasiItem() { ItemNo = 3, ItemCaption = "Kepemilikan Saham" });
            lstDataOrganisasi.Add(new DataOrganisasiItem() { ItemNo = 4, ItemCaption = "Asisten" });
            lstDataOrganisasi.Add(new DataOrganisasiItem() { ItemNo = 5, ItemCaption = "Tenaga Ahli" });
            lstDataOrganisasi.Add(new DataOrganisasiItem() { ItemNo = 6, ItemCaption = "Contact Person" });
            return lstDataOrganisasi;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
    public class DataOrganisasiItem
    {
        public int ItemNo { get; set; }
        public string ItemCaption { get; set; }
    }
}
