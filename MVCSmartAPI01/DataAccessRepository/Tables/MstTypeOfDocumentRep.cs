using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstTypeOfDocumentRep : IDataAccessRepository<mstTypeOfDocument, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstTypeOfDocument> Get()
        {
            return ctx.mstTypeOfDocuments.ToList();
        }
        //Get Specific Data based on Id
        public mstTypeOfDocument Get(int id)
        {
            return ctx.mstTypeOfDocuments.Find(id);
        }
        public IEnumerable<mstTypeOfDocument> GetActive()
        {
            return ctx.mstTypeOfDocuments.ToList().Where(x => x.IsActive.Equals(true));
        }
        //Create a new Data
        public void Post(mstTypeOfDocument entity)
        {
            ctx.mstTypeOfDocuments.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstTypeOfDocument entity)
        {
            var myData = ctx.mstTypeOfDocuments.Find(id);
            if (myData != null)
            {
                myData.NameType = entity.NameType;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstTypeOfDocuments.Find(id);
            if (myData != null)
            {
                ctx.mstTypeOfDocuments.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}