using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxManagementRep : IDataAccessRepository<trxManagement, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxManagement> Get()
        {
            return ctx.trxManagement.ToList();
        }
        //Get Specific Data based on Id
        public trxManagement Get(int id)
        {
            return ctx.trxManagement.Find(id);
        }
        public trxManagement GetByKTP_NPWP(string nomorKTP, string nomorNPWP)
        {
            trxManagement trxResult = new trxManagement();
            trxResult = ctx.trxManagement.Where(x => x.NomorKTP.Equals(nomorKTP)).FirstOrDefault();
            if (trxResult == null)
            {
                trxResult = ctx.trxManagement.Where(x => x.NomorNPWP.Equals(nomorNPWP)).FirstOrDefault();
            }
            return trxResult;
        }
        public IEnumerable<trxManagement> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxManagement.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        public IEnumerable<trxManagement> GetByRekananActive(Guid idRekanan)
        {
            return ctx.trxManagement.Where(x => x.IdRekanan.Equals(idRekanan)).Where(x => x.IsActive.Equals(true)).ToList();
        }

        //Create a new Data
        public void Post(trxManagement entity)
        {
            try
            {
                ctx.trxManagement.Add(entity);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
        //Update Exisiting Data
        public void Put(int id, trxManagement entity)
        {
            var myData = ctx.trxManagement.Find(id);
            if (myData != null)
            {
                //myData.IdRekanan = entity.IdRekanan;
                myData.Name = entity.Name;
                myData.IsTTDLaporan = entity.IsTTDLaporan;
                myData.RoleTitle = entity.RoleTitle;
                myData.Alamat = entity.Alamat;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.NomorIAPI = entity.NomorIAPI;
                myData.NomorCV = entity.NomorCV;
                myData.NomorIjinPenilai = entity.NomorIjinPenilai;
                myData.TanggalLahir = entity.TanggalLahir;
                myData.AkhirBerlakuIAPI = entity.AkhirBerlakuIAPI;
                myData.AsalKantorCabang = entity.AsalKantorCabang;
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
                myData.FileExtIAPI = entity.FileExtIAPI;
                myData.FileExtCV = entity.FileExtCV;
                myData.FileExtIzin = entity.FileExtIzin;
                myData.FileExtGelar = entity.FileExtGelar;
                myData.IsActive = entity.IsActive;

                myData.TanggalMulaiBlackList = entity.TanggalMulaiBlackList;
                myData.TanggalAkhirBlacklist = entity.TanggalAkhirBlacklist;
                myData.StatusBlackList = entity.StatusBlackList;
                myData.Catatan = entity.Catatan;

                ctx.SaveChanges();
            }
        }

        public void PostPartner(trxManagement entity)
        {
            try
            {
                ctx.trxManagement.Add(entity);
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
        //Update Exisiting Data
        public void PutPartner(int id, trxManagement entity)
        {
            var myData = ctx.trxManagement.Find(id);
            if (myData != null)
            {
                myData.Name = entity.Name;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.NomorIAPI = entity.NomorIAPI;
                myData.NomorCV = entity.NomorCV;
                myData.ImageBaseName = entity.ImageBaseName;
                myData.FileExtKTP = entity.FileExtKTP;
                myData.FileExtNPWP = entity.FileExtNPWP;
                myData.FileExtIAPI = entity.FileExtIAPI;
                myData.FileExtCV = entity.FileExtCV;
                myData.FileExtIzin = entity.FileExtIzin;
                myData.FileExtGelar = entity.FileExtGelar;
                myData.TanggalMulaiBlackList = entity.TanggalMulaiBlackList;
                myData.TanggalAkhirBlacklist = entity.TanggalAkhirBlacklist;
                myData.StatusBlackList = entity.StatusBlackList;
                myData.Catatan = entity.Catatan;

                ctx.SaveChanges();
            }
        }
        
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxManagement.Find(id);
            if (myData != null)
            {
                ctx.trxManagement.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public IEnumerable<vwRekManagemenBL> GetRekManagemenBL()
        {
            return ctx.vwRekManagemenBL.ToList();
        }
        public IEnumerable<trxManagement> GetManagemenBL()
        {
            return ctx.trxManagement.Where(x => x.IsActive.Equals(true)).ToList();
        }
    }
}