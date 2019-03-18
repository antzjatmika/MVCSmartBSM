using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstKecamatanRep : IDataAccessRepository<mstKecamatan, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstKecamatan> Get()
        {
            return ctx.mstKecamatans.ToList();
        }
        public IEnumerable<mstKecamatan> GetActive()
        {
            return ctx.mstKecamatans.ToList().Where(x => x.IsActive.Equals(true));
        }
        //Get Specific Data based on Id
        public mstKecamatan Get(int id)
        {
            return ctx.mstKecamatans.Find(id);
        }

        //Create a new Data
        public void Post(mstKecamatan entity)
        {
            ctx.mstKecamatans.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstKecamatan entity)
        {
            var myData = ctx.mstKecamatans.Find(id);
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
            var myData = ctx.mstKecamatans.Find(id);
            if (myData != null)
            {
                ctx.mstKecamatans.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}