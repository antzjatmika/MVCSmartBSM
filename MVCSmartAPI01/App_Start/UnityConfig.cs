using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using MVCSmartAPI01.DataAccessRepository;
using MVCSmartAPI01.Models;

using LocalAccountsApp.Controllers;

namespace MVCSmartAPI01
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDataAccessRepository<emlNotificationLog, int>, EmlNotificationLogRep>();
            container.RegisterType<IDataAccessRepository<emlNotification, int>, EmlNotificationRep>();
            container.RegisterType<IDataAccessRepository<emlTemplate, int>, EmlTemplateRep>();
            container.RegisterType<IDataAccessRepository<mstContact_ARC, int>, MstContact_ARCRep>();
            container.RegisterType<IDataAccessRepository<mstContact, int>, MstContactRep>();
            container.RegisterType<IDataAccessRepository<mstKecamatan, int>, MstKecamatanRep>();
            container.RegisterType<IDataAccessRepository<mstReference, int>, MstReferenceRep>();
            container.RegisterType<IDataAccessRepository<mstRekanan_ARC, int>, MstRekanan_ARCRep>();
            container.RegisterType<IDataAccessRepository<mstRekanan, System.Guid>, MstRekananRep>();
            container.RegisterType<IDataAccessRepository<mstTypeOfBadanUsaha, int>, MstTypeOfBadanUsahaRep>();
            container.RegisterType<IDataAccessRepository<mstTypeOfDocument, int>, MstTypeOfDocumentRep>();
            container.RegisterType<IDataAccessRepository<mstTypeOfRegion, int>, MstTypeOfRegionRep>();
            container.RegisterType<IDataAccessRepository<mstTypeOfRekanan, int>, MstTypeOfRekananRep>();
            container.RegisterType<IDataAccessRepository<mstWilayah, int>, MstWilayahRep>();
            container.RegisterType<IDataAccessRepository<trxAuditLog, int>, TrxAuditLogRep>();
            container.RegisterType<IDataAccessRepository<trxBranchOffice_ARC, int>, TrxBranchOffice_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxBranchOffice, int>, TrxBranchOfficeRep>();
            container.RegisterType<IDataAccessRepository<trxBranchOfficeTMP, int>, trxBranchOfficeTMPRep>();
            container.RegisterType<IDataAccessRepository<trxBranchOfficeHeader, int>, TrxBranchOfficeHeaderRep>();
            container.RegisterType<IDataAccessRepository<trxDataOrganisasi_ARC, int>, TrxDataOrganisasi_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxDataOrganisasi, int>, TrxDataOrganisasiRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaan_ARC, int>, TrxDetailPekerjaan_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaan, int>, TrxDetailPekerjaanRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaanAS_1M, int>, TrxDetailPekerjaanAS_1MRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaanAS_3M, int>, TrxDetailPekerjaanAS_3MRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaanBLG, int>, TrxDetailPekerjaanBLGRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaanTMP, int>, TrxDetailPekerjaanTMPRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaanAS_1MTMP, int>, TrxDetailPekerjaanAS_1MTMPRep>();
            container.RegisterType<IDataAccessRepository<trxDetailPekerjaanHeader, int>, TrxDetailPekerjaanHeaderRep>();
            container.RegisterType<IDataAccessRepository<trxDocumentMandatory, int>, TrxDocumentMandatoryRep>();
            container.RegisterType<IDataAccessRepository<trxFileDataPekerjaan, int>, TrxFileDataPekerjaanRep>();
            container.RegisterType<IDataAccessRepository<trxMonitoringFeeBased_ARC, int>, TrxMonitoringFeeBased_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxMonitoringFeeBased, int>, TrxMonitoringFeeBasedRep>();
            container.RegisterType<IDataAccessRepository<trxNotaris_ARC, int>, TrxNotaris_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxNotari, int>, TrxNotarisRep>();
            container.RegisterType<IDataAccessRepository<trxOwnership_ARC, int>, TrxOwnership_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxOwnership, int>, TrxOwnershipRep>();
            container.RegisterType<IDataAccessRepository<trxRegistrationRequest, int>, TrxRegistrationRequestRep>();
            container.RegisterType<IDataAccessRepository<trxRekananDocument, int>, TrxRekananDocumentRep>();
            container.RegisterType<IDataAccessRepository<trxRekananDocumentTrack, int>, TrxRekananDocumentTrackRep>();
            container.RegisterType<IDataAccessRepository<trxSanksiEksternal_ARC, int>, TrxSanksiEksternal_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxSanksiEksternal, int>, TrxSanksiEksternalRep>();
            container.RegisterType<IDataAccessRepository<trxSanksiInternal_ARC, int>, TrxSanksiInternal_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxSanksiInternal, int>, TrxSanksiInternalRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaAhli_ARC, int>, TrxTenagaAhli_ARCRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaAhli, int>, TrxTenagaAhliRep>();
            container.RegisterType<IDataAccessRepository<trxNotificationContent, int>, TrxNotificationContentRep>();
            container.RegisterType<IDataAccessRepository<trxNotificationHeader, int>, TrxNotificationHeaderRep>();
            container.RegisterType<IDataAccessRepository<trxNotificationDetail, int>, TrxNotificationDetailRep>();
            container.RegisterType<IDataAccessRepository<vwUserRole, string>, VwUserRoleRep>();
            container.RegisterType<IDataAccessRepository<rptAsuransi, int>, RptAsuransiRep>();
            container.RegisterType<IDataAccessRepository<mstSegmentasi, int>, MstSegmentasiRep>();
            container.RegisterType<IDataAccessRepository<trxDocumentMandatory, int>, TrxDocumentMandatoryRep>();
            container.RegisterType<IDataAccessRepository<trxDocMandatoryDetail, int>, TrxDocMandatoryDetailRep>();
            container.RegisterType<IDataAccessRepository<trxDocMandatoryFile, int>, TrxDocMandatoryFileRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaPendukung, int>, TrxTenagaPendukungRep>();
            container.RegisterType<IDataAccessRepository<trxAsisten, int>, TrxAsistenRep>();
            container.RegisterType<IDataAccessRepository<trxManagement, int>, TrxManagementRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaAhliTetapImp, int>, TrxTenagaAhliTetapImpRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaAhliTidakTetapImp, int>, TrxTenagaAhliTidakTetapImpRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaPendukungImp, int>, TrxTenagaPendukungImpRep>();
            container.RegisterType<IDataAccessRepository<trxFileScanned, int>, TrxFileScannedRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaAhliHeader, int>, TrxTenagaAhliHeaderRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaAhliTMP, int>, TrxTenagaAhliTMPRep>();
            container.RegisterType<IDataAccessRepository<trxTenagaPendukungTMP, int>, TrxTenagaPendukungTMPRep>();
            container.RegisterType<IDataAccessRepository<mstRegionAdmin, Guid>, MstRegionAdminRep>();
            container.RegisterType<IDataAccessRepository<mstSubRegion, int>, MstSubRegionRep>();
            container.RegisterType<IDataAccessRepository<mstPengumuman, int>, mstPengumumanRep>();
            container.RegisterType<IDataAccessRepository<trxNotarisItem, int>, trxNotarisItemRep>();
            container.RegisterType<IDataAccessRepository<trxPertanyaanNilai, int>, TrxPertanyaanNilaiRep>();
            container.RegisterType<IDataAccessRepository<trxKonsolidasi, int>, TrxKonsolidasiRep>();
            container.RegisterType<IDataAccessRepository<mstKategoriResiko, int>, MstKategoriResikoRep>();
            container.RegisterType<IDataAccessRepository<mstProdukAsuransi, int>, MstProdukAsuransiRep>();
            container.RegisterType<IDataAccessRepository<trxPeriodeScoring, int>, TrxPeriodeScoringRep>();
            container.RegisterType<IDataAccessRepository<trxDocMandatoryVerification, int>, TrxDocMandatoryVerificationRep>();

            container.RegisterType<AccountController>(new InjectionConstructor());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}