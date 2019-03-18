using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstContact_ARCRep : IDataAccessRepository<mstContact_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstContact_ARC> Get()
        {
            return ctx.mstContact_ARC.ToList();
        }
        //Get Specific Data based on Id
        public mstContact_ARC Get(int id)
        {
            return ctx.mstContact_ARC.Find(id);
        }

        //Create a new Data
        public void Post(mstContact_ARC entity)
        {
            ctx.mstContact_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstContact_ARC entity)
        {
            var myData = ctx.mstContact_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdContact = entity.IdContact;
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.Title = entity.Title;
                myData.Phone = entity.Phone;
                myData.CellPhone = entity.CellPhone;
                myData.EmailAddress = entity.EmailAddress;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstContact_ARC.Find(id);
            if (myData != null)
            {
                ctx.mstContact_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}