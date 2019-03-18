using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstWilayahRep : IDataAccessRepository<mstWilayah, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstWilayah> Get()
        {
            return ctx.mstWilayahs.ToList();
        }
        public IEnumerable<mstWilayah> GetActive()
        {
            return ctx.mstWilayahs.ToList().Where(x => x.IsActive.Equals(true));
        }
        //Get Specific Data based on Id
        public mstWilayah Get(int id)
        {
            return ctx.mstWilayahs.Find(id);
        }

        //Create a new Data
        public void Post(mstWilayah entity)
        {
            ctx.mstWilayahs.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstWilayah entity)
        {
            var myData = ctx.mstWilayahs.Find(id);
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
            var myData = ctx.mstWilayahs.Find(id);
            if (myData != null)
            {
                ctx.mstWilayahs.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}