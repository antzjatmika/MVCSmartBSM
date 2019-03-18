using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstContactController : ApiController
    {
        private IDataAccessRepository<mstContact, int> _repository;
        private MstContactRep _repContact = new MstContactRep();
        //Inject the DataAccessRepository using Construction Injection 
        public MstContactController(IDataAccessRepository<mstContact, int> r, MstContactRep repContact)
        {
            _repository = r;
            _repContact = repContact;
        }
        public IEnumerable<mstContact> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstContact))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstContact))]
        public IHttpActionResult Post(mstContact myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstContact myData)
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
        [Route("api/MstContact/GetByRekanan/{idRekanan}")]
        public IEnumerable<mstContact> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<mstContact> ContactsRekanan;
            ContactsRekanan = _repContact.GetByRekanan(idRekanan);
            return ContactsRekanan;
        }
    }
}
