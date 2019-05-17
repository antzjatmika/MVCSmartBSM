using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxDocMandatoryDetailController : ApiController
    {
        private IDataAccessRepository<trxDocMandatoryDetail, int> _repository;
        private MstRekananRep _repRekanan;
        private TrxDocMandatoryDetailRep _repDocDetail;
        private MstTypeOfDocumentRep _repTipeDoc;
        private TrxDocMandatoryVerificationRep _repVerify;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxDocMandatoryDetailController(IDataAccessRepository<trxDocMandatoryDetail, int> r, MstRekananRep repRekanan
            , TrxDocMandatoryDetailRep repDocDetail, MstTypeOfDocumentRep repTipeDoc, TrxDocMandatoryVerificationRep repVerify)
        {
            _repository = r;
            _repRekanan = repRekanan;
            _repDocDetail = repDocDetail;
            _repTipeDoc = repTipeDoc;
            _repVerify = repVerify;
        }
        public IEnumerable<trxDocMandatoryDetail> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxDocMandatoryDetail))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxDocMandatoryDetail))]
        public IHttpActionResult Post(trxDocMandatoryDetail myData)
        {
            myData.LMDate = DateTime.Today;
            _repository.Post(myData);
            _repRekanan.UpdateNote(myData.IdRekanan, myData.ProcInfo);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxDocMandatoryDetail myData)
        {
            myData.LMDate = DateTime.Today;
            _repository.Put(id, myData);
            //Update catatan di mstRekanan
            _repRekanan.UpdateNote(myData.IdRekanan, myData.ProcInfo);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/TrxDocMandatoryDetail/GetTotDocumentDetailByRek/{idRekanan}")]
        public trxRekananDocumentMulti GetTotDocumentDetailByRek(System.Guid idRekanan)
        {
            trxRekananDocumentMulti DocumentByRekanan = new trxRekananDocumentMulti();
            var myTypeDocColls = _repTipeDoc.GetActive();
            DocumentByRekanan.TypeOfDocumentColls = myTypeDocColls;

            DocumentByRekanan.TotDocDetailByRekanan = _repDocDetail.GetTotDocumentDetailByRek(idRekanan);
            return DocumentByRekanan;
        }
        [HttpPost]
        [Route("api/TrxDocMandatoryDetail/StoreVerificationAdmin")]
        //[HttpPut]
        public IHttpActionResult StoreVerificationAdmin(trxDocMandatoryVerification myData)
        {
            _repVerify.StoreWiCheck(myData);
            return StatusCode(HttpStatusCode.NoContent);
        }
        //[HttpPost]
        //[Route("api/TrxDocMandatoryDetail/StoreVerificationAdmin")]
        ////[HttpPut]
        //public IHttpActionResult StoreVerificationAdmin(List<trxDocMandatoryVerification> myList)
        //{
        //    _repVerify.StoreWiCheck_List(myList);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [Route("api/TrxDocMandatoryDetail/GetTenagaAhliByRek/{idRekanan}")]
        public IEnumerable<trxDocMandatoryDetail> GetTenagaAhliByRek(System.Guid idRekanan)
        {
            List<trxDocMandatoryDetail> myDataList = new List<trxDocMandatoryDetail>();
            myDataList = _repDocDetail.GetTenagaAhliByRek(idRekanan, 27);
            return myDataList;
        }
        [Route("api/TrxDocMandatoryDetail/GetTenagaAhliTTByRek/{idRekanan}")]
        public IEnumerable<trxDocMandatoryDetail> GetTenagaAhliTTByRek(System.Guid idRekanan)
        {
            List<trxDocMandatoryDetail> myDataList = new List<trxDocMandatoryDetail>();
            myDataList = _repDocDetail.GetTenagaAhliByRek(idRekanan, 28);
            return myDataList;
        }

    }
}
