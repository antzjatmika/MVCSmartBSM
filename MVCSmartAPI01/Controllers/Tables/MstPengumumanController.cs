using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class MstPengumumanController : ApiController
    {
        private IDataAccessRepository<mstPengumuman, int> _repository;
        private mstPengumumanRep _repContact = new mstPengumumanRep();
        private MstTypeOfRekananRep _repTypeOfRekanan;
        //Inject the DataAccessRepository using Construction Injection 
        public MstPengumumanController(IDataAccessRepository<mstPengumuman, int> r, mstPengumumanRep repPengumuman, MstTypeOfRekananRep rTRekanan)
        {
            _repository = r;
            _repContact = repPengumuman;
            _repTypeOfRekanan = rTRekanan;
        }
        public mstPengumumanMulti Get()
        {
            mstPengumumanMulti PengumumanColls = new mstPengumumanMulti();
            //re-populate MstTypeOfRekanan
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            PengumumanColls.MstPengumumanColls = _repository.Get();
            PengumumanColls.TypeOfRekananColls = myTypeOfRekananColls;

            return PengumumanColls;
        }

        [ResponseType(typeof(mstPengumuman))]
        public IHttpActionResult Get(int id)
        {
            var myTypeOfRekananColls = _repTypeOfRekanan.GetActive();
            mstPengumuman myPengumuman = new mstPengumuman();
            mstPengumumanSingle myData = new mstPengumumanSingle();
            if (id > 0)
            {
                myData.DataPengumuman = _repository.Get(id);
            }
            myData.TypeOfRekananColls = myTypeOfRekananColls;

            return Ok(myData); 
        }

        [ResponseType(typeof(mstPengumuman))]
        public IHttpActionResult Post(mstPengumuman myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstPengumuman myData)
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
        [Route("api/MstPengumuman/GetByRekanan/{idRekanan}")]
        public IEnumerable<mstPengumuman> GetByTypeOfRekanan(int IdTypeOfRekanan)
        {
            IEnumerable<mstPengumuman> PengumumanColls;
            PengumumanColls = _repContact.GetByIdTypeOfRekanan(IdTypeOfRekanan);
            return PengumumanColls;
        }
    }
}
