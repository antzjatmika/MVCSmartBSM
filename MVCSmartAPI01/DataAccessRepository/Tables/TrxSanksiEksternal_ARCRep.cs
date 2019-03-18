using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxSanksiEksternal_ARCRep : IDataAccessRepository<trxSanksiEksternal_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxSanksiEksternal_ARC> Get()
        {
            return ctx.trxSanksiEksternal_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxSanksiEksternal_ARC Get(int id)
        {
            return ctx.trxSanksiEksternal_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxSanksiEksternal_ARC entity)
        {
            ctx.trxSanksiEksternal_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxSanksiEksternal_ARC entity)
        {
            var myData = ctx.trxSanksiEksternal_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdSanksiEksternal = entity.IdSanksiEksternal;
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
            var myData = ctx.trxSanksiEksternal_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxSanksiEksternal_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}