using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxBranchOfficeRep : IDataAccessRepository<trxBranchOffice, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxBranchOffice> Get()
        {
            return ctx.trxBranchOffices.ToList();
        }
        //Get Specific Data based on Id
        public trxBranchOffice Get(int id)
        {
            return ctx.trxBranchOffices.Find(id);
        }
        public IEnumerable<trxBranchOffice> GetByRekanan(Guid idRekanan)
        {
            return ctx.trxBranchOffices.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
        }
        public IEnumerable<trxBranchOffice> GetByGuidHeader(Guid guidHeader)
        {
            return ctx.trxBranchOffices.Where(x => x.GuidHeader.Equals(guidHeader)).ToList();
        }
        public IEnumerable<trxBranchOffice> GetByIdOrganisasi(int idOrganisasi)
        {
            return ctx.trxBranchOffices.Where(x => x.IdOrganisasi.Equals(idOrganisasi)).ToList();
        }
        //Create a new Data
        public void Post(trxBranchOffice entity)
        {
            ctx.trxBranchOffices.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxBranchOffice entity)
        {
            var myData = ctx.trxBranchOffices.Find(id);
            if (myData != null)
            {
                myData.IdCabang = entity.IdCabang;
                myData.IdOrganisasi = entity.IdOrganisasi;
                myData.BranchType = entity.BranchType;
                myData.JumlahPegawai = entity.JumlahPegawai;
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
            var myData = ctx.trxBranchOffices.Find(id);
            if (myData != null)
            {
                ctx.trxBranchOffices.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}