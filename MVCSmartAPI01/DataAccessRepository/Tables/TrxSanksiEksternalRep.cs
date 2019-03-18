using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxSanksiEksternalRep : IDataAccessRepository<trxSanksiEksternal, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxSanksiEksternal> Get()
        {
            return ctx.trxSanksiEksternals.ToList();
        }
        //Get Specific Data based on Id
        public trxSanksiEksternal Get(int id)
        {
            return ctx.trxSanksiEksternals.Find(id);
        }

        //Create a new Data
        public void Post(trxSanksiEksternal entity)
        {
            ctx.trxSanksiEksternals.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxSanksiEksternal entity)
        {
            var myData = ctx.trxSanksiEksternals.Find(id);
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
            var myData = ctx.trxSanksiEksternals.Find(id);
            if (myData != null)
            {
                ctx.trxSanksiEksternals.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}