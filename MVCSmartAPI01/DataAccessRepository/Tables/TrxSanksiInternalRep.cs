using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxSanksiInternalRep : IDataAccessRepository<trxSanksiInternal, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxSanksiInternal> Get()
        {
            return ctx.trxSanksiInternals.ToList();
        }
        //Get Specific Data based on Id
        public trxSanksiInternal Get(int id)
        {
            return ctx.trxSanksiInternals.Find(id);
        }

        //Create a new Data
        public void Post(trxSanksiInternal entity)
        {
            ctx.trxSanksiInternals.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxSanksiInternal entity)
        {
            var myData = ctx.trxSanksiInternals.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.SanksiType = entity.SanksiType;
                myData.Pelanggaran = entity.Pelanggaran;
                myData.LetterNumber = entity.LetterNumber;
                myData.LetterDate = entity.LetterDate;
                myData.Period = entity.Period;
                myData.SanksiStatus = entity.SanksiStatus;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxSanksiInternals.Find(id);
            if (myData != null)
            {
                ctx.trxSanksiInternals.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}