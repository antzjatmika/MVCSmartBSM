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
using System.Xml;
using System.Data.SqlClient;
using System.Linq;
using Omu.ValueInjecter;
using System.Web.Routing;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class trxDetailPekerjaanHeaderController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/trxDetailPekerjaanHeaderHeader";
        string url = string.Empty;
        string urlAS_1M = string.Empty;
        string urlDetail = string.Empty;
        string urlDetailTMP = string.Empty;
        string urlDetailAS_1MTMP = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public trxDetailPekerjaanHeaderController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxDetailPekerjaanHeader", SmartAPIUrl);
            urlAS_1M = string.Format("{0}/api/trxDetailPekerjaan", SmartAPIUrl);
            urlDetail = string.Format("{0}/api/TrxDetailPekerjaan", SmartAPIUrl);
            urlDetailTMP = string.Format("{0}/api/trxDetailPekerjaanTMP", SmartAPIUrl);
            urlDetailAS_1MTMP = string.Format("{0}/api/trxDetailPekerjaanAS_1MTMP", SmartAPIUrl);

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
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var Employees = JsonConvert.DeserializeObject<List<trxDetailPekerjaanHeader>>(responseData);
                return View(Employees);
            }
            return View("Error");
        }
        public async Task<ActionResult> _DeletePekerjaanHeader(Guid GuidHeader)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + GuidHeader.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetNotiInboxSentDraft", "TrxNotificationHeader");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> AddEditPekerjaanHeader(Guid GuidHeader)
        {
            trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();

            if ((int)tokenContainer.IdTypeOfRekanan == 4 || (int)tokenContainer.IdTypeOfRekanan == 5)
            {
                if (GuidHeader != null && GuidHeader != Guid.Empty)
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByGuidHeaderAS_1M/" + GuidHeader.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        myDataForm = JsonConvert.DeserializeObject<trxDetailPekerjaanHeaderForm>(responseData);
                        ViewBag.GuidHeader = GuidHeader;
                        ViewBag.JudulDaftarTenaga = "Data Pekerjaan Asuransi Bulanan";
                        return View("AddEditPekerjaanAS_1MHeader", myDataForm);
                    }
                }
                else
                {
                    myDataForm.IdPekerjaanHeader = -1;
                    ViewBag.GuidHeader = Guid.Empty;
                    return View("AddEditPekerjaanAS_1MHeader", myDataForm);
                }
            }
            else
            {
                if (GuidHeader != null && GuidHeader != Guid.Empty)
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByGuidHeader/" + GuidHeader.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        myDataForm = JsonConvert.DeserializeObject<trxDetailPekerjaanHeaderForm>(responseData);
                        ViewBag.GuidHeader = GuidHeader;
                        ViewBag.JudulDaftarTenaga = "Data Tenaga Ahli Tetap";
                        return View("AddEditPekerjaanHeader", myDataForm);
                    }
                }
                else
                {
                    myDataForm.IdPekerjaanHeader = -1;
                    ViewBag.GuidHeader = Guid.Empty;
                    return View("AddEditPekerjaanHeader", myDataForm);
                }
            }
            return View("Error");
        }

        public async Task<ActionResult> _AddEditPekerjaanAS_1MHeader(Guid GuidHeader)
        {
            trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();

            if (GuidHeader != null && GuidHeader != Guid.Empty)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByGuidHeaderAS_1M/" + GuidHeader.ToString());
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataForm = JsonConvert.DeserializeObject<trxDetailPekerjaanHeaderForm>(responseData);
                    ViewBag.GuidHeader = GuidHeader;
                    //ViewBag.JudulDaftarTenaga = "Data Pekerjaan Asuransi Bulanan";
                    return PartialView("_AddEditPekerjaanAS_1MHeader", myDataForm);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> AddEditPekerjaanHeader(trxDetailPekerjaanHeaderForm myDataForm)
        {
            string strXLSPointer = myDataForm.GuidHeader.ToString();
            string strReqToExecTransData = string.Empty;
            trxDetailPekerjaanHeader myData = new trxDetailPekerjaanHeader();
            myData.InjectFrom(myDataForm);
            //pindahkan data temporary import XLS ke fix table berdasarkan XLS pointer guid
            if (strXLSPointer != string.Empty)
            {
                if ((int)tokenContainer.IdTypeOfRekanan == 4 || (int)tokenContainer.IdTypeOfRekanan == 5)
                {
                    strReqToExecTransData = string.Format("{0}/ExecTransDataPekAS_1M/{1}", url, strXLSPointer);
                }
                else
                {
                    strReqToExecTransData = string.Format("{0}/ExecTransDataPek/{1}", url, strXLSPointer);
                }
                HttpResponseMessage responseMessageXLS = await client.GetAsync(strReqToExecTransData);

                if (!responseMessageXLS.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error");
                }
            }
            myData.JudulDokumen = string.Format("Diupload tanggal : {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            if (myData.IdPekerjaanHeader > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdPekerjaanHeader, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    if ((int)tokenContainer.SupervisorId > 0)
                    {
                        return RedirectToAction("RekananDetailedInfo", new RouteValueDictionary(
                                                new
                                                {
                                                    controller = "MstRekanan",
                                                    action = "RekananDetailedInfo",
                                                    IdRekanan = tokenContainer.IdRekananContact.ToString()
                                                ,
                                                    IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan
                                                }));
                    }
                    else
                    {
                        return RedirectToAction("GetByRekanan", "TrxDetailPekerjaanHeader");
                    }
                }
                return RedirectToAction("Error");
            }
            else
            {
                //myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    if ((int)tokenContainer.SupervisorId > 0)
                    {
                        return RedirectToAction("RekananDetailedInfo", new RouteValueDictionary(
                                                new
                                                {
                                                    controller = "MstRekanan",
                                                    action = "RekananDetailedInfo",
                                                    IdRekanan = tokenContainer.IdRekananContact.ToString()
                                                ,
                                                    IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan
                                                }));
                    }
                    else
                    {
                        return RedirectToAction("GetByRekanan", "TrxDetailPekerjaanHeader");
                    }
                }
                return RedirectToAction("Error");
            }
        }
        public async Task<ActionResult> _DelHeaderByRekanan(Guid PekHeader)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(String.Format("{0}/DelHeaderByRekanan/{1}/{2}", url, tokenContainer.IdRekananContact.ToString(), PekHeader.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                //var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                //var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanHeader>>(responseData);
                //return PartialView("_GetByRekanan", myData);
                //return RedirectToAction("RekananDetailedInfo", "MstRekanan");
                return RedirectToAction("RekananDetailedInfo", new RouteValueDictionary(
                                        new { controller = "MstRekanan", action = "RekananDetailedInfo", IdRekanan = tokenContainer.IdRekananContact.ToString()
                                            , IdTypeOfRekanan = (int) tokenContainer.IdTypeOfRekanan
                                        }));
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanHeader>>(responseData);
                return PartialView("_GetByRekanan", myData);
            }
            return View("Error");
        }
        public ActionResult _GetByRekananTab()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                //hanya dipanggil dari admin
                ViewBag.IsRekanan = true;
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanHeader>>(responseData);
                return PartialView("_GetByRekanan", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekanan_ASR()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanHeader>>(responseData);
                return PartialView("_GetByRekanan", myData);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult _GetByRekanan_ASR(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =
                                     System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
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
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(conn);
                    string query = "Insert into Person(Name,Email,Mobile) Values('" + ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return View();
        }
        public async Task<ActionResult> GetByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/GetByRekanan/{1}", url, tokenContainer.IdRekananContact.ToString()));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanHeader>>(responseData);
                return View("GetByRekanan", myData);
            }
            return View("Error");
        }
        public FilePathResult ListPekerjaanBulanan(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }
        public FilePathResult ListAsuransiBulanan(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }
        public ActionResult _PopulatePekerjaanFromFile(HttpPostedFileBase file, int IdPekerjaanHeader)
        {
            bool bolImported = false;
            int intBarisMulaiData = 0;
            int intKolomMulaiData = 0;

            try
            {
                intBarisMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["BarisDataXLS_Pek"]);
            }
            catch (Exception ex)
            { }
            try
            {
                intKolomMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["KolomDataXLS_Pek"]);
            }
            catch (Exception ex)
            { }
            Guid gdPointer = Guid.NewGuid();

            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                string strFileName = gdPointer.ToString() + fileExtension;
                string fileLocation = Server.MapPath("~/Content/FileUpload/") + strFileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["file"].SaveAs(fileLocation);

                bolImported = ImportXLSHelper.ImportXLS(4, gdPointer, (Guid)tokenContainer.IdRekananContact, intBarisMulaiData, intKolomMulaiData, strFileName, fileExtension, fileLocation);
                ViewBag.Message = string.Format("Proses import {0}", "Berhasil");
                //populate form header and grid from temp table
                trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();

                //populate grid data
                List<trxDetailPekerjaan> myDataList = new List<trxDetailPekerjaan>();
                HttpResponseMessage responseMessage = client.GetAsync(urlDetailTMP + "/GetByGuidHeader/" + gdPointer.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataList = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                }
                myDataForm.PekerjaanColls = myDataList;

                myDataForm.IdPekerjaanHeader = IdPekerjaanHeader;
                myDataForm.GuidHeader = gdPointer;
                myDataForm.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                ViewBag.GuidHeader = gdPointer;
                return View("AddEditPekerjaanHeader", myDataForm);
            }
            return View("Error");
        }

        public ActionResult _PopulatePekerjaanFromFileAS_1M(HttpPostedFileBase file, int IdPekerjaanHeader)
        {
            bool bolImported = false;
            int intBarisMulaiData = 0;
            int intKolomMulaiData = 0;

            try
            {
                intBarisMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["BarisDataXLS_Pek"]);
            }
            catch (Exception ex)
            { }
            try
            {
                intKolomMulaiData = Convert.ToInt16(ConfigurationManager.AppSettings["KolomDataXLS_Pek"]);
            }
            catch (Exception ex)
            { }
            Guid gdPointer = Guid.NewGuid();

            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                string strFileName = gdPointer.ToString() + fileExtension;
                string fileLocation = Server.MapPath("~/Content/FileUpload/") + strFileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["file"].SaveAs(fileLocation);

                bolImported = ImportXLSHelper.ImportXLS(5, gdPointer, (Guid)tokenContainer.IdRekananContact, intBarisMulaiData, intKolomMulaiData, strFileName, fileExtension, fileLocation);
                ViewBag.Message = string.Format("Proses import {0}", "Berhasil");
                //populate form header and grid from temp table
                trxDetailPekerjaanHeaderForm myDataForm = new trxDetailPekerjaanHeaderForm();
                //List<trxDetailPekerjaanAS_1M> myDataList = new List<trxDetailPekerjaanAS_1M>();
                HttpResponseMessage responseMessage = client.GetAsync(urlDetailAS_1MTMP + "/GetByGuidHeader/" + gdPointer.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myDataForm = JsonConvert.DeserializeObject<trxDetailPekerjaanHeaderForm>(responseData);
                }
                
                myDataForm.IdPekerjaanHeader = IdPekerjaanHeader;
                myDataForm.GuidHeader = gdPointer;
                myDataForm.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                ViewBag.GuidHeader = gdPointer;
                return View("AddEditPekerjaanAS_1MHeader", myDataForm);
            }
            return View("Error");
        }

        public async Task<ActionResult> _Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<trxDetailPekerjaanHeader>(responseData);

                return View(Employee);
            }
            return View("Error");
        }

    }
}
