using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstTypeOfRegionRep : IDataAccessRepository<mstTypeOfRegion, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstTypeOfRegion> Get()
        {
            return ctx.mstTypeOfRegions.ToList();
        }
        public IEnumerable<mstTypeOfRegion> GetActive()
        {
            return ctx.mstTypeOfRegions.ToList().Where(x => x.IsActive.Equals(true));
        }
        //Get Specific Data based on Id
        public mstTypeOfRegion Get(int id)
        {
            return ctx.mstTypeOfRegions.Find(id);
        }

        //Create a new Data
        public void Post(mstTypeOfRegion entity)
        {
            ctx.mstTypeOfRegions.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstTypeOfRegion entity)
        {
            var myData = ctx.mstTypeOfRegions.Find(id);
            if (myData != null)
            {
                myData.IdSupervisor = entity.IdSupervisor;
                myData.RegionDescription = entity.RegionDescription;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstTypeOfRegions.Find(id);
            if (myData != null)
            {
                ctx.mstTypeOfRegions.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}