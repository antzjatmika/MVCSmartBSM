using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class DashboardController : ApiController
    {
        private IDataAccessRepository<fDashFeeByRekanan_Result, int> _repository;
        private DashboardRep _repDashboard;
        //Inject the DataAccessRepository using Construction Injection 
        public DashboardController(DashboardRep repDashboard)
        {
            _repDashboard = repDashboard;
        }
        public IEnumerable<fDashFeeByRekanan_Result> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(fDashFeeByRekanan_Result))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(fDashFeeByRekanan_Result))]
        public IHttpActionResult Post(fDashFeeByRekanan_Result myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, fDashFeeByRekanan_Result myData)
        {
            _repository.Put(id, myData);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpGet]
        [Route("api/Dashboard/FeeByRekanan/{Tahun}/{TopN}/{TypeOfRekanan}")]
        public IEnumerable<dashFeeByRekanan> FeeByRekanan(int Tahun, int TopN, int TypeOfRekanan)
        {
            IEnumerable<dashFeeByRekanan> DashReturnList;
            DashReturnList = _repDashboard.FeeByRekanan(Tahun, TopN, TypeOfRekanan);
            return DashReturnList;
        }
        [HttpGet]
        [Route("api/Dashboard/PekerjaanByRekanan/{Tahun}/{TopN}/{TypeOfRekanan}")]
        public IEnumerable<dashPekerjaanByRekanan> PekerjaanByRekanan(int Tahun, int TopN, int TypeOfRekanan)
        {
            IEnumerable<dashPekerjaanByRekanan> DashReturnList;
            DashReturnList = _repDashboard.PekerjaanByRekanan(Tahun, TopN, TypeOfRekanan);
            return DashReturnList;
        }
        [HttpGet]
        [Route("api/Dashboard/LatLongByRekanan/{TypeOfRekanan}")]
        public IEnumerable<mstRekananMap> LatLongByRekanan(int TypeOfRekanan)
        {
            IEnumerable<mstRekananMap> DashReturnList;
            DashReturnList = _repDashboard.LatLongByRekanan(TypeOfRekanan);
            return DashReturnList;
        }
    }
}
