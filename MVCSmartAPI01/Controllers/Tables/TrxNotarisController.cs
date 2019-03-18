using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using log4net;
using DevExpress.Data.Filtering;

namespace APIService.Controllers
{
    public class trxNotarisController : ApiController
    {
        private IDataAccessRepository<trxNotari, int> _repository;
        private TrxNotarisRep _repNotaris;
        
        private ILog _auditer = LogManager.GetLogger("Audit");
        //Inject the DataAccessRepository using Construction Injection 
        public trxNotarisController(IDataAccessRepository<trxNotari, int> r, TrxNotarisRep rNotaris)
        {
            _repository = r;
            _repNotaris = rNotaris;
        }
        public IEnumerable<trxNotari> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxNotarisFormNew))]
        public IHttpActionResult Get(int id)
        {
            trxNotari notTemp = new trxNotari();
            trxNotarisFormNew NotarisSingle = new trxNotarisFormNew();
            if (id > 0)
            {
                notTemp = _repository.Get(id);
                //get notarisTabular by rekanan

                //notTemp.IdRekanan;
                NotarisSingle.InjectFrom(notTemp);
            }
            else
            {
                //NotarisSingle.IsKoperasiMember = false;
                //NotarisSingle.IsBapepamReg = false;
                //NotarisSingle.IsINIMember = false;
                //NotarisSingle.IsIPPATMember = false;
            }
            return Ok(NotarisSingle); 
        }

        [ResponseType(typeof(trxNotarisForm))]
        public IHttpActionResult Post(trxNotarisForm myData)
        {
            trxNotari notTemp = new trxNotari();
            notTemp.InjectFrom(myData);
            _repository.Post(notTemp);
            return Ok(notTemp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxNotarisForm myData)
        {
            trxNotari notTemp = new trxNotari();
            notTemp.InjectFrom(myData);
            _repository.Put(id, notTemp);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/TrxNotaris/AddUpdateNotarisTabular")]
        //[ResponseType(typeof(trxNotarisTabular))]
        [HttpPut, HttpPost]
        public IHttpActionResult AddUpdateNotarisTabular(trxNotarisTabular myData)
        {
            trxNotarisTabular notTemp = new trxNotarisTabular();
            notTemp.InjectFrom(myData);

            if (myData.IdNotarisTabular > 0)
            {
                _repNotaris.PutTabular(notTemp.IdNotarisTabular, notTemp);
            }
            else
            {
                _repNotaris.PostTabular(notTemp);
            }
            return Ok(notTemp);
        }

        [Route("api/TrxNotaris/AddUpdateNotarisDetail")]
        [HttpPut, HttpPost]
        public IHttpActionResult AddUpdateNotarisDetail(trxNotarisDetail myData)
        {
            trxNotarisDetail notTemp = new trxNotarisDetail();
            notTemp.InjectFrom(myData);

            if (myData.IdNotarisDetail > 0)
            {
                _repNotaris.PutDetail(notTemp.IdNotarisDetail, notTemp);
            }
            else
            {
                _repNotaris.PostDetail(notTemp);
            }
            return Ok(notTemp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/TrxNotaris/DeleteNotarisTabular/{IdNotarisTabular}")]
        //[Route("api/TrxNotaris/DeleteNotarisTabular")]
        [HttpGet]
        public IHttpActionResult DeleteTabular(int IdNotarisTabular)
        {
            _repNotaris.DeleteTabular(IdNotarisTabular);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/TrxNotaris/GetByRekananId/{rekananId}")]
        public trxNotarisFormNew GetByRekananId(System.Guid rekananId)
        {
            trxNotarisFormNew NotarisByRekananId = new trxNotarisFormNew();
            NotarisByRekananId = _repNotaris.GetByRekananId(rekananId);
            return NotarisByRekananId;
        }
        [Route("api/TrxNotaris/NotarisTabularGetByRek/{rekananId}")]
        public List<trxNotarisTabular> GetNotarisTabularByRek(System.Guid rekananId)
        {
            List<trxNotarisTabular> myDataList = new List<trxNotarisTabular>();
            myDataList = _repNotaris.GetNotarisTabularByRek(rekananId);
            return myDataList;
        }
        [Route("api/TrxNotaris/NotarisDetailGetByRek/{rekananId}")]
        public trxNotarisDetail GetNotarisDetailByRek(System.Guid rekananId)
        {
            trxNotarisDetail myData = new trxNotarisDetail();
            myData = _repNotaris.GetNotarisDetailByRek(rekananId);
            return myData;
        }
        [Route("api/TrxNotaris/NotarisDetailAll")]
        public List<vwNotarisTabular> GetNotarisDetailAll()
        {
            List<vwNotarisTabular> myData = new List<vwNotarisTabular>();
            myData = _repNotaris.GetNotarisDetailAll();
            return myData;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/TrxNotaris/XLS_NotarisDetailAll/{strFilterExpression}")]
        public IEnumerable<vwNotarisTabular> XLS_NotarisDetailAll(string strFilterExpression)
        {
            string strFilterExp = string.Empty;
            LogicalThreadContext.Properties["UserName"] = User.Identity.Name;
            LogicalThreadContext.Properties["ActionType"] = "GetNotarisDetailAll";
            _auditer.Info("Display List Notaris Detail");

            if (!string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExp = CustomCriteriaToLinqWhereParser.Process(CriteriaOperator.Parse(strFilterExpression) as CriteriaOperator);
            }
            var rekananCollection = _repNotaris.XLS_GetNotarisDetailAll(strFilterExp);
            return rekananCollection;
        }

    }
}
