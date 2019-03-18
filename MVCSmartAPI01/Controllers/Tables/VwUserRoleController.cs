using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MVCSmartAPI01.Models;
using MVCSmartAPI01.DataAccessRepository;
using System.Web.Http.Description;
using System.Text.RegularExpressions;

namespace APIService.Controllers
{
    [RoutePrefix("api/VwUserRole")]
    public class VwUserRoleController : ApiController
    {
        private IDataAccessRepository<vwUserRole, string> _repository;
        private VwUserRoleRep _rUserRole;
        //Inject the DataAccessRepository using Construction Injection 
        public VwUserRoleController(IDataAccessRepository<vwUserRole, string> r, VwUserRoleRep rUserRole)
        {
            _repository = r;
            _rUserRole = rUserRole;
        }
        public IEnumerable<vwUserRole> Get()
        {
            return _repository.Get();
        }
        public IEnumerable<vwUserRole> Get(string id)
        {
            //string statusnya = "KOSONG";
            //if (_rUserRole != null) statusnya = "ISI DAH";
            //List<vwUserRole> dummy = new List<vwUserRole>();
            //dummy.Add(new vwUserRole()
            //    {
            //        Email = statusnya,
            //        id = "zz",
            //        IdNotaris = 1,
            //        IdOrganisasi = 2,
            //        IdRekananContact = new System.Guid(),
            //        IdSupervisor = 1,
            //        IdTypeOfRekanan = 1,
            //        Name = "nama",
            //        RoleId = "role",
            //        UserName = "apa"
            //    });
            //return _rUserRole.GetUserRoleByEmail(id);
            return _rUserRole.GetUserRoleByUserName(id);
        }
    }
}
