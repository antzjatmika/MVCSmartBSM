using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstSubRegionRep : IDataAccessRepository<mstSubRegion, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstSubRegion> Get()
        {
            return ctx.mstSubRegions.Where(x => x.IsActive.Equals(true)).ToList().OrderBy(x => x.SubDescription);
        }
        //Get Specific Data based on Id
        public mstSubRegion Get(int id)
        {
            return ctx.mstSubRegions.Find(id);
        }
        public IEnumerable<mstSubRegion> GetByRegion(Guid IdRegion)
        {
            return ctx.mstSubRegions.Where(x => x.IdRegionAdmin.Equals(IdRegion)).ToList();
        }

        //Create a new Data
        public void Post(mstSubRegion entity)
        {
            try
            {
                ctx.mstSubRegions.Add(entity);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
        //Update Exisiting Data
        public void Put(int id, mstSubRegion entity)
        {
            var myData = ctx.mstSubRegions.Find(id);
            if (myData != null)
            {
                myData.IdRegionAdmin = entity.IdRegionAdmin;
                myData.SubDescription = entity.SubDescription;
                myData.LongDescription = entity.LongDescription;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstSubRegions.Find(id);
            if (myData != null)
            {
                ctx.mstSubRegions.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}