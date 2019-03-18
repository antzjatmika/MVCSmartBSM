using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Newtonsoft.Json;
//using MVCSmartClient01.Models;
//using ApiHelper;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
//using System.IO;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class ImportXLSHelper
    {
        public static bool ImportXLS_Old(int pintJenisData, Guid pgdPointer, Guid pgdIdRekanan, int pintBarisMulai, int pintKolomMulai, string pstrFileName, string pstrFileExtension, string pstrFileLocation)
        {
            bool bolReturn = false;
            DataSet ds = new DataSet();

            if (pstrFileExtension == ".xls" || pstrFileExtension == ".xlsx")
            {
                string excelConnectionString = string.Empty;
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                //connection String for xls file format.
                if (pstrFileExtension == ".xls")
                {
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                //connection String for xlsx file format.
                else if (pstrFileExtension == ".xlsx")
                {
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                //Create Connection to Excel work book and add oledb namespace
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                DataTable dt = new DataTable();

                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return bolReturn;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                //excel data saves in temp file here.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                {
                    dataAdapter.Fill(ds);
                }
            }

            switch (pintJenisData)
            {
                case 1:

                    #region INSERT TENAGA AHLI

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxTenagaAhliTMP]" +
                            "([GuidHeader],[IdRekanan],[NamaLengkap],[Jabatan],[MulaiMendudukiJabatan]" +
                            ",[MulaiBekerjaSebagaiPenilai],[RiwayatPekerjaan],[LingkupPekerjaan],[AlumniPTNPTS],[JenjangPendidikan]" +
                            ",[KeanggotaanMAPPI],[NoIjinPenilai],[KantorPusatCabang],[Catatan],[CreatedUser]" +
                            ",[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
                            ", @Value16 )";

                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
                        cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
                        cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
                        cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());
                        cmd.Parameters.AddWithValue("@Value14", "Catatan Kecil 1234 5 4321");
                        cmd.Parameters.AddWithValue("@Value15", "Admin");

                        cmd.Parameters.AddWithValue("@Value16", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 3:

                    #region INSERT TENAGA PENDUKUNG

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxTenagaPendukungTMP]" +
                            "([GuidHeader],[IdRekanan],[Name],[Alamat],[NomorKTP]" +
                            ",[CreatedUser],[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7)";

                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
                        cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());

                        cmd.Parameters.AddWithValue("@Value6", "Admin");
                        cmd.Parameters.AddWithValue("@Value7", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 4:

                    #region INSERT DETAIL PEKERJAAN

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxDetailPekerjaanTMP]" +
                            "([GuidHeader],[IdRekanan],[IdRegion],[IdSegmentasi],[TahunLaporan]" +
                            ",[BulanLaporan],[DebiturName],[DebiturLocation],[BidangUsahaDebitur],[JenisJasa]" +
                            ",[PenanggungJawab],[NamaPemberiPekerjaan],[UnitKerja],[NilaiPenutupan],[TanggalMulaiPekerjaan]" +
                            ",[TanggalSelesaiPekerjaan],[NomorPolis],[PICRekanan],[PICBank],[Keterangan]" +
                            ",[CreatedUser],[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
                            ", @Value16, @Value17, @Value18, @Value19, @Value20" +
                            ", @Value21, @Value22)";
                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", 1);
                        cmd.Parameters.AddWithValue("@Value4", 1);
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());
                        cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
                        cmd.Parameters.AddWithValue("@Value14", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
                        cmd.Parameters.AddWithValue("@Value15", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());

                        cmd.Parameters.AddWithValue("@Value16", ds.Tables[0].Rows[i][pintKolomMulai + 11].ToString());
                        cmd.Parameters.AddWithValue("@Value17", ds.Tables[0].Rows[i][pintKolomMulai + 12].ToString());
                        cmd.Parameters.AddWithValue("@Value18", ds.Tables[0].Rows[i][pintKolomMulai + 13].ToString());
                        cmd.Parameters.AddWithValue("@Value19", ds.Tables[0].Rows[i][pintKolomMulai + 14].ToString());
                        cmd.Parameters.AddWithValue("@Value20", ds.Tables[0].Rows[i][pintKolomMulai + 15].ToString());
                        
                        cmd.Parameters.AddWithValue("@Value21", "Admin");
                        cmd.Parameters.AddWithValue("@Value22", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 5:

                    #region INSERT DETAIL PEKERJAAN ASURANSI Per Month

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxDetailPekerjaanAS_1MTMP]" +
                            "([GuidHeader],[IdRekanan],[TahunLaporan],[BulanLaporan],[BisnisUnit]" +
                            ",[Cabang],[NomorPolis],[NamaDebitur],[ProdukAsuransi],[Premi]" +
                            ",[FeeBasedPercent],[FeeBasedNominal],[PPnNominal],[PICGroup],[DirectIndirect]" +
                            ",[Keterangan],[CreatedUser],[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
                            ", @Value16, @Value17, @Value18)";
                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
                        cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
                        cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
                        cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());
                        cmd.Parameters.AddWithValue("@Value14", ds.Tables[0].Rows[i][pintKolomMulai + 11].ToString());
                        cmd.Parameters.AddWithValue("@Value15", ds.Tables[0].Rows[i][pintKolomMulai + 12].ToString());

                        cmd.Parameters.AddWithValue("@Value16", ds.Tables[0].Rows[i][pintKolomMulai + 13].ToString());
                        cmd.Parameters.AddWithValue("@Value17", "Admin");
                        cmd.Parameters.AddWithValue("@Value18", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 6:

                    #region INSERT BRANCH OFFICE

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxBranchOfficeTMP] " +
                            "([GuidHeader],[IdRekanan],[IdOrganisasi],[BranchType],[Name] " +
                            ", [Address],[Email1],[Telephone1],[Telephone2],[Fax1] " +
                            ", [ZipCode],[CreatedUser],[CreatedDate],[IsActive])" +
                            " VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14)";
                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", 1);
                        cmd.Parameters.AddWithValue("@Value4", 1);
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value12", "Admin");
                        cmd.Parameters.AddWithValue("@Value13", DateTime.Today);
                        cmd.Parameters.AddWithValue("@Value14", 1);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
            }

            bolReturn = true;
            return bolReturn;
        }

        public static bool ImportXLS(int pintJenisData, Guid pgdPointer, Guid pgdIdRekanan, int pintBarisMulai, int pintKolomMulai, string pstrFileName, string pstrFileExtension, string pstrFileLocation)
        {
            bool bolReturn = false;
            DataSet ds = new DataSet();
            string strNomor = string.Empty;
            int intNomor = 0;
            string strSegmentasi = string.Empty;
            int intSegmentasi = 0;
            string strCabang = string.Empty;
            int intCabang = 0;

            if (pstrFileExtension == ".xls" || pstrFileExtension == ".xlsx")
            {
                string excelConnectionString = string.Empty;
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                //connection String for xls file format.
                if (pstrFileExtension == ".xls")
                {
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                //connection String for xlsx file format.
                else if (pstrFileExtension == ".xlsx")
                {
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
                {
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return bolReturn;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                ////Create Connection to Excel work book and add oledb namespace
                //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                //excelConnection.Open();
                //DataTable dt = new DataTable();

                //dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //if (dt == null)
                //{
                //    return bolReturn;
                //}

                //String[] excelSheets = new String[dt.Rows.Count];
                //int t = 0;
                ////excel data saves in temp file here.
                //foreach (DataRow row in dt.Rows)
                //{
                //    excelSheets[t] = row["TABLE_NAME"].ToString();
                //    t++;
                //}
                //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                //{
                //    dataAdapter.Fill(ds);
                //}
            }

            switch (pintJenisData)
            {
                case 1:

                    #region INSERT TENAGA AHLI

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxTenagaAhliTMP]" +
                            "([GuidHeader],[IdRekanan],[NamaLengkap],[Jabatan],[MulaiMendudukiJabatan]" +
                            ",[MulaiBekerjaSebagaiPenilai],[RiwayatPekerjaan],[LingkupPekerjaan],[AlumniPTNPTS],[JenjangPendidikan]" +
                            ",[KeanggotaanMAPPI],[NoIjinPenilai],[KantorPusatCabang],[Catatan],[CreatedUser]" +
                            ",[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
                            ", @Value16 )";

                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
                        cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
                        cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
                        cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());
                        cmd.Parameters.AddWithValue("@Value14", "Catatan Kecil 1234 5 4321");
                        cmd.Parameters.AddWithValue("@Value15", "Admin");

                        cmd.Parameters.AddWithValue("@Value16", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 3:

                    #region INSERT TENAGA PENDUKUNG

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxTenagaPendukungTMP]" +
                            "([GuidHeader],[IdRekanan],[Name],[NomorKTP],[Alamat]" +
                            ",[CreatedUser],[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7)";

                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
                        cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());

                        cmd.Parameters.AddWithValue("@Value6", "Admin");
                        cmd.Parameters.AddWithValue("@Value7", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 4:

                    #region INSERT DETAIL PEKERJAAN

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxDetailPekerjaanTMP]" +
                            "([GuidHeader],[IdRekanan],[TahunLaporan],[BulanLaporan],[IdSegmentation]" +
                            ",[IdRegion],[DebiturName],[DebiturLocation],[BidangUsahaDebitur],[JenisJasa]" +
                            ",[PenanggungJawab],[NamaPemberiPekerjaan],[UnitKerja],[NilaiPenutupan],[TanggalMulaiPekerjaan]" +
                            ",[TanggalSelesaiPekerjaan],[NomorPolis],[PICRekanan],[PICBank],[Keterangan]" +
                            ",[CreatedUser],[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
                            ", @Value16, @Value17, @Value18, @Value19, @Value20" +
                            ", @Value21, @Value22)";
                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", 1);
                        cmd.Parameters.AddWithValue("@Value4", 1);
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());
                        cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
                        cmd.Parameters.AddWithValue("@Value14", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
                        cmd.Parameters.AddWithValue("@Value15", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());

                        cmd.Parameters.AddWithValue("@Value16", ds.Tables[0].Rows[i][pintKolomMulai + 11].ToString());
                        cmd.Parameters.AddWithValue("@Value17", ds.Tables[0].Rows[i][pintKolomMulai + 12].ToString());
                        cmd.Parameters.AddWithValue("@Value18", ds.Tables[0].Rows[i][pintKolomMulai + 13].ToString());
                        cmd.Parameters.AddWithValue("@Value19", ds.Tables[0].Rows[i][pintKolomMulai + 14].ToString());
                        cmd.Parameters.AddWithValue("@Value20", ds.Tables[0].Rows[i][pintKolomMulai + 15].ToString());

                        cmd.Parameters.AddWithValue("@Value21", "Admin");
                        cmd.Parameters.AddWithValue("@Value22", DateTime.Today);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
                case 5:

                    #region INSERT DETAIL PEKERJAAN ASURANSI Per Month

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxDetailPekerjaanAS_1MTMP]" +
                            "([GuidHeader],[IdRekanan],[TahunLaporan],[BulanLaporan],[IdSegmentation]" +
                            ",[IdRegion],[NomorPolis],[NamaDebitur],[ProdukAsuransi],[NilaiPertanggungan]" +
                            ",[Premi],[FeeBasedPercent],[FeeBasedNominal],[PPnNominal],[PICGroup]" +
                            ",[DirectIndirect],[Keterangan],[CreatedUser],[CreatedDate]) VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
                            ", @Value16, @Value17, @Value18, @Value19)";
                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());

                        strNomor = ds.Tables[0].Rows[i][pintKolomMulai].ToString();
                        if (string.IsNullOrEmpty(strNomor))
                        {
                            intNomor = 0;
                        }
                        else
                        {
                            try
                            {
                                intNomor = Int32.Parse(strNomor.Replace(".", ""));
                            }
                            catch (FormatException e)
                            {
                                intNomor = 0;
                            }
                        }
                        if (intNomor > 0)
                        {
                            cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
                            cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                            //cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());
                            //OBTAIN INTEGER PART -- SEGMENTASI
                            strSegmentasi = ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString();
                            if (string.IsNullOrEmpty(strSegmentasi))
                            {
                                intSegmentasi = 0;
                            }
                            else
                            {
                                try
                                {
                                    intSegmentasi = Int32.Parse(strSegmentasi.Substring(1, 1));
                                }
                                catch (FormatException e)
                                {
                                    intSegmentasi = 0;
                                }
                            }
                            cmd.Parameters.AddWithValue("@Value5", intSegmentasi);

                            //OBTAIN INTEGER PART -- CABANG / REGION
                            strCabang = ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString();
                            if (string.IsNullOrEmpty(strCabang))
                            {
                                intCabang = 0;
                            }
                            else
                            {
                                try
                                {
                                    intCabang = Int32.Parse(strCabang.Substring(1, 2));
                                }
                                catch (FormatException e)
                                {
                                    intCabang = 0;
                                }
                            }
                            cmd.Parameters.AddWithValue("@Value6", intCabang);
                            cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                            cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());
                            cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                            cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());

                            cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
                            cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
                            cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());
                            cmd.Parameters.AddWithValue("@Value14", ds.Tables[0].Rows[i][pintKolomMulai + 11].ToString());
                            cmd.Parameters.AddWithValue("@Value15", ds.Tables[0].Rows[i][pintKolomMulai + 12].ToString());

                            cmd.Parameters.AddWithValue("@Value16", ds.Tables[0].Rows[i][pintKolomMulai + 13].ToString());
                            cmd.Parameters.AddWithValue("@Value17", ds.Tables[0].Rows[i][pintKolomMulai + 14].ToString());
                            cmd.Parameters.AddWithValue("@Value18", "Admin");
                            cmd.Parameters.AddWithValue("@Value19", DateTime.Today);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    #endregion

                    break;
                case 6:

                    #region INSERT BRANCH OFFICE

                    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);

                        string queryByParam = "INSERT INTO [dbo].[trxBranchOfficeTMP] " +
                            "([GuidHeader],[IdRekanan],[IdOrganisasi],[BranchType],[Name] " +
                            ", [Address],[Email1],[Telephone1],[Telephone2],[Fax1] " +
                            ", [ZipCode],[CreatedUser],[CreatedDate],[IsActive])" +
                            " VALUES " +
                            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
                            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
                            ", @Value11, @Value12, @Value13, @Value14)";
                        SqlCommand cmd = new SqlCommand(queryByParam, con);
                        cmd.Parameters.AddWithValue("@Value1", pgdPointer.ToString());
                        cmd.Parameters.AddWithValue("@Value2", pgdIdRekanan.ToString());
                        cmd.Parameters.AddWithValue("@Value3", 1);
                        cmd.Parameters.AddWithValue("@Value4", 1);
                        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai].ToString());

                        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
                        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());
                        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
                        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
                        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());

                        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
                        cmd.Parameters.AddWithValue("@Value12", "Admin");
                        cmd.Parameters.AddWithValue("@Value13", DateTime.Today);
                        cmd.Parameters.AddWithValue("@Value14", 1);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    #endregion

                    break;
            }

            bolReturn = true;
            return bolReturn;
        }

    }
}
