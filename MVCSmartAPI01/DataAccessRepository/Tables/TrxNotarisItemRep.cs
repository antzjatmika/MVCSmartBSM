using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class trxNotarisItemRep : IDataAccessRepository<trxNotarisItem, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxNotarisItem> Get()
        {
            return ctx.trxNotarisItem.ToList();
        }
        //Get Specific Data based on Id
        public trxNotarisItem Get(int id)
        {
            return ctx.trxNotarisItem.Find(id);
        }
        public IEnumerable<trxNotarisItem> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxNotarisItem.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        //Create a new Data
        public void Post(trxNotarisItem entity)
        {
            ctx.trxNotarisItem.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxNotarisItem entity)
        {
            var myData = ctx.trxNotarisItem.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.RoleTitle = entity.RoleTitle;
                myData.Alamat = entity.Alamat;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.TanggalLahir = entity.TanggalLahir;
                myData.Email1 = entity.Email1;
                myData.Email2 = entity.Email2;
                myData.Handphone1 = entity.Handphone1;
                myData.Handphone2 = entity.Handphone2;
                myData.Telephone1 = entity.Telephone1;
                myData.Telephone2 = entity.Telephone2;
                myData.Telephone3 = entity.Telephone3;
                myData.Fax1 = entity.Fax1;
                myData.Fax2 = entity.Fax2;
                myData.ImageBaseName = entity.ImageBaseName;
                myData.FileExtKTP = entity.FileExtKTP;
                myData.FileExtNPWP = entity.FileExtNPWP;
                myData.IsActive = entity.IsActive;

                myData.TanggalMulaiBlacklist = entity.TanggalMulaiBlacklist;
                myData.TanggalAkhirBlacklist = entity.TanggalAkhirBlacklist;
                myData.StatusBlackList = entity.StatusBlackList;
                myData.Catatan = entity.Catatan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxNotarisItem.Find(id);
            if (myData != null)
            {
                ctx.trxNotarisItem.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}