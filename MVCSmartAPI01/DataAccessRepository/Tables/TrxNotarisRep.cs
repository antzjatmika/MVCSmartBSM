using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using Omu.ValueInjecter;
using System.Linq.Dynamic;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxNotarisRep : IDataAccessRepository<trxNotari, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxNotari> Get()
        {
            return ctx.trxNotaris.ToList();
        }
        //Get Specific Data based on Id
        public trxNotari Get(int id)
        {
            return ctx.trxNotaris.Find(id);
        }
        public List<trxNotarisTabular> GetTabularByRekanan(Guid IdRekanan)
        {
            List<trxNotarisTabular> lstTabular = new List<trxNotarisTabular>();
            lstTabular = ctx.trxNotarisTabulars.Where(x => x.IdRekanan.Equals(IdRekanan)).ToList();
            return lstTabular;
        }
        //Create a new Data
        public void Post(trxNotari entity)
        {
            ctx.trxNotaris.Add(entity);
            ctx.SaveChanges();
        }
        public void PostTabular(trxNotarisTabular entity)
        {
            ctx.trxNotarisTabulars.Add(entity);
            ctx.SaveChanges();
        }
        public void PostDetail(trxNotarisDetail entity)
        {
            ctx.trxNotarisDetails.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxNotari entity)
        {
            var myData = ctx.trxNotaris.Find(id);
            if (myData != null)
            {
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
                myData.PengalamanBUMNCorp = entity.PengalamanBUMNCorp;
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
        public void PutTabular(int id, trxNotarisTabular entity)
        {
            var myData = ctx.trxNotarisTabulars.Find(id);
            if (myData != null)
            {
                myData.SKNotarisNumber = entity.SKNotarisNumber;
                myData.SKNotarisDate = entity.SKNotarisDate;
                myData.SumpahNotarisNumber = entity.SumpahNotarisNumber;
                myData.SumpahNotarisDate = entity.SumpahNotarisDate;
                myData.WilayahKerjaNotaris = entity.WilayahKerjaNotaris;
                myData.NotarisPensionDate = entity.NotarisPensionDate;

                myData.PPATSKNumber = entity.PPATSKNumber;
                myData.PPATSKDate = entity.PPATSKDate;
                myData.PPATSumpahNumber = entity.PPATSumpahNumber;
                myData.PPATSumpahDate = entity.PPATSumpahDate;
                myData.WilayahKerjaPPAT = entity.WilayahKerjaPPAT;
                myData.PPATPensionDate = entity.PPATPensionDate;
                try
                {
                    ctx.SaveChanges();
                }
                catch(Exception ex)
                {
                    string aa = ex.Message;
                }
            }
        }
        public void PutDetail(int id, trxNotarisDetail entity)
        {
            var myData = ctx.trxNotarisDetails.Find(id);
            if (myData != null)
            {
                myData.FileExtKoperasi = entity.FileExtKoperasi;
                myData.FileExtPasarModal = entity.FileExtPasarModal;
                myData.IsNotarisKoperasi = entity.IsNotarisKoperasi;
                myData.IsNotarisPasarModal = entity.IsNotarisPasarModal;
                myData.Remark = entity.Remark;
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    string aa = ex.Message;
                }
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxNotaris.Find(id);
            if (myData != null)
            {
                ctx.trxNotaris.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public void DeleteTabular(int id)
        {
            var myData = ctx.trxNotarisTabulars.Find(id);
            if (myData != null)
            {
                ctx.trxNotarisTabulars.Remove(myData);
                ctx.SaveChanges();
            }
        }

        public trxNotarisForm GetByRekananIdOld(Guid rekananId)
        {
            trxNotarisForm NotarisSingle = new trxNotarisForm();
            trxNotari notTemp = new trxNotari();
            notTemp = (trxNotari)ctx.trxNotaris.First(a => a.IdRekanan == rekananId);
            NotarisSingle.InjectFrom(notTemp);
            return NotarisSingle;
        }
        public trxNotarisFormNew GetByRekananId(Guid rekananId)
        {
            trxNotarisFormNew NotarisSingle = new trxNotarisFormNew();

            trxNotarisDetail notDetail = new trxNotarisDetail();
            int intNumDetail = ctx.trxNotarisDetails.Where(x => x.IdRekanan.Equals(rekananId)).Count();
            if (intNumDetail > 0)
            {
                notDetail = (trxNotarisDetail)ctx.trxNotarisDetails.First(a => a.IdRekanan == rekananId);
            }
            else
            {
                notDetail.IdRekanan = Guid.Empty;
            }
            NotarisSingle.DetailNotaris = notDetail;
            
            List<trxNotarisTabular> notTabular = new List<trxNotarisTabular>();
            int intNumTabular = ctx.trxNotarisTabulars.Where(x => x.IdRekanan.Equals(rekananId)).Count();
            if (intNumTabular > 0)
            {
                notTabular = (List<trxNotarisTabular>)ctx.trxNotarisTabulars.Where(a => a.IdRekanan == rekananId).ToList();
                NotarisSingle.LstNotaris = notTabular;
            }
            return NotarisSingle;
        }
        public List<trxNotarisTabular> GetNotarisTabularByRek(Guid rekananId)
        {
            List<trxNotarisTabular> myDataList = new List<trxNotarisTabular>();
            myDataList = (List<trxNotarisTabular>)ctx.trxNotarisTabulars.Where(a => a.IdRekanan == rekananId).ToList();
            return myDataList;
        }
        public trxNotarisDetail GetNotarisDetailByRek(Guid rekananId)
        {
            trxNotarisDetail myData = new trxNotarisDetail();
            myData = (trxNotarisDetail)ctx.trxNotarisDetails.Where(a => a.IdRekanan == rekananId).First();
            return myData;
        }
        public List<vwNotarisTabular> GetNotarisDetailAll()
        {
            List<vwNotarisTabular> myData = new List<vwNotarisTabular>();
            myData = ctx.vwNotarisTabulars.ToList();
            return myData;
        }
        public IEnumerable<vwNotarisTabular> XLS_GetNotarisDetailAll(string strFilterExpression)
        {
            string strError = string.Empty;
            if (string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExpression = "1 = 1";
            }
            List<vwNotarisTabular> lstNotarisDetail = new List<vwNotarisTabular>();
            try
            {
                var query = (from excelSmart in ctx.vwNotarisTabulars select excelSmart).AsQueryable().Where(strFilterExpression);
                lstNotarisDetail = query.ToList<vwNotarisTabular>();
                //lstNotarisDetail = ctx.vwNotarisTabulars.AsQueryable().Where(strFilterExpression).ToList();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return lstNotarisDetail;
        }

    }
}