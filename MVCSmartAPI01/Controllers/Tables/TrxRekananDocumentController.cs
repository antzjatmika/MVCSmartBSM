using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using System.Linq;

namespace APIService.Controllers
{
    public class TrxRekananDocumentController : ApiController
    {
        private IDataAccessRepository<trxRekananDocument, int> _repository;
        private TrxRekananDocumentRep _repRekDoc = new TrxRekananDocumentRep();
        private MstTypeOfDocumentRep _repTipeDoc;
        private TrxDocMandatoryDetailRep _repDocDetail;
        private TrxDocMandatoryFileRep _repDocFile;
        //private MstRekananRep _repRekanan;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxRekananDocumentController(IDataAccessRepository<trxRekananDocument, int> r, TrxRekananDocumentRep repRekDoc
            , MstTypeOfDocumentRep repTipeDoc, TrxDocMandatoryDetailRep repDocDetail, TrxDocMandatoryFileRep repDocFile
            //, MstRekananRep repRekanan
            )
        {
            _repository = r;
            _repRekDoc = repRekDoc;
            _repTipeDoc = repTipeDoc;
            _repDocDetail = repDocDetail;
            _repDocFile = repDocFile;
            //_repRekanan = repRekanan;
        }
        public trxRekananDocumentMulti Get()
        {
            trxRekananDocumentMulti DocumentByRekanan = new trxRekananDocumentMulti();
            var myTypeDocColls = _repTipeDoc.GetActive();
            DocumentByRekanan.TypeOfDocumentColls = myTypeDocColls;

            DocumentByRekanan.TrxRekananDocumentMaster = _repository.Get();
            return DocumentByRekanan;
        }

        [ResponseType(typeof(trxRekananDocumentForm))]
        public IHttpActionResult Get(int id)
        {
            trxRekananDocument rekDocTemp = new trxRekananDocument();
            trxRekananDocumentForm RekananDocSingle = new trxRekananDocumentForm();
            if (id > 0)
            {
                rekDocTemp = _repository.Get(id);
                RekananDocSingle.InjectFrom(rekDocTemp);
            }
            var myTypeDocColls = _repTipeDoc.GetActive();
            RekananDocSingle.TypeOfDocumentColls = myTypeDocColls;

            return Ok(RekananDocSingle); 
        }

        [ResponseType(typeof(trxRekananDocument))]
        public IHttpActionResult Post(trxRekananDocument myData)
        {
            trxRekananDocument rekDocTemp = new trxRekananDocument();
            rekDocTemp.InjectFrom(myData);
            _repository.Post(rekDocTemp);
            return Ok(rekDocTemp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxRekananDocument myData)
        {
            trxRekananDocument rekDocTemp = new trxRekananDocument();
            rekDocTemp.InjectFrom(myData);
            _repository.Put(id, rekDocTemp);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/TrxRekananDocument/GetByRekanan/{idTypeOfRekanan}")]
        public trxRekananDocumentMulti GetByRekanan(int idTypeOfRekanan)
        {
            trxRekananDocumentMulti DocumentByRekanan = new trxRekananDocumentMulti();
            var myTypeDocColls = _repTipeDoc.GetActive();
            DocumentByRekanan.TypeOfDocumentColls = myTypeDocColls;

            DocumentByRekanan.TrxRekananDocumentMulti = _repRekDoc.GetByRekanan(idTypeOfRekanan);
            return DocumentByRekanan;
        }
        [Route("api/TrxRekananDocument/GetDocDetailByRekanan/{IdRekanan}/{IdTypeOfDocument}")]
        public IEnumerable<trxDocMandatoryDetail> GetDocDetailByRekanan(System.Guid IdRekanan, int IdTypeOfDocument)
        {
            List<trxDocMandatoryDetail> DocumentDetail = new List<trxDocMandatoryDetail>();
            DocumentDetail = _repDocDetail.GetDetailByRekanan(IdRekanan, IdTypeOfDocument).ToList();
            return DocumentDetail;
        }
        [Route("api/TrxRekananDocument/GetFileByDetail/{IdDocMandatoryDetail}")]
        public IEnumerable<trxDocMandatoryFile> GetFileByDetail(int IdDocMandatoryDetail)
        {
            List<trxDocMandatoryFile> DocumentDetail = new List<trxDocMandatoryFile>();
            DocumentDetail = _repDocFile.GetFileByDetail(IdDocMandatoryDetail).ToList();
            return DocumentDetail;
        }
    }
}
