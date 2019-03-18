using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstKategoriResikoController : ApiController
    {
        private IDataAccessRepository<mstKategoriResiko, int> _repository;
        private MstKategoriResikoRep _repKategoriResiko = new MstKategoriResikoRep();
        //Inject the DataAccessRepository using Construction Injection 
        public MstKategoriResikoController(IDataAccessRepository<mstKategoriResiko, int> r, MstKategoriResikoRep repKategoriResiko)
        {
            _repository = r;
            _repKategoriResiko = repKategoriResiko;
        }
        public IEnumerable<mstKategoriResiko> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(mstKategoriResiko))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(mstKategoriResiko))]
        public IHttpActionResult Post(mstKategoriResiko myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstKategoriResiko myData)
        {
            mstKategoriResiko SingleData = _repository.Get(id);
            SingleData.NilaiBawahC1 = myData.NilaiBawahC1;
            SingleData.NilaiBawahC2 = myData.NilaiBawahC2;
            SingleData.NilaiBawahC3 = myData.NilaiBawahC3;
            SingleData.NilaiBawahC4 = myData.NilaiBawahC4;

            SingleData.NilaiAtasC1 = myData.NilaiAtasC1;
            SingleData.NilaiAtasC2 = myData.NilaiAtasC2;
            SingleData.NilaiAtasC3 = myData.NilaiAtasC3;
            SingleData.NilaiAtasC4 = myData.NilaiAtasC4;

            _repository.Put(id, SingleData);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpGet]
        [Route("api/MstKategoriResiko/GetByIdTypeOfRekanan/{IdTypeOfRekanan}")]
        public IEnumerable<mstKategoriResiko> GetByIdTypeOfRekanan(int IdTypeOfRekanan)
        {
            IEnumerable<mstKategoriResiko> PKonsoColls;
            PKonsoColls = _repKategoriResiko.GetByIdTypeOfRekanan(IdTypeOfRekanan);
            return PKonsoColls;
        }
    }
}
