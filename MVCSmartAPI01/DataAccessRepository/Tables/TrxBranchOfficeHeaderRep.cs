using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxBranchOfficeHeaderRep : IDataAccessRepository<trxBranchOfficeHeader, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxBranchOfficeHeader> Get()
        {
            return ctx.trxBranchOfficeHeaders.ToList();
        }
        //Get Specific Data based on Id
        public trxBranchOfficeHeader Get(int id)
        {
            return ctx.trxBranchOfficeHeaders.Find(id);
        }
        public IEnumerable<trxBranchOfficeHeader> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxBranchOfficeHeaders.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        public trxBranchOfficeHeader GetByGuidHeader(Guid guidHeader)
        {
            trxBranchOfficeHeader myData = new trxBranchOfficeHeader();
            if (guidHeader != Guid.Empty)
            {
                myData = ctx.trxBranchOfficeHeaders.Where(x => x.GuidHeader.Equals(guidHeader)).First();
            }
            return myData;
        }
        //Create a new Data
        public void Post(trxBranchOfficeHeader entity)
        {
            ctx.trxBranchOfficeHeaders.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxBranchOfficeHeader entity)
        {
            var myData = ctx.trxBranchOfficeHeaders.Find(id);
            if (myData != null)
            {
                myData.GuidHeader = entity.GuidHeader;
                myData.IdTipeBranch = entity.IdTipeBranch;
                myData.IdRekanan = entity.IdRekanan;
                myData.JudulDokumen = entity.JudulDokumen;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxBranchOfficeHeaders.Find(id);
            if (myData != null)
            {
                ctx.trxBranchOfficeHeaders.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}