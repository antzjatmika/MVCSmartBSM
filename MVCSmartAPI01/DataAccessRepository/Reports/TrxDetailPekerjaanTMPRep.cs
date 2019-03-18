using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanTMPRep : IDataAccessRepository<trxDetailPekerjaanTMP, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaanTMP> Get()
        {
            return ctx.trxDetailPekerjaanTMPs.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaanTMP Get(int id)
        {
            return ctx.trxDetailPekerjaanTMPs.Find(id);
        }
        public IEnumerable<trxDetailPekerjaanTMP> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaanTMPs.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public IEnumerable<trxDetailPekerjaanTMP> GetByGuidHeader(Guid GuidHeader)
        {
            return ctx.trxDetailPekerjaanTMPs.Where(x => x.GuidHeader.Equals(GuidHeader)).ToList();
        }
        public void Post(trxDetailPekerjaanTMP entity)
        {
            ctx.trxDetailPekerjaanTMPs.Add(entity);
            ctx.SaveChanges();
        }
        public void Put(int id, trxDetailPekerjaanTMP entity)
        {
            var myData = ctx.trxDetailPekerjaans.Find(id);
            if (myData != null)
            {
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaanTMPs.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaanTMPs.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}