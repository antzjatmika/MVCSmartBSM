using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxSanksiInternal_ARCRep : IDataAccessRepository<trxSanksiInternal_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxSanksiInternal_ARC> Get()
        {
            return ctx.trxSanksiInternal_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxSanksiInternal_ARC Get(int id)
        {
            return ctx.trxSanksiInternal_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxSanksiInternal_ARC entity)
        {
            ctx.trxSanksiInternal_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxSanksiInternal_ARC entity)
        {
            var myData = ctx.trxSanksiInternal_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdSanksiInternal = entity.IdSanksiInternal;
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
            var myData = ctx.trxSanksiInternal_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxSanksiInternal_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}