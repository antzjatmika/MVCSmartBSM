using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;

namespace APIService.Controllers
{
    public class TrxNotificationDetailController : ApiController
    {
        private IDataAccessRepository<trxNotificationDetail, int> _repository;
        private TrxNotificationDetailRep _repo;
        //Inject the DataAccessRepository using Construction Injection 
        public TrxNotificationDetailController(IDataAccessRepository<trxNotificationDetail, int> r, TrxNotificationDetailRep repo)
        {
            _repository = r;
            _repo = repo;
        }
        public IEnumerable<trxNotificationDetail> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxNotificationDetail))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxNotificationDetail))]
        public IHttpActionResult Post(trxNotificationDetail myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxNotificationDetail myData)
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/TrxNotificationDetail/SendNotiDetail/{IdNotificationDetail}")]
        public fGetNotiByIdNotification_Result SendNotiDetail(int IdNotificationDetail)
        {
            fGetNotiByIdNotification_Result myData = new fGetNotiByIdNotification_Result();
            _repo.SendNotiDetail(IdNotificationDetail);
            return myData;
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/TrxNotificationDetail/ReadNotiDetail/{IdNotificationDetail}")]
        public fGetNotiByIdNotification_Result ReadNotiDetail(int IdNotificationDetail)
        {
            fGetNotiByIdNotification_Result myData = new fGetNotiByIdNotification_Result();
            _repo.ReadNotiDetail(IdNotificationDetail);
            return myData;
        }

    }
}
