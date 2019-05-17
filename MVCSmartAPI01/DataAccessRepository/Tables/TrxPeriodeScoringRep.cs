using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxPeriodeScoringRep : IDataAccessRepository<trxPeriodeScoring, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxPeriodeScoring> Get()
        {
            return ctx.trxPeriodeScoring.ToList();
        }
        //Get Specific Data based on Id
        public trxPeriodeScoring Get(int id)
        {
            return ctx.trxPeriodeScoring.Find(id);
        }
        public IEnumerable<fPeriodeScoringByRekanan_Result> GetPeriodeScoringByRekanan(Guid idRekanan)
        {
            //return ctx.trxPeriodeScoring.Where(x => x.IdRekanan.Equals(idRekanan)).ToList();
            return ctx.fPeriodeScoringByRekanan(idRekanan);
        }

        //Create a new Data
        public void Post(trxPeriodeScoring entity)
        {
            try
            {
                ctx.trxPeriodeScoring.Add(entity);
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
        public void Put(int id, trxPeriodeScoring entity)
        {
            var myData = ctx.trxPeriodeScoring.Find(id);
            if (myData != null)
            {
                myData.IdRekanan = entity.IdRekanan;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxPeriodeScoring.Find(id);
            if (myData != null)
            {
                ctx.trxPeriodeScoring.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}