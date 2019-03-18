using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Collections;
using log4net;

//using System.Security.Claims;
//using System.Net.Http;

namespace APIService.Controllers
{
    public class MstRekananController : ApiController
    {
        private IDataAccessRepository<mstRekanan, System.Guid> _repository;
        private MstTypeOfRegionRep _repRegion;
        private MstTypeOfRekananRep _repTypeOfRekanan;
        private MstTypeOfBadanUsahaRep _repTypeOfBadanUsaha;
        private MstWilayahRep _repWilayah;
        private MstKecamatanRep _repKecamatan;
        private MstRekananRep _repRekanan;
        private ILog _auditer = LogManager.GetLogger("Audit");

        //Inject the DataAccessRepository using Construction Injection 
        public MstRekananController(IDataAccessRepository<mstRekanan, System.Guid> r, MstTypeOfRegionRep rRegion
            , MstTypeOfRekananRep rTRekanan, MstTypeOfBadanUsahaRep rTBadan, MstWilayahRep rTWilayah, MstKecamatanRep rTKecamatan
            , MstRekananRep rRekanan)
        {
            _repository = r;
            _repRegion = rRegion;
            _repTypeOfRekanan = rTRekanan;
            _repTypeOfBadanUsaha = rTBadan;
            _repWilayah = rTWilayah;
            _repKecamatan = rTKecamatan;
            _repRekanan = rRekanan;
        }
        public mstRekananMulti Get()
        {
            mstRekananMulti RekananBySupervisorId = new mstRekananMulti();
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
            _auditer.Info("Display List Rekanan");
            //re-populate MstRegion
            var myRegionColls = _repRegion.GetActive();
            RekananBySupervisorId.TypeOfRegionColls = myRegionColls;

            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            RekananBySupervisorId.TypeOfRekananColls = myTypeOfRekananColls;

            //re-populate MstTypeOfBadan
            var myTypeOfBadanColls = _repTypeOfBadanUsaha.GetActive();
            RekananBySupervisorId.TypeOfBadanUsahaColls = myTypeOfBadanColls;

            //re-populate MstWilayah
            var myWilayahColls = _repWilayah.GetActive();
            RekananBySupervisorId.WilayahColls = myWilayahColls;

            //re-populate MstKecamatan
            var myKecamatanColls = _repKecamatan.GetActive();
            RekananBySupervisorId.KecamatanColls = myKecamatanColls;
            RekananBySupervisorId.MstRekananMulti = _repository.Get();
            return RekananBySupervisorId;
        }

        [ResponseType(typeof(mstRekanan))]
        public IHttpActionResult Get(System.Guid id)
        {
            var myRekanan = new mstRekanan();
            if(id != System.Guid.Empty)
            {
                myRekanan = _repository.Get(id);
            }
            //re-populate MstRegion
            var myRegionColls = _repRegion.GetActive();
            myRekanan.TypeOfRegionColls = myRegionColls;

            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            myRekanan.TypeOfRekananColls = myTypeOfRekananColls;

            //re-populate MstTypeOfBadan
            var myTypeOfBadanColls = _repTypeOfBadanUsaha.GetActive();
            myRekanan.TypeOfBadanUsahaColls = myTypeOfBadanColls;

            //re-populate MstWilayah
            var myWilayahColls = _repWilayah.GetActive();
            myRekanan.WilayahColls = myWilayahColls;

            //re-populate MstKecamatan
            var myKecamatanColls = _repKecamatan.GetActive();
            myRekanan.KecamatanColls = myKecamatanColls;

            return Ok(myRekanan);
        }

        [ResponseType(typeof(mstRekanan))]
        public IHttpActionResult Post(mstRekanan myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(System.Guid id, mstRekanan myData)
        {
            _repository.Put(id, myData);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(System.Guid id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        //[Route("api/MstRekanan/GetBySupervisorId/{supervisorId}")]
        //public IEnumerable<mstRekanan> GetBySupervisorId(int supervisorId)
        //{
        //    LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
        //    LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
        //    _auditer.Info("Display List Rekanan By SupervisorId");
        //    var rekananCollection = _repRekanan.GetBySupervisorId(supervisorId);
        //    return _repRekanan.GetBySupervisorId(supervisorId);
        //}
        [Route("api/MstRekanan/GetBySupervisorId/{supervisorId}")]
        public mstRekananMulti GetBySupervisorId(int supervisorId)
        {
            mstRekananMulti RekananBySupervisorId = new mstRekananMulti();
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetRekanan";
            _auditer.Info("Display List Rekanan By SupervisorId");
            //re-populate MstRegion
            var myRegionColls = _repRegion.GetActive();
            RekananBySupervisorId.TypeOfRegionColls = myRegionColls;

            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            RekananBySupervisorId.TypeOfRekananColls = myTypeOfRekananColls;

            //re-populate MstTypeOfBadan
            var myTypeOfBadanColls = _repTypeOfBadanUsaha.GetActive();
            RekananBySupervisorId.TypeOfBadanUsahaColls = myTypeOfBadanColls;

            //re-populate MstWilayah
            var myWilayahColls = _repWilayah.GetActive();
            RekananBySupervisorId.WilayahColls = myWilayahColls;

            //re-populate MstKecamatan
            var myKecamatanColls = _repKecamatan.GetActive();
            RekananBySupervisorId.KecamatanColls = myKecamatanColls;

            var rekananCollection = _repRekanan.GetBySupervisorId(supervisorId);
            RekananBySupervisorId.MstRekananMulti = rekananCollection;
            return RekananBySupervisorId;
        }
        
    }
}
