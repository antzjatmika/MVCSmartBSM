using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstRegionAdminRep : IDataAccessRepository<mstRegionAdmin, Guid>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstRegionAdmin> Get()
        {
            return ctx.mstRegionAdmins.ToList();
        }
        //Get Specific Data based on Id
        public mstRegionAdmin Get(Guid id)
        {
            return ctx.mstRegionAdmins.Find(id);
        }
        //Create a new Data
        public void Post(mstRegionAdmin entity)
        {
            try
            {
                ctx.mstRegionAdmins.Add(entity);
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
        public void Put(Guid id, mstRegionAdmin entity)
        {
            var myData = ctx.mstRegionAdmins.Find(id);
            if (myData != null)
            {
                myData.IdSupervisor = entity.IdSupervisor;
                myData.RegionAdminCode = entity.RegionAdminCode;
                myData.RegionAdminDescription = entity.RegionAdminDescription;
                myData.CreatedDate = entity.CreatedDate;
                myData.CreatedUser = entity.CreatedUser;
                try
                {
                    ctx.SaveChanges();
                }
                catch(Exception ex)
                {
                    string strError = ex.Message;
                }
            }
        }
        //Delete Data based on Id
        public void Delete(Guid id)
        {
            var myData = ctx.mstRegionAdmins.Find(id);
            if (myData != null)
            {
                ctx.mstRegionAdmins.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}