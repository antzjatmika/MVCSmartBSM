using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDataOrganisasi_ARCRep : IDataAccessRepository<trxDataOrganisasi_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDataOrganisasi_ARC> Get()
        {
            return ctx.trxDataOrganisasi_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxDataOrganisasi_ARC Get(int id)
        {
            return ctx.trxDataOrganisasi_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxDataOrganisasi_ARC entity)
        {
            ctx.trxDataOrganisasi_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDataOrganisasi_ARC entity)
        {
            var myData = ctx.trxDataOrganisasi_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdOrganisasi = entity.IdOrganisasi;
                myData.IdRekanan = entity.IdRekanan;
                myData.OfficeStatus = entity.OfficeStatus;
                myData.NumberOfBranch = entity.NumberOfBranch;
                myData.NumberOfFixEmpl = entity.NumberOfFixEmpl;
                myData.NumberOfNonFixEmpl = entity.NumberOfNonFixEmpl;
                myData.NumberOfAgent = entity.NumberOfAgent;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxDataOrganisasi_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxDataOrganisasi_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}