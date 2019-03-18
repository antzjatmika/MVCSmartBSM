using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using ApiHelper;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxTenagaAhliController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxTenagaAhli";
        string url = string.Empty;
         
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxTenagaAhliController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxTenagaAhli", SmartAPIUrl);
            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
               var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;

               var Employees = JsonConvert.DeserializeObject<List<trxTenagaAhli>>(responseData); 
 
                 return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhli>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _TenagaAhliByRek()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhli>>(responseData);
                return PartialView("_TenagaAhliByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByGuidHeader(int IdHeader)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByGuidHeader/" + IdHeader.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhli>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public ActionResult _GetByGuidHeader(Guid GuidHeader)
        {
            //string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;

            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByGuidHeader/" + GuidHeader.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxTenagaAhli>>(responseData);
                return PartialView(myData);
            }
            return View("Error");
        }

        public FilePathResult DownloadTempPekerjaan(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }

        //public ActionResult _PopulateTAFromFile()
        //{
        //    List<trxTenagaAhliTetapImp> myDataList = new List<trxTenagaAhliTetapImp>();
        //    return PartialView(myDataList);
        //}

        //private bool ImportXLS(Guid pgdPointer, int pintBarisMulai, int pintKolomMulai, string pstrFileName, string pstrFileExtension, string pstrFileLocation)
        //{
        //    bool bolReturn = false;
        //    DataSet ds = new DataSet();

        //    if (pstrFileExtension == ".xls" || pstrFileExtension == ".xlsx")
        //    {
        //        string excelConnectionString = string.Empty;
        //        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        //connection String for xls file format.
        //        if (pstrFileExtension == ".xls")
        //        {
        //            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //        }
        //        //connection String for xlsx file format.
        //        else if (pstrFileExtension == ".xlsx")
        //        {
        //            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pstrFileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        }
        //        //Create Connection to Excel work book and add oledb namespace
        //        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //        excelConnection.Open();
        //        DataTable dt = new DataTable();

        //        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //        if (dt == null)
        //        {
        //            return bolReturn;
        //        }

        //        String[] excelSheets = new String[dt.Rows.Count];
        //        int t = 0;
        //        //excel data saves in temp file here.
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            excelSheets[t] = row["TABLE_NAME"].ToString();
        //            t++;
        //        }
        //        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

        //        string query = string.Format("Select * from [{0}]", excelSheets[0]);
        //        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
        //        {
        //            dataAdapter.Fill(ds);
        //        }
        //    }
        //    for (int i = pintBarisMulai; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        string conn = ConfigurationManager.ConnectionStrings["DB_Connect"].ConnectionString;
        //        SqlConnection con = new SqlConnection(conn);

        //        string queryByParam = "INSERT INTO [dbo].[trxTenagaAhli]" +
        //            "([IdHeader],[IdRekanan],[NamaLengkap],[Jabatan],[MulaiMendudukiJabatan]" +
        //            ",[MulaiBekerjaSebagaiPenilai],[RiwayatPekerjaan],[LingkupPekerjaan],[AlumniPTNPTS],[JenjangPendidikan]" +
        //            ",[KeanggotaanMAPPI],[NoIjinPenilai],[KantorPusatCabang],[Catatan],[CreatedUser]" +
        //            ",[CreatedDate]) VALUES " +
        //            "( @Value1, @Value2, @Value3, @Value4, @Value5" +
        //            ", @Value6, @Value7, @Value8, @Value9, @Value10" +
        //            ", @Value11, @Value12, @Value13, @Value14, @Value15" +
        //            ", @Value16 )";

        //        SqlCommand cmd = new SqlCommand(queryByParam, con);
        //        cmd.Parameters.AddWithValue("@Value1", 0);
        //        cmd.Parameters.AddWithValue("@Value2", pgdPointer.ToString());
        //        cmd.Parameters.AddWithValue("@Value3", ds.Tables[0].Rows[i][pintKolomMulai].ToString());
        //        cmd.Parameters.AddWithValue("@Value4", ds.Tables[0].Rows[i][pintKolomMulai + 1].ToString());
        //        cmd.Parameters.AddWithValue("@Value5", ds.Tables[0].Rows[i][pintKolomMulai + 2].ToString());

        //        cmd.Parameters.AddWithValue("@Value6", ds.Tables[0].Rows[i][pintKolomMulai + 3].ToString());
        //        cmd.Parameters.AddWithValue("@Value7", ds.Tables[0].Rows[i][pintKolomMulai + 4].ToString());
        //        cmd.Parameters.AddWithValue("@Value8", ds.Tables[0].Rows[i][pintKolomMulai + 5].ToString());
        //        cmd.Parameters.AddWithValue("@Value9", ds.Tables[0].Rows[i][pintKolomMulai + 6].ToString());
        //        cmd.Parameters.AddWithValue("@Value10", ds.Tables[0].Rows[i][pintKolomMulai + 7].ToString());

        //        cmd.Parameters.AddWithValue("@Value11", ds.Tables[0].Rows[i][pintKolomMulai + 8].ToString());
        //        cmd.Parameters.AddWithValue("@Value12", ds.Tables[0].Rows[i][pintKolomMulai + 9].ToString());
        //        cmd.Parameters.AddWithValue("@Value13", ds.Tables[0].Rows[i][pintKolomMulai + 10].ToString());
        //        cmd.Parameters.AddWithValue("@Value14", "Catatan Kecil 1234 5 4321");
        //        cmd.Parameters.AddWithValue("@Value15", "Admin");

        //        cmd.Parameters.AddWithValue("@Value16", DateTime.Today);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    bolReturn = true;
        //    return bolReturn;
        //}

        //[HttpPost]
        //public ActionResult _PopulateTAFromFile(HttpPostedFileBase file)
        //{
        //    bool bolImported = false;
        //    int intBarisMulaiData = 0;
        //    int intKolomMulaiData = 0;
        //    try
        //    {
        //        intBarisMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["BarisDataXLS"]);
        //    }
        //    catch(Exception ex)
        //    {}
        //    try
        //    {
        //        intKolomMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["KolomDataXLS"]);
        //    }
        //    catch (Exception ex)
        //    {}
        //    Guid gdPointer = Guid.NewGuid();
        //    tokenContainer.XLSPointer = gdPointer;

        //    if (Request.Files["file"].ContentLength > 0)
        //    {
        //        string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
        //        string strFileName = Request.Files["file"].FileName;
        //        string fileLocation = Server.MapPath("~/Content/FileUpload/") + strFileName;
        //        if (System.IO.File.Exists(fileLocation))
        //        {
        //            System.IO.File.Delete(fileLocation);
        //        }
        //        Request.Files["file"].SaveAs(fileLocation);

        //        bolImported = ImportXLSHelper.ImportXLS(gdPointer, intBarisMulaiData, intKolomMulaiData, strFileName, fileExtension, fileLocation);
        //        ViewBag.Message = string.Format("Proses import {0}", "Berhasil");
        //        return PartialView("_AddEditTAhliHeader");
        //    }
        //    return View("Error");
        //}

        public ActionResult Create()
        {
            return View(new trxTenagaAhli());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxTenagaAhli Emp)
        {
            Emp.IdRekanan = (Guid)tokenContainer.IdRekananContact;
            HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(url,Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url+"/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myData = JsonConvert.DeserializeObject<trxTenagaAhli>(responseData);
 
                return View(myData);
            }
            return View("Error");
        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxTenagaAhli Emp)
        {
 
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url+"/" +id, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
 
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var myData = JsonConvert.DeserializeObject<trxTenagaAhli>(responseData);
 
                return View(myData);
            }
            return View("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxTenagaAhli Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}
