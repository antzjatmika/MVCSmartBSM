using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxOwnershipRep : IDataAccessRepository<trxOwnership, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxOwnership> Get()
        {
            return ctx.trxOwnerships.ToList();
        }
        //Get Specific Data based on Id
        public trxOwnership Get(int id)
        {
            return ctx.trxOwnerships.Find(id);
        }
        public IEnumerable<trxOwnership> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxOwnerships.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxOwnership entity)
        {
            ctx.trxOwnerships.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxOwnership entity)
        {
            var myData = ctx.trxOwnerships.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.Title = entity.Title;
                myData.Alamat = entity.Alamat;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.Email1 = entity.Email1;
                myData.Email2 = entity.Email2;
                myData.Handphone1 = entity.Handphone1;
                myData.Handphone2 = entity.Handphone2;
                myData.Telephone1 = entity.Telephone1;
                myData.Telephone2 = entity.Telephone2;
                myData.Telephone3 = entity.Telephone3;
                myData.Fax1 = entity.Fax1;
                myData.Fax2 = entity.Fax2;
                myData.Sertifikasi = entity.Sertifikasi;
                myData.PercentSaham = entity.PercentSaham;
                myData.NominalSaham = entity.NominalSaham;
                myData.IsKeyPerson = entity.IsKeyPerson;
                myData.NPWP = entity.NPWP;
                myData.NamaPerusahaan1 = entity.NamaPerusahaan1;
                myData.NamaPerusahaan2 = entity.NamaPerusahaan2;
                myData.NamaPerusahaan3 = entity.NamaPerusahaan3;
                myData.Persen1 = entity.Persen1;
                myData.Persen2 = entity.Persen2;
                myData.Persen3 = entity.Persen3;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxOwnerships.Find(id);
            if (myData != null)
            {
                ctx.trxOwnerships.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}