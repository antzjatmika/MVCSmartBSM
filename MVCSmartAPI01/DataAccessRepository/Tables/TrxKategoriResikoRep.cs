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
    public class MstKategoriResikoRep : IDataAccessRepository<mstKategoriResiko, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstKategoriResiko> Get()
        {
            return ctx.mstKategoriResiko.ToList();
        }
        //Get Specific Data based on Id
        public mstKategoriResiko Get(int id)
        {
            return ctx.mstKategoriResiko.Find(id);
        }

        //Create a new Data
        public void Post(mstKategoriResiko entity)
        {
            try
            {
                ctx.mstKategoriResiko.Add(entity);
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
        public void Put(int id, mstKategoriResiko entity)
        {
            var myData = ctx.mstKategoriResiko.Find(id);
            if (myData != null)
            {
                //myData.Nilai= entity.Nilai;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstKategoriResiko.Find(id);
            if (myData != null)
            {
                ctx.mstKategoriResiko.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public List<mstKategoriResiko> GetByIdTypeOfRekanan(int IdTypeOfRekanan)
        {
            List<mstKategoriResiko> myData = new List<mstKategoriResiko>();
            myData = ctx.mstKategoriResiko.Where(x => x.IdTypeOfRekanan.Equals(IdTypeOfRekanan)).ToList();
            return myData;
        }

    }
}