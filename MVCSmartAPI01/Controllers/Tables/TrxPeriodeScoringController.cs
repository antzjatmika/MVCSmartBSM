using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Data;
using System.Data.SqlClient;

namespace APIService.Controllers
{
    public class TrxPeriodeScoringController : ApiController
    {
        private IDataAccessRepository<trxPeriodeScoring, int> _repository;
        private TrxPeriodeScoringRep _repAsisten = new TrxPeriodeScoringRep();
        //Inject the DataAccessRepository using Construction Injection 
        public TrxPeriodeScoringController(IDataAccessRepository<trxPeriodeScoring, int> r, TrxPeriodeScoringRep repAsisten)
        {
            _repository = r;
            _repAsisten = repAsisten;
        }
        public IEnumerable<trxPeriodeScoring> Get()
        {
            return _repository.Get();
        }

        [ResponseType(typeof(trxPeriodeScoring))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.Get(id)); 
        }

        [ResponseType(typeof(trxPeriodeScoring))]
        public IHttpActionResult Post(trxPeriodeScoring myData)
        {
            try
            {
                using (var context = new DB_SMARTEntities1())
                {
                    SqlParameter[] @params = 
                    {
                       new SqlParameter("@IdRekanan", SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = myData.IdRekanan},
                       new SqlParameter("@TahunBulan", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = myData.TahunBulan}
                     };
                    context.Database.ExecuteSqlCommand("EXEC spPopulatePertanyaanNilai @IdRekanan, @TahunBulan", @params);
                }
            }
            catch (Exception ex)
            {
            }
            return Ok(myData);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, trxPeriodeScoring myData)
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
        [Route("api/trxPeriodeScoring/GetByRekanan/{idRekanan}")]
        public IEnumerable<fPeriodeScoringByRekanan_Result> GetByRekanan(System.Guid idRekanan)
        {
            IEnumerable<fPeriodeScoringByRekanan_Result> ContactsRekanan;
            ContactsRekanan = _repAsisten.GetPeriodeScoringByRekanan(idRekanan);
            return ContactsRekanan;
        }
    }
}
