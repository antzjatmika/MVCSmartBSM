using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using Omu.ValueInjecter;

namespace APIService.Controllers
{
    public class MstSubRegionController : ApiController
    {
        private IDataAccessRepository<mstSubRegion, int> _repository;
        private MstSubRegionRep _repSubRegion = new MstSubRegionRep();
        private MstRegionAdminRep _repRegion = new MstRegionAdminRep();
        //Inject the DataAccessRepository using Construction Injection 
        public MstSubRegionController(IDataAccessRepository<mstSubRegion, int> r, MstSubRegionRep repSubRegion, MstRegionAdminRep repRegion)
        {
            _repository = r;
            _repSubRegion = repSubRegion;
            _repRegion = repRegion;
        }
        public mstSubRegionMulti Get()
        {
            mstSubRegionMulti myDataMulti = new mstSubRegionMulti();
            myDataMulti.SubRegionColls = _repository.Get();
            myDataMulti.RegionColls = _repRegion.Get();
            return myDataMulti;
        }

        [ResponseType(typeof(mstSubRegion))]
        public IHttpActionResult Get(int id)
        {
            mstSubRegionForm myDataForm = new mstSubRegionForm();
            mstSubRegion myData = new mstSubRegion();
            myData = _repository.Get(id);
            if(myData != null)
            {
                myDataForm.InjectFrom(myData);
                myDataForm.RegionColls = _repRegion.Get();
            }
            else
            {
                myDataForm.RegionColls = _repRegion.Get();
            }
            return Ok(myDataForm); 
        }

        [ResponseType(typeof(mstSubRegion))]
        public IHttpActionResult Post(mstSubRegion myData)
        {
            _repository.Post(myData);
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, mstSubRegion myData)
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
        [Route("api/MstSubRegion/GetByRegion/{idRegion}")]
        public IEnumerable<mstSubRegion> GetByRegion(Guid idRegion)
        {
            IEnumerable<mstSubRegion> SubRegionList;
            SubRegionList = _repSubRegion.GetByRegion(idRegion);
            return SubRegionList;
        }
    }
}
