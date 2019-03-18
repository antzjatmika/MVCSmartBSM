using System;
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
                myData.AlamatEmail = entity.AlamatEmail;
                myData.IdTypeOfRekanan = entity.IdTypeOfRekanan;
                myData.ImageBaseName = entity.ImageBaseName;
                myData.UserName = entity.UserName;
                myData.UserPassKey = entity.UserPassKey;

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