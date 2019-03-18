using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;

namespace APIService.Controllers
{
    public class TrxBranchOfficeController : ApiController
    {
        private IDataAccessRepository<trxBranchOffice, int> _repository;
        private TrxBranchOfficeRep _repBranch;
        private MstWilayahRep _repWilayah;
        private MstKecamatanRep _repKecamatan;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxBranchOfficeController(IDataAccessRepository<trxBranchOffice, int> r, TrxBranchOfficeRep repBranch, MstWilayahRep rTWilayah, MstKecamatanRep rTKecamatan)
        {
            _repository = r;
            _repBranch = repBranch;
            _repWilayah = rTWilayah;
            _repKecamatan = rTKecamatan;
        }
        public IEnumerable<trxBranchOffice> Get()
        {
            return _repository.Get();
        }

        //public trxBranchOfficeMulti Get()
        //{
        //    trxBranchOfficeMulti BranchOfficeByRekanan = new trxBranchOfficeMulti();
        //    //re-populate MstWilayah
        //    var myWilayahColls = _repWilayah.GetActive();
        //    BranchOfficeByRekanan.WilayahColls = myWilayahColls;

        //    //re-populate MstKecamatan
        //    var myKecamatanColls = _repKecamatan.GetActive();
        //    BranchOfficeByRekanan.KecamatanColls = myKecamatanColls;

        //    BranchOfficeByRekanan.TrxBranchOfficeMulti = _repository.Get();
        //    return BranchOfficeByRekanan;
        //}

        [ResponseType(typeof(trxBranchOffice))]
        public IHttpActionResult Get(int id)
        {
            return Ok(_repository.Get(id)); 
            //trxBranchOffice branchOTemp = new trxBranchOffice();
            //trxBranchOfficeForm branchOSingle = new trxBranchOfficeForm();
            //if (id > 0)
            //{
            //    branchOTemp = _repository.Get(id);
            //    //RekananSingle
            //    //branchOSingle.InjectFrom(branchOTemp);
            //}

            //re-populate MstWilayah
            //var myWilayahColls = _repWilayah.GetActive();
            //branchOSingle.WilayahColls = myWilayahColls;

            //re-populate MstKecamatan
            //var myKecamatanColls = _repKecamatan.GetActive();
            //branchOSingle.KecamatanColls = myKecamatanColls;

            //return Ok(branchOSingle);
            //return Ok(branchOTemp);
        }

        [ResponseType(typeof(trxBranchOffice))]
        public IHttpActionResult Post(trxBranchOffice myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxBranchOffice myData)
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
        //GetByIdOrganisasi
        [Route("api/TrxBranchOffice/GetByIdOrganisasi/{idOrganisasi}")]
        public trxBranchOfficeMulti GetByIdOrganisasi(int idOrganisasi)
        {
            trxBranchOfficeMulti BranchByOrg = new trxBranchOfficeMulti();
            BranchByOrg.TrxBranchOfficeMulti = _repBranch.GetByIdOrganisasi(idOrganisasi);
            //re-populate MstWilayah
            var myWilayahColls = _repWilayah.GetActive();
            BranchByOrg.WilayahColls = myWilayahColls;
            //re-populate MstKecamatan
            var myKecamatanColls = _repKecamatan.GetActive();
            BranchByOrg.KecamatanColls = myKecamatanColls;

            return BranchByOrg;
        }
        [Route("api/TrxBranchOffice/GetByRekanan/{idRekanan}")]
        public IEnumerable<trxBranchOffice> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<trxBranchOffice> BranchRekanan;
            BranchRekanan = _repBranch.GetByRekanan(idRekanan);
            return BranchRekanan;
        }
        [Route("api/TrxBranchOffice/GetByGuidHeader/{guidHeader}")]
        public IEnumerable<trxBranchOffice> GetByGuidHeader(System.Guid guidHeader)
        {
            IEnumerable<trxBranchOffice> BranchRekanan;
            BranchRekanan = _repBranch.GetByGuidHeader(guidHeader);
            return BranchRekanan;
        }

    }
}
