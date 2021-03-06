﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSmartAPI01.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_SMARTEntities : DbContext
    {
        public DB_SMARTEntities()
            : base("name=DB_SMARTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<emlNotification> emlNotifications { get; set; }
        public virtual DbSet<emlNotificationLog> emlNotificationLogs { get; set; }
        public virtual DbSet<emlTemplate> emlTemplates { get; set; }
        public virtual DbSet<mstContact> mstContacts { get; set; }
        public virtual DbSet<mstContact_ARC> mstContact_ARC { get; set; }
        public virtual DbSet<mstKecamatan> mstKecamatans { get; set; }
        public virtual DbSet<mstReference> mstReferences { get; set; }
        public virtual DbSet<mstRegionAdmin> mstRegionAdmins { get; set; }
        public virtual DbSet<mstRekanan> mstRekanans { get; set; }
        public virtual DbSet<mstRekanan_ARC> mstRekanan_ARC { get; set; }
        public virtual DbSet<mstScoreValue> mstScoreValues { get; set; }
        public virtual DbSet<mstSegmentasi> mstSegmentasis { get; set; }
        public virtual DbSet<mstTemplateDokRekanan> mstTemplateDokRekanans { get; set; }
        public virtual DbSet<mstTypeOfBadanUsaha> mstTypeOfBadanUsahas { get; set; }
        public virtual DbSet<mstTypeOfDocument> mstTypeOfDocuments { get; set; }
        public virtual DbSet<mstTypeOfRegion> mstTypeOfRegions { get; set; }
        public virtual DbSet<mstTypeOfRekanan> mstTypeOfRekanans { get; set; }
        public virtual DbSet<mstWilayah> mstWilayahs { get; set; }
        public virtual DbSet<rptKAP> rptKAPs { get; set; }
        public virtual DbSet<rptKJPP> rptKJPPs { get; set; }
        public virtual DbSet<rptKonsultanManageman> rptKonsultanManagemen { get; set; }
        public virtual DbSet<rptNotari> rptNotaris { get; set; }
        public virtual DbSet<trxAsisten> trxAsistens { get; set; }
        public virtual DbSet<trxAuditLog> trxAuditLogs { get; set; }
        public virtual DbSet<trxBranchOffice> trxBranchOffices { get; set; }
        public virtual DbSet<trxBranchOffice_ARC> trxBranchOffice_ARC { get; set; }
        public virtual DbSet<trxDataOrganisasi> trxDataOrganisasis { get; set; }
        public virtual DbSet<trxDataOrganisasi_ARC> trxDataOrganisasi_ARC { get; set; }
        public virtual DbSet<trxDetailPekerjaan_ARC> trxDetailPekerjaan_ARC { get; set; }
        public virtual DbSet<trxDocMandatoryFile> trxDocMandatoryFiles { get; set; }
        public virtual DbSet<trxDocumentMandatory> trxDocumentMandatories { get; set; }
        public virtual DbSet<trxFileDataPekerjaan> trxFileDataPekerjaans { get; set; }
        public virtual DbSet<trxMonitoringFeeBased> trxMonitoringFeeBaseds { get; set; }
        public virtual DbSet<trxMonitoringFeeBased_ARC> trxMonitoringFeeBased_ARC { get; set; }
        public virtual DbSet<trxNotari> trxNotaris { get; set; }
        public virtual DbSet<trxNotaris_ARC> trxNotaris_ARC { get; set; }
        public virtual DbSet<trxNotificationAttachment> trxNotificationAttachments { get; set; }
        public virtual DbSet<trxNotificationContent> trxNotificationContents { get; set; }
        public virtual DbSet<trxNotificationDetail> trxNotificationDetails { get; set; }
        public virtual DbSet<trxNotificationHeader> trxNotificationHeaders { get; set; }
        public virtual DbSet<trxOwnership> trxOwnerships { get; set; }
        public virtual DbSet<trxOwnership_ARC> trxOwnership_ARC { get; set; }
        public virtual DbSet<trxRegistrationRequest> trxRegistrationRequests { get; set; }
        public virtual DbSet<trxRekananDocumentTrack> trxRekananDocumentTracks { get; set; }
        public virtual DbSet<trxSanksiEksternal> trxSanksiEksternals { get; set; }
        public virtual DbSet<trxSanksiEksternal_ARC> trxSanksiEksternal_ARC { get; set; }
        public virtual DbSet<trxSanksiInternal> trxSanksiInternals { get; set; }
        public virtual DbSet<trxSanksiInternal_ARC> trxSanksiInternal_ARC { get; set; }
        public virtual DbSet<trxTenagaAhli_ARC> trxTenagaAhli_ARC { get; set; }
        public virtual DbSet<rptAsuransi> rptAsuransis { get; set; }
        public virtual DbSet<rptBalaiLelang> rptBalaiLelangs { get; set; }
        public virtual DbSet<vwKelengkapanDokumenRekanan> vwKelengkapanDokumenRekanans { get; set; }
        public virtual DbSet<vwUserRole> vwUserRoles { get; set; }
        public virtual DbSet<trxRekananDocument> trxRekananDocuments { get; set; }
        public virtual DbSet<trxTenagaAhliTidakTetapImp> trxTenagaAhliTidakTetapImps { get; set; }
        public virtual DbSet<trxTenagaPendukungImp> trxTenagaPendukungImps { get; set; }
        public virtual DbSet<trxTenagaAhliTetapImp> trxTenagaAhliTetapImps { get; set; }
        public virtual DbSet<trxFileScanned> trxFileScanneds { get; set; }
        public virtual DbSet<trxManagement> trxManagements { get; set; }
        public virtual DbSet<trxTenagaAhli> trxTenagaAhlis { get; set; }
        public virtual DbSet<trxTenagaPendukung> trxTenagaPendukungs { get; set; }
        public virtual DbSet<trxTenagaAhliHeader> trxTenagaAhliHeaders { get; set; }
        public virtual DbSet<trxTenagaAhliTMP> trxTenagaAhliTMPs { get; set; }
        public virtual DbSet<trxTenagaPendukungTMP> trxTenagaPendukungTMPs { get; set; }
        public virtual DbSet<trxDetailPekerjaanHeader> trxDetailPekerjaanHeaders { get; set; }
        public virtual DbSet<trxDetailPekerjaan> trxDetailPekerjaans { get; set; }
        public virtual DbSet<trxDetailPekerjaanTMP> trxDetailPekerjaanTMPs { get; set; }
        public virtual DbSet<trxDocMandatoryDetail> trxDocMandatoryDetails { get; set; }
    
        [DbFunction("DB_SMARTEntities", "fDataOrganisasiByRek")]
        public virtual IQueryable<fDataOrganisasiByRek_Result> fDataOrganisasiByRek(Nullable<System.Guid> idRekanan)
        {
            var idRekananParameter = idRekanan.HasValue ?
                new ObjectParameter("IdRekanan", idRekanan) :
                new ObjectParameter("IdRekanan", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fDataOrganisasiByRek_Result>("[DB_SMARTEntities].[fDataOrganisasiByRek](@IdRekanan)", idRekananParameter);
        }
    
        [DbFunction("DB_SMARTEntities", "fDataRekananByIds")]
        public virtual IQueryable<fDataRekananByIds_Result> fDataRekananByIds(string emailAddress, string nomorNPWP, string nomorKTP)
        {
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("emailAddress", emailAddress) :
                new ObjectParameter("emailAddress", typeof(string));
    
            var nomorNPWPParameter = nomorNPWP != null ?
                new ObjectParameter("nomorNPWP", nomorNPWP) :
                new ObjectParameter("nomorNPWP", typeof(string));
    
            var nomorKTPParameter = nomorKTP != null ?
                new ObjectParameter("nomorKTP", nomorKTP) :
                new ObjectParameter("nomorKTP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fDataRekananByIds_Result>("[DB_SMARTEntities].[fDataRekananByIds](@emailAddress, @nomorNPWP, @nomorKTP)", emailAddressParameter, nomorNPWPParameter, nomorKTPParameter);
        }
    
        [DbFunction("DB_SMARTEntities", "fGenerateUsernamePassword")]
        public virtual IQueryable<fGenerateUsernamePassword_Result> fGenerateUsernamePassword(string idRekanan)
        {
            var idRekananParameter = idRekanan != null ?
                new ObjectParameter("IdRekanan", idRekanan) :
                new ObjectParameter("IdRekanan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fGenerateUsernamePassword_Result>("[DB_SMARTEntities].[fGenerateUsernamePassword](@IdRekanan)", idRekananParameter);
        }
    
        [DbFunction("DB_SMARTEntities", "fGetNotificationBySubject")]
        public virtual IQueryable<fGetNotificationBySubject_Result> fGetNotificationBySubject(Nullable<System.Guid> subjetTo, Nullable<int> levelUrgensi)
        {
            var subjetToParameter = subjetTo.HasValue ?
                new ObjectParameter("SubjetTo", subjetTo) :
                new ObjectParameter("SubjetTo", typeof(System.Guid));
    
            var levelUrgensiParameter = levelUrgensi.HasValue ?
                new ObjectParameter("LevelUrgensi", levelUrgensi) :
                new ObjectParameter("LevelUrgensi", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fGetNotificationBySubject_Result>("[DB_SMARTEntities].[fGetNotificationBySubject](@SubjetTo, @LevelUrgensi)", subjetToParameter, levelUrgensiParameter);
        }
    
        [DbFunction("DB_SMARTEntities", "fGetRekananByIdSupervisor")]
        public virtual IQueryable<mstRekanan> fGetRekananByIdSupervisor(Nullable<int> idSupervisor)
        {
            var idSupervisorParameter = idSupervisor.HasValue ?
                new ObjectParameter("IdSupervisor", idSupervisor) :
                new ObjectParameter("IdSupervisor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<mstRekanan>("[DB_SMARTEntities].[fGetRekananByIdSupervisor](@IdSupervisor)", idSupervisorParameter);
        }
    
        [DbFunction("DB_SMARTEntities", "fDataOrganisasiByRek_Type")]
        public virtual IQueryable<fDataOrganisasiByRek_Type_Result> fDataOrganisasiByRek_Type(Nullable<System.Guid> idRekanan, Nullable<int> tipeRekanan)
        {
            var idRekananParameter = idRekanan.HasValue ?
                new ObjectParameter("IdRekanan", idRekanan) :
                new ObjectParameter("IdRekanan", typeof(System.Guid));
    
            var tipeRekananParameter = tipeRekanan.HasValue ?
                new ObjectParameter("TipeRekanan", tipeRekanan) :
                new ObjectParameter("TipeRekanan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fDataOrganisasiByRek_Type_Result>("[DB_SMARTEntities].[fDataOrganisasiByRek_Type](@IdRekanan, @TipeRekanan)", idRekananParameter, tipeRekananParameter);
        }
    
        public virtual int spTranDataTenagaAhli(Nullable<System.Guid> guidHeader)
        {
            var guidHeaderParameter = guidHeader.HasValue ?
                new ObjectParameter("GuidHeader", guidHeader) :
                new ObjectParameter("GuidHeader", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTranDataTenagaAhli", guidHeaderParameter);
        }
    
        public virtual int spTranDataTenagaAhliTidakTetap(Nullable<System.Guid> guidHeader)
        {
            var guidHeaderParameter = guidHeader.HasValue ?
                new ObjectParameter("GuidHeader", guidHeader) :
                new ObjectParameter("GuidHeader", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTranDataTenagaAhliTidakTetap", guidHeaderParameter);
        }
    
        public virtual int spTranDataTenagaPendukung(Nullable<System.Guid> guidHeader)
        {
            var guidHeaderParameter = guidHeader.HasValue ?
                new ObjectParameter("GuidHeader", guidHeader) :
                new ObjectParameter("GuidHeader", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTranDataTenagaPendukung", guidHeaderParameter);
        }
    
        [DbFunction("DB_SMARTEntities", "fPekerjaanByTypeOfRekanan")]
        public virtual IQueryable<trxDetailPekerjaan> fPekerjaanByTypeOfRekanan(Nullable<int> idTypeOfRekanan)
        {
            var idTypeOfRekananParameter = idTypeOfRekanan.HasValue ?
                new ObjectParameter("IdTypeOfRekanan", idTypeOfRekanan) :
                new ObjectParameter("IdTypeOfRekanan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<trxDetailPekerjaan>("[DB_SMARTEntities].[fPekerjaanByTypeOfRekanan](@IdTypeOfRekanan)", idTypeOfRekananParameter);
        }
    
        public virtual int spTranDataDetailPekerjaan(Nullable<System.Guid> guidHeader)
        {
            var guidHeaderParameter = guidHeader.HasValue ?
                new ObjectParameter("GuidHeader", guidHeader) :
                new ObjectParameter("GuidHeader", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTranDataDetailPekerjaan", guidHeaderParameter);
        }
    }
}
