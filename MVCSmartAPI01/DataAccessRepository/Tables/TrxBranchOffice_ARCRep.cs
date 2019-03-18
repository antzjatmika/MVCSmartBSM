using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxBranchOffice_ARCRep : IDataAccessRepository<trxBranchOffice_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxBranchOffice_ARC> Get()
        {
            return ctx.trxBranchOffice_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxBranchOffice_ARC Get(int id)
        {
            return ctx.trxBranchOffice_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxBranchOffice_ARC entity)
        {
            ctx.trxBranchOffice_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxBranchOffice_ARC entity)
        {
            var myData = ctx.trxBranchOffice_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdCabang = entity.IdCabang;
                myData.IdOrganisasi = entity.IdOrganisasi;
                myData.BranchType = entity.BranchType;
                myData.Name = entity.Name;
                myData.Address = entity.Address;
                myData.Phone = entity.Phone;
                myData.IdWilayah = entity.IdWilayah;
                myData.IdKecamatan = entity.IdKecamatan;
                myData.ZipCode = entity.ZipCode;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxBranchOffice_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxBranchOffice_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}