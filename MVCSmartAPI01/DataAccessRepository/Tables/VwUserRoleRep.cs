using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class VwUserRoleRep : IDataAccessRepository<vwUserRole, string>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<vwUserRole> Get()
        {
            return ctx.vwUserRoles.ToList();
        }
        //Get Specific Data based on Id
        public vwUserRole Get(string id)
        {
            return ctx.vwUserRoles.Find(id);
        }
        public IEnumerable<vwUserRole> GetUserRoleByEmail(string id)
        {
            return ctx.vwUserRoles.ToList().Where(x => x.Email.Equals(id));
        }
        public IEnumerable<vwUserRole> GetUserRoleByUserName(string id)
        {
            string strErr = "AWAL";
            IEnumerable<vwUserRole> myDataList = new List<vwUserRole>();
            try
            {
                myDataList = ctx.vwUserRoles.ToList().Where(x => x.UserName.Equals(id));
            }
            catch(Exception ex)
            {
                strErr = ex.Message;
            }
            return myDataList;
        }

        public void Post(vwUserRole entity)
        {
            throw new NotImplementedException();
        }
        //Update Existing Data
        public void Put(string id, vwUserRole entity)
        {
            throw new NotImplementedException();
        }
        //Delete Data based on Id
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

    }
}