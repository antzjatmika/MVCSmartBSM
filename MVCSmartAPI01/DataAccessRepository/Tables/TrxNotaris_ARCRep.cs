using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxNotaris_ARCRep : IDataAccessRepository<trxNotaris_ARC, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxNotaris_ARC> Get()
        {
            return ctx.trxNotaris_ARC.ToList();
        }
        //Get Specific Data based on Id
        public trxNotaris_ARC Get(int id)
        {
            return ctx.trxNotaris_ARC.Find(id);
        }

        //Create a new Data
        public void Post(trxNotaris_ARC entity)
        {
            ctx.trxNotaris_ARC.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxNotaris_ARC entity)
        {
            var myData = ctx.trxNotaris_ARC.Find(id);
            if (myData != null)
            {
                myData.IdAction = entity.IdAction;
                myData.IdNotaris = entity.IdNotaris;
                myData.IdRekanan = entity.IdRekanan;
                myData.SKNotaris = entity.SKNotaris;
                myData.SumpahNotarisNumber = entity.SumpahNotarisNumber;
                myData.SumpahNotarisDate = entity.SumpahNotarisDate;
                myData.City = entity.City;
                myData.IsKoperasiMember = entity.IsKoperasiMember;
                myData.SKKoperasiNumber = entity.SKKoperasiNumber;
                myData.SKKoperasiDate = entity.SKKoperasiDate;
                myData.IsBapepamReg = entity.IsBapepamReg;
                myData.BapepamSKNumber = entity.BapepamSKNumber;
                myData.BapepamSKDate = entity.BapepamSKDate;
                myData.UpLimit = entity.UpLimit;
                myData.PPATSKNumber = entity.PPATSKNumber;
                myData.PPATSKDate = entity.PPATSKDate;
                myData.PPATSumpahNumber = entity.PPATSumpahNumber;
                myData.PPATSumpahDate = entity.PPATSumpahDate;
                myData.WilayahKerjaPPAT = entity.WilayahKerjaPPAT;
                myData.IsIPPATMember = entity.IsIPPATMember;
                myData.SKIPPATNumber = entity.SKIPPATNumber;
                myData.SKPPATDate = entity.SKPPATDate;
                myData.IsINIMember = entity.IsINIMember;
                myData.INISKNumber = entity.INISKNumber;
                myData.INISKDate = entity.INISKDate;
                myData.Remark = entity.Remark;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxNotaris_ARC.Find(id);
            if (myData != null)
            {
                ctx.trxNotaris_ARC.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}