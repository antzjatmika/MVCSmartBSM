using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSmartClient01.Models;

namespace MVCSmartClient01.Controllers
{
    public class TrxDashboardController : Controller
    {
        // GET: Dashboard
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BarView()
        {
            return View("Index", dshTest.GetSales());
        }

        public ActionResult BarPartial()
        {
            return PartialView("BarViewPartial", dshTest.GetSales());
        }
    }
}