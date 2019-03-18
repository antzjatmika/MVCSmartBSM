using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstDatamasterController : ApiController
    {
        private MstTypeOfRekananRep _repTypeOfRekanan = new MstTypeOfRekananRep();
        private MstTypeOfRegionRep _repRegion = new MstTypeOfRegionRep();
        private MstSegmentasiRep _repSegmen = new MstSegmentasiRep();
        private MstReferenceRep _repReff = new MstReferenceRep();
        private MstSubRegionRep _repSubRegion = new MstSubRegionRep();
        public MstDatamasterController(MstTypeOfRekananRep repTypeOfRekanan, MstTypeOfRegionRep repRegion, MstSegmentasiRep repSegmen, MstReferenceRep repReff, MstSubRegionRep repSubRegion)
        {
            _repTypeOfRekanan = repTypeOfRekanan;
            _repRegion = repRegion;
            _repSegmen = repSegmen;
            _repReff = repReff;
            _repSubRegion = repSubRegion;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/MstDatamaster")]
        public mstDataMaster GetMaster()
        {
            mstDataMaster myDataMaster = new mstDataMaster();
            myDataMaster.TypeOfRekananColls = _repTypeOfRekanan.GetActive();
            myDataMaster.TypeOfRegionColls = _repRegion.GetActive();
            myDataMaster.SubRegionColls = _repSubRegion.Get();
            myDataMaster.TypeOfSegmentasi3Colls = _repSegmen.SegmenForKAP();
            myDataMaster.TypeOfSegmentasi5Colls = _repSegmen.GetActive();
            myDataMaster.TypeTotalAsetColls = _repReff.GetByType("TotalAset");
            myDataMaster.TriwulanColls = _repReff.GetByType("Triwulan");
            myDataMaster.TingkatResikoColls = _repReff.GetByType("ResikoRBC");
            myDataMaster.LbgPemeringkatColls = _repReff.GetByType("LembagaPemeringkat");
            return myDataMaster;
        }
    }
}
