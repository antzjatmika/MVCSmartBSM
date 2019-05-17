using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstProdukAsuransiRep : IDataAccessRepository<mstProdukAsuransi, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstProdukAsuransi> Get()
        {
            return ctx.mstProdukAsuransi.ToList();
        }
        public IEnumerable<mstProdukAsuransi> GetByRekanan(Guid IdRekanan)
        {
            return ctx.mstProdukAsuransi.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
        }
        //Get Specific Data based on Id
        public mstProdukAsuransi Get(int id)
        {
            return ctx.mstProdukAsuransi.Find(id);
        }
        //Create a new Data
        public void Post(mstProdukAsuransi entity)
        {
            try
            {
                ctx.mstProdukAsuransi.Add(entity);
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
        public void Put(int id, mstProdukAsuransi entity)
        {
            var myData = ctx.mstProdukAsuransi.Find(id);
            if (myData != null)
            {
                myData.NamaProduk = entity.NamaProduk;
                myData.NoIzinProduk = entity.NoIzinProduk;
                myData.TanggalIzin = entity.TanggalIzin;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstProdukAsuransi.Find(id);
            if (myData != null)
            {
                ctx.mstProdukAsuransi.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}