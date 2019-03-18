using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDetailPekerjaanHeaderRep : IDataAccessRepository<trxDetailPekerjaanHeader, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDetailPekerjaanHeader> Get()
        {
            return ctx.trxDetailPekerjaanHeaders.ToList();
        }
        //Get Specific Data based on Id
        public trxDetailPekerjaanHeader Get(int id)
        {
            return ctx.trxDetailPekerjaanHeaders.Find(id);
        }
        public IEnumerable<trxDetailPekerjaanHeader> GetByRekanan(Guid IdRekanan)
        {
            return ctx.trxDetailPekerjaanHeaders.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        public trxDetailPekerjaanHeader GetByGuidHeader(Guid GuidHeader)
        {
            return ctx.trxDetailPekerjaanHeaders.Where(x => x.GuidHeader.Equals(GuidHeader)).First();
        }
        //Create a new Data
        public void Post(trxDetailPekerjaanHeader entity)
        {
            ctx.trxDetailPekerjaanHeaders.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDetailPekerjaanHeader entity)
        {
            var myData = ctx.trxDetailPekerjaanHeaders.Find(id);
            if (myData != null)
            {
                myData.GuidHeader = entity.GuidHeader;
                myData.IdTipePekerjaan = entity.IdTipePekerjaan;
                myData.IdRekanan = entity.IdRekanan;
                myData.JudulDokumen = entity.JudulDokumen;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDetailPekerjaanHeaders.Find(id);
            if (myData != null)
            {
                ctx.trxDetailPekerjaanHeaders.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}