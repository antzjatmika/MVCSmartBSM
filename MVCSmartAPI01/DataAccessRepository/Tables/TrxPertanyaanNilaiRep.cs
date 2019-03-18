using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxPertanyaanNilaiRep : IDataAccessRepository<trxPertanyaanNilai, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxPertanyaanNilai> Get()
        {
            return ctx.trxPertanyaanNilai.ToList();
        }
        //Get Specific Data based on Id
        public trxPertanyaanNilai Get(int id)
        {
            return ctx.trxPertanyaanNilai.Find(id);
        }

        //Create a new Data
        public void Post(trxPertanyaanNilai entity)
        {
            try
            {
                ctx.trxPertanyaanNilai.Add(entity);
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
        public void Put(int id, trxPertanyaanNilai entity)
        {
            var myData = ctx.trxPertanyaanNilai.Find(id);
            if (myData != null)
            {
                myData.Nilai= entity.Nilai;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxPertanyaanNilai.Find(id);
            if (myData != null)
            {
                ctx.trxPertanyaanNilai.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public List<vwPertanyaanNilai> GetNilaiByPenilai(int IdTypeOfGroup, int IdPenilai, Guid IdRekanan)
        {
            List<vwPertanyaanNilai> myData = new List<vwPertanyaanNilai>();
            myData = ctx.vwPertanyaanNilai.Where(x => x.IdTypeOfGroup.Equals(IdTypeOfGroup) && x.IdPenilai.Equals(IdPenilai) && x.IdRekanan.Equals(IdRekanan)).ToList();
            return myData;
        }
        public List<vwPertanyaanNilaiAkhir> GetNilaiAkhir(Guid IdRekanan)
        {
            List<vwPertanyaanNilaiAkhir> myData = new List<vwPertanyaanNilaiAkhir>();
            myData = ctx.vwPertanyaanNilaiAkhir.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
            return myData;
        }

    }
}