using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using System.Data.SqlClient;

namespace APIService.Controllers
{
    public class TrxBranchOfficeHeaderController : ApiController
    {
        private IDataAccessRepository<trxBranchOfficeHeader, int> _repository;
        private TrxBranchOfficeHeaderRep _repBranchHeader;
        private TrxBranchOfficeRep _repBranch;
        private MstWilayahRep _repWilayah;
        private MstKecamatanRep _repKecamatan;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxBranchOfficeHeaderController(IDataAccessRepository<trxBranchOfficeHeader, int> r, TrxBranchOfficeHeaderRep repBranchHeader, MstWilayahRep rTWilayah, MstKecamatanRep rTKecamatan, TrxBranchOfficeRep repBranch)
        {
            _repository = r;
            _repBranchHeader = repBranchHeader;
            _repWilayah = rTWilayah;
            _repKecamatan = rTKecamatan;
            _repBranch = repBranch;
        }
        public IEnumerable<trxBranchOfficeHeader> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxBranchOfficeHeader))]
        public IHttpActionResult Get(int id)
        {
            return Ok(_repository.Get(id)); 
        }

        [ResponseType(typeof(trxBranchOfficeHeader))]
        public IHttpActionResult Post(trxBranchOfficeHeader myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxBranchOfficeHeader myData)
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
        [Route("api/TrxBranchOfficeHeader/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxBranchOfficeHeader> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxBranchOfficeHeader> BranchRekanan;
            BranchRekanan = _repBranchHeader.GetByRekanan(idRekanan);
            return BranchRekanan;
        }
        [HttpGet]
        [Route("api/TrxBranchOfficeHeader/GetByGuidHeader/{GuidHeader}")]
        public trxBranchOfficeHeaderForm GetByGuidHeader(System.Guid GuidHeader)
        {
            trxBranchOfficeHeaderForm myDataForm = new trxBranchOfficeHeaderForm();
            trxBranchOfficeHeader myData = _repBranchHeader.GetByGuidHeader(GuidHeader);
            myDataForm.InjectFrom(myData);
            IEnumerable<trxBranchOffice> myDataList = _repBranch.GetByGuidHeader(GuidHeader);
            myDataForm.BranchOfficeColls = myDataList;
            return myDataForm;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("api/TrxBranchOfficeHeader/ExecTransBranch/{pGuidHeader}")]
        public string TransDataBranch(Guid pGuidHeader)
        {
            string strReturn = "Executed OK";
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    var paramIdHeader = new SqlParameter("@GuidHeader", pGuidHeader);
                    var result = context.Database.ExecuteSqlCommand("EXEC spTranDataBranch @GuidHeader", paramIdHeader);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }
            return strReturn;
        }

    }
}
