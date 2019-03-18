using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanAS_1MTMPRep : IDataAccessRepository<trxDetailPekerjaanAS_1MTMP, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaanAS_1MTMP> Get()
        {
            return ctx.trxDetailPekerjaanAS_1MTMP.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaanAS_1MTMP Get(int id)
        {
            return ctx.trxDetailPekerjaanAS_1MTMP.Find(id);
        }
        public IEnumerable<trxDetailPekerjaanAS_1MTMP> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaanAS_1MTMP.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public IEnumerable<trxDetailPekerjaanAS_1MTMP> GetByGuidHeader(Guid GuidHeader)
        {
            return ctx.trxDetailPekerjaanAS_1MTMP.Where(x => x.GuidHeader.Equals(GuidHeader)).ToList();
        }
        public void Post(trxDetailPekerjaanAS_1MTMP entity)
        {
            ctx.trxDetailPekerjaanAS_1MTMP.Add(entity);
            ctx.SaveChanges();
        }
        public void Put(int id, trxDetailPekerjaanAS_1MTMP entity)
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
            var myData = ctx.trxDetailPekerjaanAS_1MTMP.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaanAS_1MTMP.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}