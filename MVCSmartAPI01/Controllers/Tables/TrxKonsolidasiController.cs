using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxKonsolidasiController : ApiController
    {
        private IDataAccessRepository<trxKonsolidasi, int> _repository;
        private TrxKonsolidasiRep _repPNilai = new TrxKonsolidasiRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxKonsolidasiController(IDataAccessRepository<trxKonsolidasi, int> r, TrxKonsolidasiRep repPNilai)
        {
            _repository = r;
            _repPNilai = repPNilai;
        }
        public IEnumerable<trxKonsolidasi> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxKonsolidasi))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxKonsolidasi))]
        public IHttpActionResult Post(trxKonsolidasi myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxKonsolidasi myData)
        {
            trxKonsolidasi SingleData = _repository.Get(id);
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
        [Route("api/TrxKonsolidasi/GetKonsoByPeriode/{IdRekanan}/{intPeriode}")]
        public IEnumerable<fKonsoByPeriode_Result> GetKonsoByPeriode(Guid IdRekanan, int intPeriode)
        {
            IEnumerable<fKonsoByPeriode_Result> PKonsoColls;
            PKonsoColls = _repPNilai.GetKonsoByPeriode(IdRekanan, intPeriode);
            return PKonsoColls;
        }
        [HttpGet]
        [Route("api/TrxKonsolidasi/GetKonsoResumeByPeriode/{IdRekanan}/{intPeriodeAwal}/{intPeriodeAkhir}/{intTipeUraian}")]
        public IEnumerable<fKonsoResumeByPeriode_Result> GetKonsoResumeByPeriode(Guid IdRekanan, int intPeriodeAwal, int intPeriodeAkhir, int intTipeUraian)
        {
            IEnumerable<fKonsoResumeByPeriode_Result> PKonsoResumeColls = _repPNilai.GetKonsoResumeByPeriode(IdRekanan, intPeriodeAwal, intPeriodeAkhir, intTipeUraian);
            return PKonsoResumeColls;
        }
        [HttpGet]
        [Route("api/TrxKonsolidasi/GetKategoriResikoByPenambah/{decPenambah}")]
        public IEnumerable<fKategoriResiko_Result> GetKategoriResikoByPenambah(decimal decPenambah)
        {
            IEnumerable<fKategoriResiko_Result> PKonsoResumeColls = _repPNilai.GetKategoriResikoByPenambah(decPenambah);
            return PKonsoResumeColls;
        }
        [HttpGet]
        [Route("api/TrxKonsolidasi/GetScoringByRekPeriode/{IdRekanan}/{Periode}")]
        public IEnumerable<fScoringByPeriode_Result> GetScoringByRekPeriode(Guid IdRekanan, int Periode)
        {
            IEnumerable<fScoringByPeriode_Result> ScoringColls = _repPNilai.GetScoringByRekPeriode(IdRekanan, Periode);
            return ScoringColls;
        }
        [HttpGet]
        [Route("api/TrxKonsolidasi/GetScoringResumeByRekPeriode/{IdRekanan}/{Periode}")]
        public IEnumerable<fScoringResumeByRek_Result> GetScoringResumeByRekPeriode(Guid IdRekanan, int Periode)
        {
            IEnumerable<fScoringResumeByRek_Result> ScoringColls = _repPNilai.GetScoringResumeByRekPeriode(IdRekanan, Periode);
            return ScoringColls;
        }
        [HttpGet]
        [Route("api/TrxKonsolidasi/GetScoringMultiByRekPeriode/{IdRekanan}/{Periode}")]
        public scoringResumeMulti GetScoringMultiByRekPeriode(Guid IdRekanan, int Periode)
        {
            scoringResumeMulti ScoringColls = _repPNilai.GetScoringMultiByRekPeriode(IdRekanan, Periode);
            return ScoringColls;
        }
        [HttpGet]
        [Route("api/TrxKonsolidasi/GetResumeRoaByRekPeriode/{IdRekanan}/{Periode}")]
        public IEnumerable<fResumeRoaByPeriode_Result> GetResumeRoaByRekPeriode(Guid IdRekanan, int Periode)
        {
            IEnumerable<fResumeRoaByPeriode_Result> ResumeColls = _repPNilai.GetResumeRoaByRekPeriode(IdRekanan, Periode);
            return ResumeColls;
        }
    }
}
