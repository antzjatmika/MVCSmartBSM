using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstProdukAsuransiController : ApiController
    {
        private IDataAccessRepository<mstProdukAsuransi, int> _repository;
        private MstProdukAsuransiRep _repAsisten = new MstProdukAsuransiRep();
        //Inject the DataAccessRepository using Construction Injection 
        public MstProdukAsuransiController(IDataAccessRepository<mstProdukAsuransi, int> r, MstProdukAsuransiRep repAsisten)
        {
            _repository = r;
            _repAsisten = repAsisten;
        }
        public IEnumerable<mstProdukAsuransi> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstProdukAsuransi))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstProdukAsuransi))]
        public IHttpActionResult Post(mstProdukAsuransi myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstProdukAsuransi myData)
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
    }
}
