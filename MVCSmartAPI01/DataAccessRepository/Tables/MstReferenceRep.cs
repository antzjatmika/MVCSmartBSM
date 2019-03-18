using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstReferenceRep : IDataAccessRepository<mstReference, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstReference> Get()
        {
            return ctx.mstReferences.ToList();
        }
        public IEnumerable<mstReference> GetByType(string RefType)
        {
            //var refTypeColls = new List<mstReference>();
            //refTypeColls.Add(new mstReference() { 
            //    IdRef = 0,
            //    RefType = RefType,
            //    RefCode = "0",
            //    RefDesc = "0",
            //    CreatedUser = "admin",
            //    CreatedDate = DateTime.Now,
            //    IsActive = true
            //});
            //refTypeColls.AddRange(ctx.mstReferences.Where(x => x.RefType == RefType).ToList());
            //return refTypeColls;
            return ctx.mstReferences.Where(x => x.RefType == RefType) .ToList();
        }
        //Get Specific Data based on Id
        public mstReference Get(int id)
        {
            return ctx.mstReferences.Find(id);
        }

        //Create a new Data
        public void Post(mstReference entity)
        {
            ctx.mstReferences.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstReference entity)
        {
            var myData = ctx.mstReferences.Find(id);
            if (myData != null)
            {
                //myData.RefType = entity.RefType;
                myData.RefCode = entity.RefCode;
                myData.RefDesc = entity.RefDesc;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstReferences.Find(id);
            if (myData != null)
            {
                ctx.mstReferences.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}