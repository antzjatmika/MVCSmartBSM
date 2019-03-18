using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;
using Omu.ValueInjecter;
using System.Linq.Dynamic;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class MstRekananRep : IDataAccessRepository<mstRekanan, System.Guid>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<mstRekanan> Get()
        {
            return ctx.mstRekanans.ToList();
        }
        //Get Specific Data based on Id
        public IEnumerable<fGetRekananByIdSupervisor_Result> GetBySupervisorId(int supervisorId)
        {
            string strError = string.Empty;
            List<mstRekananExt> lstMstRekananExt = new List<mstRekananExt>();
            List<fGetRekananByIdSupervisor_Result> lstMstRekananRest = new List<fGetRekananByIdSupervisor_Result>();
            try
            {
                lstMstRekananRest = ctx.fGetRekananByIdSupervisor(supervisorId).ToList<fGetRekananByIdSupervisor_Result>();
            }
            catch(Exception ex)
            {
                strError = ex.Message;
            }
            return lstMstRekananRest;
        }


        public IEnumerable<fManagementRekanan_Result> GetManagementRekanan()
        {
            string strError = string.Empty;
            List<fManagementRekanan_Result> lstManagemenRekRest = new List<fManagementRekanan_Result>();
            try
            {
                lstManagemenRekRest = ctx.fManagementRekanan().ToList<fManagementRekanan_Result>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return lstManagemenRekRest;
        }

        public IEnumerable<trxManagementP2PK> GetManagementP2PK(int idTypeOfRekanan, bool isActive)
        {
            string strError = string.Empty;
            List<trxManagementP2PK> lstManagemenRekRest = new List<trxManagementP2PK>();
            try
            {
                lstManagemenRekRest = ctx.trxManagementP2PK.Where(x => x.TipeRekanan.Equals(idTypeOfRekanan) && x.IsActive.Equals(isActive)).ToList<trxManagementP2PK>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return lstManagemenRekRest;
        }

        public fGetSupervisorByRek_Result GetSupervisorByRek(Guid IdRekanan)
        {
            string strError = string.Empty;
            fGetSupervisorByRek_Result myData = new fGetSupervisorByRek_Result();
            try
            {
                myData = ctx.fGetSupervisorByRek(IdRekanan).ToList<fGetSupervisorByRek_Result>().First();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return myData;
        }
        public IEnumerable<fXLS_RekByIdSupervisor_Result> XLS_RekByIdSupervisor(int supervisorId, string strFilterExpression)
        {
            string strError = string.Empty;
            if(string.IsNullOrEmpty( strFilterExpression))
            {
                strFilterExpression = "1 = 1";
            }
            //List<fXLS_RekByIdSupervisor_Result> lstMstRekananRest = new List<fXLS_RekByIdSupervisor_Result>();
            List<fXLS_RekByIdSupervisor_Result> lstMstRekananFinal = new List<fXLS_RekByIdSupervisor_Result>();
            try
            {
                //lstMstRekananRest = ctx.fXLS_RekByIdSupervisor(supervisorId).ToList<fXLS_RekByIdSupervisor_Result>();
                var query = (from excelSmart in ctx.fXLS_RekByIdSupervisor(supervisorId) select excelSmart).AsQueryable().Where(strFilterExpression);
                lstMstRekananFinal = query.ToList<fXLS_RekByIdSupervisor_Result>();
            }
            catch(Exception ex)
            {
                strError = ex.Message;
            }
            return lstMstRekananFinal;
        }
        public IEnumerable<fManagementRekanan_Result> XLS_ManagementRekanan(string strFilterExpression)
        {
            string strError = string.Empty;
            if (string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExpression = "1 = 1";
            }
            List<fManagementRekanan_Result> lstManagementRek = new List<fManagementRekanan_Result>();
            try
            {
                var query = (from excelSmart in ctx.fManagementRekanan() select excelSmart).AsQueryable().Where(strFilterExpression);
                lstManagementRek = query.ToList<fManagementRekanan_Result>();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return lstManagementRek;
        }
        public IEnumerable<fInfoLastModifiedByRek_Result> GetInfoLastModifiedByRek(Guid IdRekanan)
        {
            List<fInfoLastModifiedByRek_Result> lstInfo = ctx.fInfoLastModifiedByRek(IdRekanan).ToList<fInfoLastModifiedByRek_Result>();
            
            return lstInfo;
        }
        public IEnumerable<fInfo2LastModifiedByRek_Result> GetInfo2LastModifiedByRek(Guid IdRekanan)
        {
            List<fInfo2LastModifiedByRek_Result> lstInfo = ctx.fInfo2LastModifiedByRek(IdRekanan).ToList<fInfo2LastModifiedByRek_Result>();
            return lstInfo;
        }
        public fGetPengumumanByTypeOfRekanan_Result GetPengumumanByTypeOfRekanan(int IdTypeOfRekanan)
        {
            fGetPengumumanByTypeOfRekanan_Result dataPengumuman = ctx.fGetPengumumanByTypeOfRekanan(IdTypeOfRekanan).ToList<fGetPengumumanByTypeOfRekanan_Result>().First();
            return dataPengumuman;
        }


        public mstRekanan Get(System.Guid id)
        {
            return ctx.mstRekanans.Find(id);
        }

        //Create a new Data
        public void Post(mstRekanan entity)
        {
            try
            {
                ctx.mstRekanans.Add(entity);
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
        //Update Exisiting Data with Note and LMDate
        public void Put(System.Guid id, mstRekanan entity)
        {
            var myData = ctx.mstRekanans.Find(id);
            if (myData != null)
            {
                myData.IdRegion = entity.IdRegion;
                myData.RegistrationNumber = entity.RegistrationNumber;
                myData.ClassOfRekanan = entity.ClassOfRekanan;
                myData.IdTypeOfRekanan = entity.IdTypeOfRekanan;
                myData.IdTypeOfBadanUsaha = entity.IdTypeOfBadanUsaha;
                myData.Name = entity.Name;
                myData.Address = entity.Address;
                myData.Kelurahan = entity.Kelurahan;
                myData.Kecamatan = entity.Kecamatan;
                myData.Kota = entity.Kota;
                myData.IdWilayah = entity.IdWilayah;
                myData.ZipCode = entity.ZipCode;
                myData.Phone1 = entity.Phone1;
                myData.Phone2 = entity.Phone2;
                myData.Phone3 = entity.Phone3;
                myData.Fax1 = entity.Fax1;
                myData.Fax2 = entity.Fax2;
                myData.EmailAddress = entity.EmailAddress;
                myData.PenerbitRating = entity.PenerbitRating;
                myData.NilaiRating = entity.NilaiRating;
                myData.IsActive = entity.IsActive;
                myData.Note = entity.Note;
                myData.LMDate = entity.LMDate;
                ctx.SaveChanges();
            }
        }
        public void UpdateNote(System.Guid id, string Note)
        {
            var myData = ctx.mstRekanans.Find(id);
            if (myData != null)
            {
                myData.Note = Note;
                myData.LMDate = DateTime.Today;
                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(System.Guid id)
        {
            var myData = ctx.mstRekanans.Find(id);
            if (myData != null)
            {
                ctx.mstRekanans.Remove(myData);
                ctx.SaveChanges();
            }
        }
        public string CheckRekananByNPWP(string strNomorNPWP)
        {
            string strInfo = string.Empty;
            string strNamaRekanan = string.Empty;
            var myRekanan = from mgm in ctx.trxManagement
                            join rek in ctx.mstRekanans on mgm.IdRekanan equals rek.IdRekanan
                            where mgm.NomorNPWP == strNomorNPWP
                            select rek;
            try
            {
                strNamaRekanan = myRekanan.SingleOrDefault().Name;
            }
            catch(Exception ex)
            {
                string strMessage = ex.Message;
                strNamaRekanan = string.Empty;
            }
            //cek di Managemen, 
            //jika tidak ada, maka pendaftaran baru
            //  - jika saat ini dalam periode pendaftaran, maka : buat user, kirim username - password
            //  - jika saat ini tidak dalam periode pendaftaran, maka : kirim pengumuman tentang p[eriode pendaftarannya
            //jika ada, maka : kirim informasi bahwa npwp sudah digunakan oleh rekanan A, silakan koordinasi dengan yg lain
            if(strNamaRekanan != string.Empty) // npwp sudah digunakan oleh rekanan
            {
                strInfo = "Proses registrasi tidak dapat dilanjutkan. NPWP sudah digunakan oleh rekanan lain";
            }
            else
            {
                //cek masa pendaftaran
                var masaRegistrasiInfo = ctx.fMasukMasaRegistrasi().SingleOrDefault();
                switch (masaRegistrasiInfo.IsMasaRegistrasi)
                {
                    case -1 :
                        strInfo = "Saat ini registrasi rekanan baru belum dibuka";
                        break;
                    case 0:
                        strInfo = "Registrasi rekanan baru akan dibuka tanggal " + masaRegistrasiInfo.AwalRegistrasi.ToString();
                        break;
                    case 1:
                        strInfo = "Proses registrasi sudah selesai, silahkan periksa email untuk informasi lebih lanjut";
                        break;
                    default:
                        break;
                }
            }

            return strNamaRekanan;
        }

    }
}