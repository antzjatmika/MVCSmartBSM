using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstRekanan_ARCRep : IDataAccessRepository<mstRekanan_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstRekanan_ARC> Get()
        {
            return ctx.mstRekanan_ARC.ToList();
        }
        //Get Specific Data based on Id
        public mstRekanan_ARC Get(int id)
        {
            return ctx.mstRekanan_ARC.Find(id);
        }

        //Create a new Data
        public void Post(mstRekanan_ARC entity)
        {
            ctx.mstRekanan_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, mstRekanan_ARC entity)
        {
            var myData = ctx.mstRekanan_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdRekanan = entity.IdRekanan;
                myData.IdRegion = entity.IdRegion;
                myData.RegistrationNumber = entity.RegistrationNumber;
                myData.ClassOfRekanan = entity.ClassOfRekanan;
                myData.IdTypeOfRekanan = entity.IdTypeOfRekanan;
                myData.IdTypeOfBadanUsaha = entity.IdTypeOfBadanUsaha;
                myData.Name = entity.Name;
                myData.Address = entity.Address;
                myData.IdWilayah = entity.IdWilayah;
                myData.IdKecamatan = entity.IdKecamatan;
                myData.ZipCode = entity.ZipCode;
                myData.Phone1 = entity.Phone1;
                myData.Phone2 = entity.Phone2;
                myData.EstablishedDate = entity.EstablishedDate;
                myData.EstablishedPlace = entity.EstablishedPlace;
                myData.Fax1 = entity.Fax1;
                myData.Fax2 = entity.Fax2;
                myData.EmailAddress = entity.EmailAddress;
                myData.NomorKTP = entity.NomorKTP;
                myData.NomorNPWP = entity.NomorNPWP;
                myData.IsConsultantManagemen = entity.IsConsultantManagemen;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstRekanan_ARC.Find(id);
            if (myData != null)
            {
                ctx.mstRekanan_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}