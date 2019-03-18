using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class trxBranchOfficeTMPRep : IDataAccessRepository<trxBranchOfficeTMP, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxBranchOfficeTMP> Get()
        {
            return ctx.trxBranchOfficeTMPs.ToList();
        }
        //Get Specific Data based on Id
        public trxBranchOfficeTMP Get(int id)
        {
            return ctx.trxBranchOfficeTMPs.Find(id);
        }
        public IEnumerable<trxBranchOfficeTMP> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxBranchOfficeTMPs.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        public IEnumerable<trxBranchOfficeTMP> GetByGuidHeader(Guid guidHeader)
        {
            return ctx.trxBranchOfficeTMPs.Where(x => x.GuidHeader.Equals(guidHeader)).ToList();
        }
        public IEnumerable<trxBranchOfficeTMP> GetByIdOrganisasi(int idOrganisasi)
        {
            return ctx.trxBranchOfficeTMPs.Where(x => x.IdOrganisasi.Equals(idOrganisasi)).ToList();
        }
        //Create a new Data
        public void Post(trxBranchOfficeTMP entity)
        {
            ctx.trxBranchOfficeTMPs.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxBranchOfficeTMP entity)
        {
            var myData = ctx.trxBranchOfficeTMPs.Find(id);
            if (myData != null)
            {
                myData.IdCabang = entity.IdCabang;
                myData.IdOrganisasi = entity.IdOrganisasi;
                myData.BranchType = entity.BranchType;
                myData.Name = entity.Name;
                myData.Address = entity.Address;
                myData.Email1 = entity.Email1;
                myData.Email2 = entity.Email2;
                myData.Telephone1 = entity.Telephone1;
                myData.Telephone2 = entity.Telephone2;
                myData.Telephone3 = entity.Telephone3;
                myData.Fax1 = entity.Fax1;
                myData.Fax2 = entity.Fax2;
                myData.Kelurahan = entity.Kelurahan;
                myData.Kecamatan = entity.Kecamatan;
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
            var myData = ctx.trxBranchOfficeTMPs.Find(id);
            if (myData != null)
            {
                ctx.trxBranchOfficeTMPs.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}