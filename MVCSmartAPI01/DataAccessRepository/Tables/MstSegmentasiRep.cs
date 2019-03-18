using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstSegmentasiRep : IDataAccessRepository<mstSegmentasi, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstSegmentasi> Get()
        {
            return ctx.mstSegmentasis.ToList();
        }
        public IEnumerable<mstSegmentasi> GetActive()
        {
            return ctx.mstSegmentasis.ToList().Where(x => x.IsActive.Equals(true));
        }
        //Get Specific Data based on Id
        public IEnumerable<mstSegmentasi> SegmenForKAP()
        {
            string[] KAP_Seg = { "COR", "COM", "SME" };
            return ctx.mstSegmentasis.Where(x => x.IsActive.Equals(true) && KAP_Seg.Contains(x.KodeSegmentasi)).ToList();
        }
        public mstSegmentasi Get(int id)
        {
            return ctx.mstSegmentasis.Find(id);
        }

        //Create a new Data
        public void Post(mstSegmentasi entity)
        {
            ctx.mstSegmentasis.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstSegmentasi entity)
        {
            var myData = ctx.mstSegmentasis.Find(id);
            if (myData != null)
            {
                myData.KodeSegmentasi = entity.KodeSegmentasi;
                myData.NamaSegmentasi = entity.NamaSegmentasi;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
            else
            {
                myData.CreatedUser = "admin";
                myData.CreatedDate = DateTime.Today;
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstSegmentasis.Find(id);
            if (myData != null)
            {
                ctx.mstSegmentasis.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}