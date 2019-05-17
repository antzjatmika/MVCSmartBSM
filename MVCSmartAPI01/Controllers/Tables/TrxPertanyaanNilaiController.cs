using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxPertanyaanNilaiController : ApiController
    {
        private IDataAccessRepository<trxPertanyaanNilai, int> _repository;
        private TrxPertanyaanNilaiRep _repPNilai = new TrxPertanyaanNilaiRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxPertanyaanNilaiController(IDataAccessRepository<trxPertanyaanNilai, int> r, TrxPertanyaanNilaiRep repPNilai)
        {
            _repository = r;
            _repPNilai = repPNilai;
        }
        public IEnumerable<trxPertanyaanNilai> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxPertanyaanNilai))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxPertanyaanNilai))]
        public IHttpActionResult Post(trxPertanyaanNilai myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxPertanyaanNilai myData)
        {
            trxPertanyaanNilai SingleData = _repository.Get(id);
            SingleData.Nilai = myData.Nilai;
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
        [Route("api/TrxPertanyaanNilai/GetNilaiByPenilai/{IdTypeOfGroup}/{IdPenilai}/{IdRekanan}")]
        public IEnumerable<vwPertanyaanNilai> GetNilaiByPenilai(int IdTypeOfGroup, int IdPenilai, Guid IdRekanan)
        {
            IEnumerable<vwPertanyaanNilai> PNilaiColls;
            PNilaiColls = _repPNilai.GetNilaiByPenilai(IdTypeOfGroup, IdPenilai, IdRekanan);
            return PNilaiColls;
        }
        [HttpGet]
        [Route("api/TrxPertanyaanNilai/GetNilaiAkhir/{IdRekanan}/{TahunBulan}")]
        public IEnumerable<vwPertanyaanNilaiAkhir> GetNilaiAkhir(Guid IdRekanan, int TahunBulan)
        {
            IEnumerable<vwPertanyaanNilaiAkhir> PNilaiColls = _repPNilai.GetNilaiAkhir(IdRekanan, TahunBulan);
            return PNilaiColls;
        }
        [HttpGet]
        [Route("api/TrxPertanyaanNilai/GetNilaiByParam/{IdRekanan}/{Periode}/{IdTypeOfGroup}/{IdPenilai}")]
        public IEnumerable<fGetPertanyaanNilaiByParam_Result> GetNilaiByParam(Guid IdRekanan, int Periode, int IdTypeOfGroup, int IdPenilai)
        {
            IEnumerable<fGetPertanyaanNilaiByParam_Result> PNilaiColls = _repPNilai.GetNilaiByParam(IdRekanan, Periode, IdTypeOfGroup, IdPenilai);
            return PNilaiColls;
        }
    }
}
