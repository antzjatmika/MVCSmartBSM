using System;
using DevExpress.Utils;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Mvc.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVCSmartClient01.Controllers
{
    public static class GridSettingHelper
    {
        public static GridViewSettings XLS_DaftarRekanan(string strFilterExp)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListRekanan";
            settings.CallbackRouteValues = new { Controller = "MstRekanan", Action = "XLS_DaftarRekanan" };

            settings.KeyFieldName = "IdRekanan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            settings.Columns.Add(column =>
            {
                column.FieldName = "RegistrationNumber";
                column.Caption = "No. Reg";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Name";
                column.Caption = "Nama Rekanan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NoNPWP";
                column.Caption = "NPWP";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Address";
                column.Caption = "Alamat";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 200;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Phone1";
                column.Caption = "Telepon 1";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Phone2";
                column.Caption = "Telepon 2";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Fax1";
                column.Caption = "Fax";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "ZipCode";
                column.Caption = "Kode Pos";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "EmailAddress";
                column.Caption = "Email";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "ContactPerson";
                column.Caption = "Contact Person";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Handphone";
                column.Caption = "Handphone";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });

            settings.SettingsExport.PageHeader.Center = "Daftar Rekanan";
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_ManagementRek(string strFilterExp)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvManagementRekanan";
            settings.CallbackRouteValues = new { Controller = "MstRekanan", Action = "XLS_ManagementRek" };

            settings.KeyFieldName = "IdManagemen";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            settings.Columns.Add(column =>
            {
                column.FieldName = "Name";
                column.Caption = "Nama Management";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "RoleTitle";
                column.Caption = "Jabatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Alamat";
                column.Caption = "Alamat";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorKTP";
                column.Caption = "No. KTP";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalLahir";
                column.Caption = "Tgl. Lahir";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorNPWP";
                column.Caption = "No. NPWP";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorIAPI";
                column.Caption = "No. IAPI";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorIjinPenilai";
                column.Caption = "No. Ijin Penilai";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "AkhirBerlakuIAPI";
                column.Caption = "Akhir IAPI";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalMulaiBlackList";
                column.Caption = "Mulai Sanksi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalAkhirBlacklist";
                column.Caption = "Akhir Sanksi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "StatusBlackList";
                column.Caption = "Status Sanksi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Email1";
                column.Caption = "Email1";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Email2";
                column.Caption = "Email2";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Handphone1";
                column.Caption = "Handphone1";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Handphone2";
                column.Caption = "Handphone2";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Telephone1";
                column.Caption = "Telephone1";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Telephone2";
                column.Caption = "Telephone2";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "RegistrationNumber";
                column.Caption = "Reg. Number";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NamaRekanan";
                column.Caption = "Nama Rekanan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });

            settings.SettingsExport.PageHeader.Center = "Daftar Management Rekanan";
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_DaftarPekerjaanKJP(string strFilterExp, string strJenisRekanan, bool IsPCP = true)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListPekerjaan";
            settings.CallbackRouteValues = new { Controller = "TrxDetailPekerjaan", Action = "XLS_PekerjaanByTypeOfRek" };

            settings.KeyFieldName = "IdDetailPekerjaan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };

            if (IsPCP)
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "RegistrationNumber";
                    column.Caption = "No. Reg";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 70;
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Name";
                    column.Caption = "Nama Rekanan";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 200;
                });
            }
            settings.Columns.Add(column =>
            {
                column.FieldName = "TahunLaporan";
                column.Caption = "Tahun";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "BulanLaporan";
                column.Caption = "Bulan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturName";
                column.Caption = "Nama Debitur";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturLocation";
                column.Caption = "Lokasi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "BidangUsahaDebitur";
                column.Caption = "Bidang Usaha";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "JenisJasa";
                column.Caption = "Jenis Jasa";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorLaporan";
                column.Caption = "No. Laporan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PenanggungJawab";
                column.Caption = "PenanggungJawab";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NamaPemberiPekerjaan";
                column.Caption = "Pemberi Pekerjaan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "UnitKerja";
                column.Caption = "Unit Kerja";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TotalAsetPerusahaan";
                column.Caption = "Total Aset";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "FeeNominal";
                column.Caption = "Fee Nominal";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalSelesaiPekerjaan";
                column.Caption = "Tgl. Selesai";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            //settings.Columns.Add(column =>
            //{
            //    column.FieldName = "";
            //    column.Caption = "";
            //    column.HeaderStyle.Font.Bold = true;
            //    column.ExportWidth = 100;
            //});

            settings.SettingsExport.PageHeader.Left = "Daftar Pekerjaan " + strJenisRekanan;
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_DaftarPekerjaanKAP(string strFilterExp, bool IsPCP = true)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListPekerjaan";
            settings.CallbackRouteValues = new { Controller = "TrxDetailPekerjaan", Action = "XLS_PekerjaanByTypeOfRek" };

            settings.KeyFieldName = "IdDetailPekerjaan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            if (IsPCP)
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "RegistrationNumber";
                    column.Caption = "No. Reg";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 70;
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Name";
                    column.Caption = "Nama Rekanan";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 200;
                });
            }
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturName";
                column.Caption = "Nama Debitur";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturLocation";
                column.Caption = "Lokasi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "BidangUsahaDebitur";
                column.Caption = "Bidang Usaha";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TotalAsetPerusahaan";
                column.Caption = "Total Aset";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "FeeNominal";
                column.Caption = "Fee Nominal";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "JenisJasa";
                column.Caption = "Jenis Jasa Rekanan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorLaporan";
                column.Caption = "No. Laporan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PenanggungJawab";
                column.Caption = "Penandatangan Laporan (Partner)";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalMulaiPekerjaan";
                column.Caption = "Tgl Terima Order";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalSelesaiPekerjaan";
                column.Caption = "Tgl. Selesai";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TahunBuku";
                column.Caption = "TahunBuku";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "UnitKerja";
                column.Caption = "Unit Kerja BSM";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PICBank";
                column.Caption = "PIC BSM";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Keterangan";
                column.Caption = "Catatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });            
            
            settings.SettingsExport.PageHeader.Left = "Daftar Pekerjaan KAP";
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_DaftarPekerjaanAS(string strFilterExp, string strJenisRekanan, bool IsPCP = true)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListPekerjaan";
            settings.CallbackRouteValues = new { Controller = "TrxDetailPekerjaan", Action = "XLS_PekerjaanByTypeOfRek" };

            settings.KeyFieldName = "IdDetailPekerjaan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            settings.Columns.Add(column =>
            {
                column.FieldName = "NamaAsuransi";
                column.Caption = "Nama Asuransi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "JenisAsuransi";
                column.Caption = "Jenis Asuransi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LabaRugiKomprehensif";
                column.Caption = "Laba Rugi Komprehensif";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "CadanganTeknis";
                column.Caption = "Cadangan Teknis";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "HasilInvestasi";
                column.Caption = "Hasil Investasi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LabaBeforeTax";
                column.Caption = "Laba Before Tax";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PremiNetto";
                column.Caption = "Premi Netto";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "RBCPercent";
                column.Caption = "RBC %";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TotalEkuitasSebelumnya";
                column.Caption = "Total Ekuitas Sebelumnya";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TotalEkuitasBerjalan";
                column.Caption = "Total Ekuitas Berjalan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "RataRataEkuitas";
                column.Caption = "Rata Rata Ekuitas";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Keterangan";
                column.Caption = "Catatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });

            settings.SettingsExport.PageHeader.Left = "Daftar Pekerjaan " + strJenisRekanan;
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_DaftarPekerjaanAS1M(string strFilterExp, string strJenisRekanan, bool IsPCP = true)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListPekerjaan";
            settings.CallbackRouteValues = new { Controller = "TrxDetailPekerjaan", Action = "XLS_PekerjaanByTypeOfRek" };

            settings.KeyFieldName = "IdDetailPekerjaan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            if (IsPCP)
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "RegistrationNumber";
                    column.Caption = "No. Reg";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 70;
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Name";
                    column.Caption = "Nama Rekanan";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 200;
                });
            }
            settings.Columns.Add(column =>
            {
                column.FieldName = "TahunLaporan";
                column.Caption = "Tahun";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "BulanLaporan";
                column.Caption = "Bulan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "IdSegmentation";
                column.Caption = "Bisnis Unit";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "IdRegion";
                column.Caption = "Kode Cabang";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NomorPolis";
                column.Caption = "Nomor Polis";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NamaDebitur";
                column.Caption = "Nama Debitur";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 200;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "ProdukAsuransi";
                column.Caption = "Produk Asuransi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NilaiPertanggungan";
                column.Caption = "NilaiPertanggungan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Premi";
                column.Caption = "Premi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "FeeBasedPercent";
                column.Caption = "Fee Based %";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 70;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "FeeBasedNominal";
                column.Caption = "Fee Based Nominal";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PPnNominal";
                column.Caption = "PPn Nominal";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PICGroup";
                column.Caption = "PIC Group";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "DirectIndirect";
                column.Caption = "Direct/Indirect";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Keterangan";
                column.Caption = "Catatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 200;
            });

            settings.SettingsExport.PageHeader.Left = "Daftar Pekerjaan Bulanan " + strJenisRekanan;
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_DaftarPekerjaanBLG(string strFilterExp, bool IsPCP = true)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListPekerjaan";
            settings.CallbackRouteValues = new { Controller = "TrxDetailPekerjaan", Action = "XLS_PekerjaanByTypeOfRek" };

            settings.KeyFieldName = "IdDetailPekerjaan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            if (IsPCP)
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "RegistrationNumber";
                    column.Caption = "No. Reg";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 70;
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Name";
                    column.Caption = "Nama Rekanan";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 200;
                });
            }
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturName";
                column.Caption = "Nama Debitur";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "JenisProperty";
                column.Caption = "Jenis Property";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Lokasi";
                column.Caption = "Lokasi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "JenisLelang";
                column.Caption = "Jenis Lelang";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NoPerjanjian";
                column.Caption = "Nomor SPK";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalPerjanjian";
                column.Caption = "Tanggal SPK";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NilaiLimitLelang";
                column.Caption = "Nilai Limit Lelang";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NilaiTransaksi";
                column.Caption = "Nilai Transaksi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "FeeImbalanJasa";
                column.Caption = "Fee Imbalan Jasa";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalMulai";
                column.Caption = "Tgl Terima Order";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "UnitKerjaBank";
                column.Caption = "Unit Kerja BSM";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PICBank";
                column.Caption = "PIC BSM";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Keterangan";
                column.Caption = "Catatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.SettingsExport.PageHeader.Left = "Daftar Pekerjaan Balai Lelang";
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
        public static GridViewSettings XLS_DaftarPekerjaanNOT(string strFilterExp, bool IsPCP = true)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvListPekerjaan";
            settings.CallbackRouteValues = new { Controller = "TrxDetailPekerjaan", Action = "XLS_PekerjaanByTypeOfRek" };

            settings.KeyFieldName = "IdDetailPekerjaan";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            if (IsPCP)
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "RegistrationNumber";
                    column.Caption = "No. Reg";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 70;
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Name";
                    column.Caption = "Nama Rekanan";
                    column.HeaderStyle.Font.Bold = true;
                    column.ExportWidth = 200;
                });
            }
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturName";
                column.Caption = "Nama Debitur";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "DebiturLocation";
                column.Caption = "Lokasi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "FeeNominal";
                column.Caption = "Fee Nominal";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LimitKreditDiOrder";
                column.Caption = "Limit Kredit";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "JenisJasa";
                column.Caption = "Jenis Jasa";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalMulaiPekerjaan";
                column.Caption = "Tgl Terima Order";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "TanggalSelesaiPekerjaan";
                column.Caption = "Tgl. Selesai";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "UnitKerja";
                column.Caption = "Unit Kerja BSM";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PICBank";
                column.Caption = "PIC BSM";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Keterangan";
                column.Caption = "Catatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });            
            
            settings.SettingsExport.PageHeader.Left = "Daftar Pekerjaan Notaris";
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }

        public static GridViewSettings XLS_NotarisDetailAll(string strFilterExp)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "gvNotarisDetailAll";
            settings.CallbackRouteValues = new { Controller = "TrxNotaris", Action = "XLS_NotarisDetailAll" };

            settings.KeyFieldName = "IdNotarisTabular";
            settings.Columns.Add(column =>
            {
                column.FieldName = "No.";
                column.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                column.Width = 10;
                column.Settings.AllowAutoFilter = DefaultBoolean.False;
            });
            settings.CustomColumnDisplayText = (sender, e) =>
            {
                if (e.Column.FieldName == "No.")
                {
                    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                }
            };
            settings.Columns.Add(column =>
            {
                column.FieldName = "Name";
                column.Caption = "Nama Notaris";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "IsNotarisKoperasi";
                column.Caption = "Koperasi";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 40;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "IsNotarisPasarModal";
                column.Caption = "Pasar Modal";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 40;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Remark";
                column.Caption = "Catatan";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "SKNotarisNumber";
                column.Caption = "No. SK";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "SKNotarisDate";
                column.Caption = "Tgl SK";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "SumpahNotarisNumber";
                column.Caption = "No. Sumpah";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "SumpahNotarisDate";
                column.Caption = "Tgl Sumpah";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "WilayahKerjaNotaris";
                column.Caption = "Wilayah Kerja";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "NotarisPensionDate";
                column.Caption = "Tgl Pensiun";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PPATSKNumber";
                column.Caption = "No. SK PPAT";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PPATSKDate";
                column.Caption = "Tgl SK PPAT";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PPATSumpahNumber";
                column.Caption = "No. Sumpah PPAT";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PPATSumpahDate";
                column.Caption = "Tgl Sumpah PPAT";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "WilayahKerjaPPAT";
                column.Caption = "Wilayah Kerja PPAT";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "PPATPensionDate";
                column.Caption = "Tgl Pensiun PPAT";
                column.HeaderStyle.Font.Bold = true;
                column.ExportWidth = 100;
            });

            settings.SettingsExport.PageHeader.Left = "Daftar Info Detail Notaris";
            settings.SettingsExport.ReportHeader = "Filter : " + strFilterExp;
            settings.SettingsExport.PageHeader.Font.Bold = true;
            settings.SettingsExport.PageHeader.Font.Size = 15;
            settings.SettingsExport.RightMargin = 150;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.TopMargin = 100;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.Styles.Row.Font.Name = "Verdana";
            settings.Styles.Row.Font.Size = 18;
            settings.Settings.ShowFilterRow = true;
            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowSelectCheckbox = true;
            settings.SettingsCookies.Enabled = true;
            settings.SettingsCookies.StoreFiltering = true;
            settings.SettingsCookies.CookiesID = "MyFilteringEx";
            return settings;
        }
    }
}