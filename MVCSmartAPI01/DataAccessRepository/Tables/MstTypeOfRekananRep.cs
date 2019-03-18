using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstTypeOfRekananRep : IDataAccessRepository<mstTypeOfRekanan, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstTypeOfRekanan> Get()
        {
            return ctx.mstTypeOfRekanans.ToList();
        }
        public IEnumerable<mstTypeOfRekanan> GetActive()
        {
            return ctx.mstTypeOfRekanans.ToList().Where(x => x.IsActive.Equals(true));
        }
        public IEnumerable<mstTypeOfRekanan> GetActiveRekanan()
        {
            return ctx.mstTypeOfRekanans.ToList().Where(x => x.IsActive.Equals(true) && x.IdTypeOfRekanan <= 7);
        }
        //Get Specific Data based on Id
        public mstTypeOfRekanan Get(int id)
        {
            return ctx.mstTypeOfRekanans.Find(id);
        }

        //Create a new Data
        public void Post(mstTypeOfRekanan entity)
        {
            ctx.mstTypeOfRekanans.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstTypeOfRekanan entity)
        {
            var myData = ctx.mstTypeOfRekanans.Find(id);
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
            var myData = ctx.mstTypeOfRekanans.Find(id);
            if (myData != null)
            {
                ctx.mstTypeOfRekanans.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}