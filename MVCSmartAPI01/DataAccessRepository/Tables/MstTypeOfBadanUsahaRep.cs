using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstTypeOfBadanUsahaRep : IDataAccessRepository<mstTypeOfBadanUsaha, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstTypeOfBadanUsaha> Get()
        {
            return ctx.mstTypeOfBadanUsahas.ToList();
        }
        public IEnumerable<mstTypeOfBadanUsaha> GetActive()
        {
            return ctx.mstTypeOfBadanUsahas.ToList().Where(x => x.IsActive.Equals(true));
        }
        //Get Specific Data based on Id
        public mstTypeOfBadanUsaha Get(int id)
        {
            return ctx.mstTypeOfBadanUsahas.Find(id);
        }

        //Create a new Data
        public void Post(mstTypeOfBadanUsaha entity)
        {
            ctx.mstTypeOfBadanUsahas.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstTypeOfBadanUsaha entity)
        {
            var myData = ctx.mstTypeOfBadanUsahas.Find(id);
            if (myData != null)
            {
                myData.Name = entity.Name;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstTypeOfBadanUsahas.Find(id);
            if (myData != null)
            {
                ctx.mstTypeOfBadanUsahas.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}