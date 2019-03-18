using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using Omu.ValueInjecter;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxDataOrganisasiRep : IDataAccessRepository<trxDataOrganisasi, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxDataOrganisasi> Get()
        {
            return ctx.trxDataOrganisasis.ToList();
        }
        //Get Specific Data based on Id
        public trxDataOrganisasi Get(int id)
        {
            return ctx.trxDataOrganisasis.Find(id);
        }

        //Create a new Data
        public void Post(trxDataOrganisasi entity)
        {
            ctx.trxDataOrganisasis.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxDataOrganisasi entity)
        {
            var myData = ctx.trxDataOrganisasis.Find(id);
            if (myData != null)
            {
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
            var myData = ctx.trxDataOrganisasis.Find(id);
            if (myData != null)
            {
                ctx.trxDataOrganisasis.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public trxDataOrganisasiForm GetByRekananId(Guid rekananId)
        {
            trxDataOrganisasiForm NotarisSingle = new trxDataOrganisasiForm();
            trxDataOrganisasi notTemp = new trxDataOrganisasi();
            notTemp = (trxDataOrganisasi)ctx.trxDataOrganisasis.First(a => a.IdRekanan == rekananId);
            NotarisSingle.InjectFrom(notTemp);
            return NotarisSingle;
        }

        public List<fDataOrganisasiByRek_Result> GetDataOrganisasiByRek(System.Guid IdRekanan)
        {
            List<fDataOrganisasiByRek_Result> myDataList = ctx.fDataOrganisasiByRek(IdRekanan).ToList();
            return myDataList;
        }

        public List<fDataOrganisasiByRek_Type_Result> GetDataOrganisasiByRek_Type(System.Guid IdRekanan, int IdTypeRekanan)
        {
            List<fDataOrganisasiByRek_Type_Result> myDataList = ctx.fDataOrganisasiByRek_Type(IdRekanan, IdTypeRekanan).ToList();
            return myDataList;
        }
    }
}