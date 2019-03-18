using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class mstPengumumanRep : IDataAccessRepository<mstPengumuman, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstPengumuman> Get()
        {
            return ctx.mstPengumumen.ToList();
        }
        //Get Specific Data based on Id
        public mstPengumuman Get(int id)
        {
            return ctx.mstPengumumen.Find(id);
        }
        public IEnumerable<mstPengumuman> GetByIdTypeOfRekanan(int idTypeOfRekanan)
        {
            return ctx.mstPengumumen.Where(x => x.IdTypeOfRekanan.Equals(idTypeOfRekanan)).ToList();
        }

        //Create a new Data
        public void Post(mstPengumuman entity)
        {
            try
            {
                ctx.mstPengumumen.Add(entity);
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
        public void Put(int id, mstPengumuman entity)
        {
            var myData = ctx.mstPengumumen.Find(id);
            if (myData != null)
            {
                myData.IdTypeOfRekanan = entity.IdTypeOfRekanan;
                myData.JudulPengumuman = entity.JudulPengumuman;
                myData.IsiPengumuman = entity.IsiPengumuman;
                myData.ImageName = entity.ImageName;
                myData.FileExt = entity.FileExt;
                myData.MulaiAktif = entity.MulaiAktif;
                myData.SelesaiAktif = entity.SelesaiAktif;
                myData.CreatedUser = entity.CreatedUser;
                myData.CreatedDate = entity.CreatedDate;
                myData.IsActive = entity.IsActive;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.mstPengumumen.Find(id);
            if (myData != null)
            {
                ctx.mstPengumumen.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}