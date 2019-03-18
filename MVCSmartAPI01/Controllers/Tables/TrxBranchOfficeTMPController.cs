using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;

namespace APIService.Controllers
{
    public class TrxBranchOfficeTMPController : ApiController
    {
        private IDataAccessRepository<trxBranchOfficeTMP, int> _repository;
        private trxBranchOfficeTMPRep _repBranch;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxBranchOfficeTMPController(IDataAccessRepository<trxBranchOfficeTMP, int> r, trxBranchOfficeTMPRep repBranch)
        {
            _repository = r;
            _repBranch = repBranch;
        }
        public IEnumerable<trxBranchOfficeTMP> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxBranchOfficeTMP))]
        public IHttpActionResult Get(int id)
        {
            return Ok(_repository.Get(id)); 
        }

        [ResponseType(typeof(trxBranchOfficeTMP))]
        public IHttpActionResult Post(trxBranchOfficeTMP myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxBranchOfficeTMP myData)
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
        [Route("api/TrxBranchOfficeTMP/GetByGuidHeader/{guidHeader}")]
        public IEnumerable<trxBranchOfficeTMP> GetByGuidHeader(System.Guid guidHeader)
        {
            IEnumerable<trxBranchOfficeTMP> BranchRekanan;
            BranchRekanan = _repBranch.GetByGuidHeader(guidHeader);
            return BranchRekanan;
        }

    }
}
