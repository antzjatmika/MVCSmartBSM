using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxFileDataPekerjaanRep : IDataAccessRepository<trxFileDataPekerjaan, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxFileDataPekerjaan> Get()
        {
            return ctx.trxFileDataPekerjaans.ToList();
        }
        //Get Specific Data based on Id
        public trxFileDataPekerjaan Get(int id)
        {
            return ctx.trxFileDataPekerjaans.Find(id);
        }

        //Create a new Data
        public void Post(trxFileDataPekerjaan entity)
        {
            ctx.trxFileDataPekerjaans.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxFileDataPekerjaan entity)
        {
            var myData = ctx.trxFileDataPekerjaans.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.FileName = entity.FileName;
                myData.BlobPekerjaan = entity.BlobPekerjaan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxFileDataPekerjaans.Find(id);
            if (myData != null)
            {
                ctx.trxFileDataPekerjaans.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}