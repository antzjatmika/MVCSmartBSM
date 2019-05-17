using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxRegistrationRequestRep : IDataAccessRepository<trxRegistrationRequest, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxRegistrationRequest> Get()
        {
            return ctx.trxRegistrationRequest.ToList();
        }
        public IEnumerable<vwRegistrationRequest> GetView()
        {
            return ctx.vwRegistrationRequest.ToList();
        }
        public IEnumerable<vwRegistrationRequest> GetViewIsActive(Byte IsAcive)
        {
            return ctx.vwRegistrationRequest.Where(x => x.IsActive.Equals(IsAcive)).ToList();
        }
        public IEnumerable<vwUserAdminPCP> GetViewUserAdmin()
        {
            return ctx.vwUserAdminPCP.ToList();
        }
        public void SetPasswordByUserName(string myUserName, string myBarePassword)
        {
            trxRegistrationRequest myData = ctx.trxRegistrationRequest.Where(x => x.UserName.Equals(myUserName)).First();
            if (myData != null)
            {
                myData.UserPassKey = myBarePassword;
                ctx.SaveChanges();
            }
        }
        //Get Specific Data based on Id
        public trxRegistrationRequest Get(int id)
        {
            return ctx.trxRegistrationRequest.Find(id);
        }

        //Create a new Data
        public void Post(trxRegistrationRequest entity)
        {
            ctx.trxRegistrationRequest.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxRegistrationRequest entity)
        {
            var myData = ctx.trxRegistrationRequest.Find(id);
            if (myData != null)
            {
                myData.NamaLengkap = entity.NamaLengkap;
                myData.AlamatLengkap = entity.AlamatLengkap;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxRegistrationRequest.Find(id);
            if (myData != null)
            {
                ctx.trxRegistrationRequest.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}