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
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Data.Filtering;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxDetailPekerjaanController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxDetailPekerjaan";
        string urlKJP_KMG = string.Empty;
        string urlKAP = string.Empty;
        string urlBLG = string.Empty;
        string urlNOT = string.Empty;
        string urlAS = string.Empty;
        string urlAS1M = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxDetailPekerjaanController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];

            urlKJP_KMG = string.Format("{0}/api/TrxDetailPekerjaan", SmartAPIUrl);
            urlKAP = string.Format("{0}/api/TrxDetailPekerjaan", SmartAPIUrl);
            urlNOT = string.Format("{0}/api/TrxDetailPekerjaan", SmartAPIUrl);

            urlBLG = string.Format("{0}/api/TrxDetailPekerjaanBLG", SmartAPIUrl);
            urlAS = string.Format("{0}/api/TrxDetailPekerjaanAS_3M", SmartAPIUrl);
            urlAS1M = string.Format("{0}/api/TrxDetailPekerjaanAS_1M", SmartAPIUrl);

            client = new HttpClient();
            tokenContainer = new TokenContainer();
            client.BaseAddress = new Uri(urlKJP_KMG);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }
 
        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData =   responseMessage.Content.ReadAsStringAsync().Result ;
                var Employees = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData); 
                return View(Employees);
        }
            return View("Error");
        }
        //public async Task<ActionResult> _GetByRekananOld()
        //{
        //    trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
        //    //1	Kantor Jasa Penilai Publik (KJPP)
        //    //2	Kantor Akuntan Publik (KAP)
        //    //3	Konsultan Manajemen
        //    //4	Asuransi Jiwa
        //    //5	Asuransi Kerugian
        //    //6	Balai Lelang
        //    //7	Notaris
        //    switch ((int)tokenContainer.IdTypeOfRekanan)
        //    {
        //        case 7:
        //            myDataForm.IsNotaris = true;
        //            break;
        //        case 4:
        //        case 5:
        //            myDataForm.IsAsuransi = true;
        //            break;
        //        default:
        //            myDataForm.IsGrupNonNotaris = true;
        //            break;
        //    }
        //    HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
        //        myDataForm.DetailPekerjaanColls = myData;

        //        return PartialView("_GetByRekanan", myDataForm);
        //    }
        //    return View("Error");
        //}

        public async Task<ActionResult> _GetByRekanan_ASR()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
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
        public async Task<ActionResult> GetByTypeKJP()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            ViewBag.CallBackAction = "GetByTypeKJP";

            responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeKJP";
                ViewBag.RInfoVisible = "true";
                ViewBag.IdTypeOfRekanan = 1;

                return View("GetByTypeKJP", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeKJP()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            ViewBag.CallBackAction = "GetByTypeKJP";

            responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeKJP";
                ViewBag.RInfoVisible = "true";
                ViewBag.IdTypeOfRekanan = 1;

                return PartialView("_GetByRekananKJP", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByTypeKAP()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";

            responseMessage = await client.GetAsync(urlKAP + "/PekerjaanByTypeOfRekanan/2");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                ViewBag.ActionPartial = "_GetByTypeKAP";
                ViewBag.RInfoVisible = "true";

                return View("GetByTypeKAP", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeKAP()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";

            responseMessage = await client.GetAsync(urlKAP + "/PekerjaanByTypeOfRekanan/2");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                ViewBag.ActionPartial = "_GetByTypeKAP";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananKAP", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByTypeKMG()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            ViewBag.CallBackAction = "GetByTypeKMG";

            responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/3");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeKMG";
                ViewBag.RInfoVisible = "true";

                return View("GetByTypeKMG", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeKMG()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            ViewBag.CallBackAction = "GetByTypeKMG";

            responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/3");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeKMG";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananKJP", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByTypeASJ()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;

            responseMessage = await client.GetAsync(urlAS + "/PekerjaanByTypeOfRekanan/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanAS_3MColls = myData;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeASJ";
                ViewBag.RInfoVisible = "true";

                return View("GetByTypeASJ", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeASJ()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;

            responseMessage = await client.GetAsync(urlAS + "/PekerjaanByTypeOfRekanan/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanAS_3MColls = myData;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeASJ";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananAS", myDataForm);
            }
            return View("Error");
        }

        public async Task<ActionResult> GetByTypeASJ1M()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            //trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            responseMessage = await client.GetAsync(urlAS1M + "/PekerjaanByTypeOfRekanan/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_1MMulti>(responseData);
                ViewBag.RInfoVisible = "true";
                ViewBag.IdTypeOfRekanan = 4;
                return View("GetByTypeASJ1M", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByTypeASK1M()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            //trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            responseMessage = await client.GetAsync(urlAS1M + "/PekerjaanByTypeOfRekanan/5");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_1MMulti>(responseData);
                //ViewBag.ActionPartial = "_GetByTypeASK1M";
                ViewBag.RInfoVisible = "true";
                ViewBag.IdTypeOfRekanan = 5;

                return View("GetByTypeASK1M", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeASJ1M()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await client.GetAsync(urlAS1M + "/PekerjaanByTypeOfRekanan/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_1MMulti>(responseData);
                ViewBag.RInfoVisible = "true";
                ViewBag.IdTypeOfRekanan = 4;

                return PartialView("_GetByRekananAS_1M", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeASK1M()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await client.GetAsync(urlAS1M + "/PekerjaanByTypeOfRekanan/5");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_1MMulti>(responseData);
                ViewBag.RInfoVisible = "true";
                ViewBag.IdTypeOfRekanan = 5;

                return PartialView("_GetByRekananAS_1M", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekananAS_1M(int IdTypeOfRekanan)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            //responseMessage = await client.GetAsync(urlAS1M + "/PekerjaanByTypeOfRekanan/5");
            responseMessage = await client.GetAsync( String.Format("{0}/PekerjaanByTypeOfRekanan/{1}", urlAS1M, IdTypeOfRekanan));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_1MMulti>(responseData);
                //ViewBag.ActionPartial = "_GetByTypeASK1M";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananAS_1M", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByTypeASK()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;

            responseMessage = await client.GetAsync(urlAS + "/PekerjaanByTypeOfRekanan/5");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fGetPekerjaan3MByIdTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanAS_3MPCPColls = myData;
                myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;
                myDataForm.TingkatResikoColls = DataMasterProvider.Instance.TingkatResikoColls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeASK";
                ViewBag.RInfoVisible = "true";

                return View("GetByTypeASK", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeASK()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";
            myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;

            responseMessage = await client.GetAsync(urlAS + "/PekerjaanByTypeOfRekanan/5");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fGetPekerjaan3MByIdTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanAS_3MPCPColls = myData;
                myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;
                myDataForm.TingkatResikoColls = DataMasterProvider.Instance.TingkatResikoColls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeASK";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananAS_PCP", myDataForm);
            }
            return View("Error");
        }

        public async Task<ActionResult> GetByTypeBLG()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";

            responseMessage = await client.GetAsync(urlBLG + "/PekerjaanByTypeOfRekanan");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanBLGColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeBLG";
                ViewBag.RInfoVisible = "true";

                return View("GetByTypeBLG", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeBLG()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";

            responseMessage = await client.GetAsync(urlBLG + "/PekerjaanByTypeOfRekanan");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanBLGColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetByTypeBLG";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananBLG", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByTypeNOT()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";

            responseMessage = await client.GetAsync(urlNOT + "/PekerjaanByTypeOfRekanan/7");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls; 
                ViewBag.ActionPartial = "_GetByTypeNOT";
                ViewBag.RInfoVisible = "true";

                return View("GetByTypeNOT", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByTypeNOT()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();

            ViewBag.IsPcp = true;
            ViewBag.ButSimpan = "hidden";
            ViewBag.ButBatal = "hidden";
            ViewBag.ButTutup = "visible";

            responseMessage = await client.GetAsync(urlNOT + "/PekerjaanByTypeOfRekanan/7");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls; 
                ViewBag.ActionPartial = "_GetByTypeNOT";
                ViewBag.RInfoVisible = "true";

                return PartialView("_GetByRekananNOT", myDataForm);
            }
            return View("Error");
        }

        private string[] ParseFilterEx(string strFilterExpression)
        {
            int intAwal = 0;
            int intAkhir = 0;
            string strDefaultAll = "1 = 1";
            string strReturn1 = string.Empty;
            string strReturn2 = string.Empty;
            string[] arrReturn = new string[] { };
            int intLengthWiTgl = "[TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05'".Length;

            //Contains([DebiturLocation], 'an');
            //[TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05'
            //[TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05' And Contains([NomorLaporan], 'lap')
            //Contains([DebiturLocation], 'an') And [TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05'
            //Contains([DebiturLocation], 'an') And [TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05' And Contains([NomorLaporan], 'lap')

            if(string.IsNullOrEmpty(strFilterExpression))
            {
                strFilterExpression = "1 = 1";
            }
            if(strFilterExpression.Contains("[TanggalSelesaiPekerjaan]$M") && strFilterExpression.Contains("[TanggalSelesaiPekerjaan]$L"))
            {
                intAwal = strFilterExpression.IndexOf("[TanggalSelesaiPekerjaan]$M");
                intAkhir = strFilterExpression.IndexOf("[TanggalSelesaiPekerjaan]$L");
            }
            else
            {
                intAwal = -1;
            }

            if(intAwal < 0)
            {
                strReturn1 = strFilterExpression;
                strReturn2 = strDefaultAll;
            }
            if (intAwal == 0 && strFilterExpression.Length <= intLengthWiTgl)
            {
                strReturn1 = strDefaultAll;
                strReturn2 = strFilterExpression;
            }
            else if (intAwal == 0 && strFilterExpression.Length > intLengthWiTgl)
            {
                strReturn1 = strFilterExpression.Substring(intLengthWiTgl + 5, strFilterExpression.Length - (intLengthWiTgl + 5));
                strReturn2 = strFilterExpression.Substring(0, intLengthWiTgl);
            }
            else if (intAwal > 0 && strFilterExpression.Length <= intAwal + intLengthWiTgl + 5)
            {
                strReturn1 = strFilterExpression.Substring(0, intAwal - 5);
                strReturn2 = strFilterExpression.Substring(intAwal, intLengthWiTgl);
            }
            else if(intAwal > 0)
            {
                strReturn1 = strFilterExpression.Substring(0, intAwal - 5) + strFilterExpression.Substring(intAwal + intLengthWiTgl, strFilterExpression.Length - (intAwal + intLengthWiTgl));
                strReturn2 = strFilterExpression.Substring(intAwal, intLengthWiTgl);
            }
            if(strReturn2 != "1 = 1")
            {
                int intTglAwal = strReturn2.IndexOf("$M");
                int intTglAkhir = strReturn2.IndexOf("$L");
                int intTahun1 = Convert.ToInt16(strReturn2.Substring(intTglAwal + 2, 4));
                int intBulan1 = Convert.ToInt16(strReturn2.Substring(intTglAwal + 7, 2));
                int intTanggal1 = Convert.ToInt16(strReturn2.Substring(intTglAwal + 10, 2));
                int intTahun2 = Convert.ToInt16(strReturn2.Substring(intTglAkhir + 2, 4));
                int intBulan2 = Convert.ToInt16(strReturn2.Substring(intTglAkhir + 7, 2));
                int intTanggal2 = Convert.ToInt16(strReturn2.Substring(intTglAkhir + 10, 2));
                strReturn2 = "TanggalSelesaiPekerjaan ME DateTime(" + intTahun1.ToString() + "," + intBulan1.ToString() + "," + intTanggal1.ToString() + ")" +
                    " NN TanggalSelesaiPekerjaan LE DateTime(" + intTahun2.ToString() + "," + intBulan2.ToString() + "," + intTanggal2.ToString() + ")";
            }
            arrReturn = new string[] { strReturn1, strReturn2 };
            return arrReturn;
        }
        public async Task<ActionResult> XLS_ListPekerjaanKJP(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            //strFilterExp1	"TanggalSelesaiPekerjaan >= DateTime(2001,1,1) && TanggalSelesaiPekerjaan <= DateTime(2018,1,1)"
            //string str0 = "Contains([DebiturLocation], 'an')";
            //string strA = "[TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05'";
            //string strB = "[TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05' And Contains([NomorLaporan], 'lap')";
            //string strC = "Contains([DebiturLocation], 'an') And [TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05'";
            //string strD = "Contains([DebiturLocation], 'an') And [TanggalSelesaiPekerjaan]$M1996-01-01' And [TanggalSelesaiPekerjaan]$L2017-09-05' And Contains([NomorLaporan], 'lap')";
            //arrResult = ParseFilterEx(str0);
            //arrResult = ParseFilterEx(strA);
            //arrResult = ParseFilterEx(strB);
            //arrResult = ParseFilterEx(strC);
            //arrResult = ParseFilterEx(strD);

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/1/{1}/{2}", urlKJP_KMG, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanKJP(strFilterExpression, "KJPP"), myData, "XLSPekerjaanKJP", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanKAP(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/2/{1}/{2}", urlKAP, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanKAP(strFilterExpression), myData, "XLSPekerjaanKAP", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanKMG(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/3/{1}/{2}", urlKJP_KMG, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanKJP(strFilterExpression, "Konsultan Management"), myData, "XLSPekerjaanKMG", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanASJ(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/4/{1}/{2}", urlAS, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanAS(strFilterExpression, "Asuransi Jiwa"), myData, "XLSPekerjaanASJ", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanASK(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/5/{1}/{2}", urlAS, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanAS(strFilterExpression, "Asuransi Kerugian"), myData, "XLSPekerjaanASK", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanASJ1M(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/4/{1}/{2}", urlAS1M, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanAS1M(strFilterExpression, "Asuransi Jiwa"), myData, "XLSPekerjaanASJ1M", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanASK1M(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/5/{1}/{2}", urlAS1M, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanAS_1MByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanAS1M(strFilterExpression, "Asuransi Kerugian"), myData, "XLSPekerjaanASK1M", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanBLG(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/{1}/{2}", urlBLG, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanBLG(strFilterExpression), myData, "XLSPekerjaanBLG", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> XLS_ListPekerjaanNOT(string strFilterExpression)
        {
            string[] arrResult = new string[] { };

            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/XLS_PekerjaanByTypeOfRek/7/{1}/{2}", urlNOT, arrResult[0], arrResult[1]));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                XlsExportOptions xlsOption = new XlsExportOptions();
                GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanNOT(strFilterExpression), myData, "XLSPekerjaanNOT", xlsOption);

                return new EmptyResult();
            }
            return View("Error");
        }
        public async Task<ActionResult> _PekerjaanByTypeOfRekanan(int pIdTypeOfRekanan)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            switch (pIdTypeOfRekanan)
            {
                case 1:
                case 3:
                    responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;

                        return PartialView("_GetByRekananKJP", myDataForm);
                    }
                    return View("Error");
                case 2:
                    responseMessage = await client.GetAsync(urlKAP + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;

                        return PartialView("_GetByRekananKAP", myDataForm);
                    }
                    return View("Error");
                case 4:
                case 5:
                    responseMessage = await client.GetAsync(urlAS + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanAS_3MColls = myData;

                        return PartialView("_GetByRekananAS", myDataForm);
                    }
                    return View("Error");
                case 6:
                    responseMessage = await client.GetAsync(urlBLG + "/PekerjaanByTypeOfRekanan");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanBLGColls = myData;

                        return PartialView("_GetByRekananBLG", myDataForm);
                    }
                    return View("Error");
                case 7:
                    responseMessage = await client.GetAsync(urlNOT + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;

                        return PartialView("_GetByRekananNOT", myDataForm);
                    }
                    return View("Error");
            }
            return View("Error");
        }

        //public async Task<ActionResult> PekerjaanByTypeOfRekanan(int pIdTypeOfRekanan)
        //{
        //    HttpResponseMessage responseMessage = new HttpResponseMessage();
        //    trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
        //    //1	Kantor Jasa Penilai Publik (KJPP)
        //    //2	Kantor Akuntan Publik (KAP)
        //    //3	Konsultan Manajemen
        //    //4	Asuransi Jiwa
        //    //5	Asuransi Kerugian
        //    //6	Balai Lelang
        //    //7	Notaris
        //    switch (pIdTypeOfRekanan)
        //    {
        //        case 1:
        //        case 3:
        //            responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
        //                myDataForm.DetailPekerjaanColls = myData;

        //                return View("_GetByRekananKJP", myDataForm);
        //            }
        //            return View("Error");
        //        case 2:
        //            responseMessage = await client.GetAsync(urlKAP + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
        //                myDataForm.DetailPekerjaanColls = myData;

        //                return View("_GetByRekananKAP", myDataForm);
        //            }
        //            return View("Error");
        //        case 4:
        //        case 5:
        //            responseMessage = await client.GetAsync(urlAS + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanAS_3M>>(responseData);
        //                myDataForm.DetailPekerjaanAS_3MColls = myData;

        //                return View("_GetByRekananAS", myDataForm);
        //            }
        //            return View("Error");
        //        case 6:
        //            responseMessage = await client.GetAsync(urlBLG + "/PekerjaanByTypeOfRekanan");
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanBLG>>(responseData);
        //                myDataForm.DetailPekerjaanBLGColls = myData;

        //                return View("_GetByRekananBLG", myDataForm);
        //            }
        //            return View("Error");
        //        case 7:
        //            responseMessage = await client.GetAsync(urlNOT + "/PekerjaanByTypeOfRekanan/" + pIdTypeOfRekanan.ToString());
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //                var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
        //                myDataForm.DetailPekerjaanColls = myData;

        //                return View("_GetByRekananNOT", myDataForm);
        //            }
        //            return View("Error");
        //    }
        //    return View("Error");
        //}

        public async Task<ActionResult> _GetByRekanan() 
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
            myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
            myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;

            if ((int)tokenContainer.IdTypeOfRekanan == 9)
            {
                ViewBag.IsPcp = true;
                ViewBag.ButSimpan = "hidden";
                ViewBag.ButBatal = "hidden";
                ViewBag.ButTutup = "visible";
            }
            else
            {
                ViewBag.IsPcp = false;
                ViewBag.ButSimpan = "visible";
                ViewBag.ButBatal = "visible";
                ViewBag.ButTutup = "hidden";
            }
            switch ((int)tokenContainer.IdTypeOfRekanan)
            {
                case 1:
                case 3:
                    responseMessage = await client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    ViewBag.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;

                        return PartialView("_GetByRekananKJP", myDataForm);
                    }
                    return View("Error");
                case 2:
                    responseMessage = await client.GetAsync(urlKAP + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        //myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;

                        return PartialView("_GetByRekananKAP", myDataForm);
                    }
                    return View("Error");
                case 4:
                case 5:
                    responseMessage = await client.GetAsync(urlAS + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                        }
                        myDataForm.DetailPekerjaanAS_3MColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;

                        return PartialView("_GetByRekananAS", myDataForm);
                    }
                    return View("Error");
                case 6:
                    responseMessage = await client.GetAsync(urlBLG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                        }
                        myDataForm.DetailPekerjaanBLGColls = myData;
                        //myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;

                        return PartialView("_GetByRekananBLG", myDataForm);
                    }
                    return View("Error");
                case 7:
                    responseMessage = await client.GetAsync(urlNOT + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        //myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;

                        return PartialView("_GetByRekananNOT", myDataForm);
                    }
                    return View("Error");
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
            myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
            myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
            myDataForm.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;

            if ((int)tokenContainer.IdTypeOfRekanan == 9)
            {
                ViewBag.IsPcp = true;
                ViewBag.ButSimpan = "hidden";
                ViewBag.ButBatal = "hidden";
                ViewBag.ButTutup = "visible";
            }
            else
            {
                ViewBag.IsPcp = false;
                ViewBag.ButSimpan = "visible";
                ViewBag.ButBatal = "visible";
                ViewBag.ButTutup = "hidden";
            }

            switch ((int)tokenContainer.IdTypeOfRekanan)
            {
                case 1:
                    ViewBag.JudulDetail = "Data Pekerjaan KJPP";
                    responseMessage = await client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 1;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";
                        ViewBag.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;

                        return View("GetByRekananKJP", myDataForm);
                    }
                    return View("Error");
                case 3:
                    ViewBag.JudulDetail = "Data Pekerjaan Konsultan Managemen";
                    responseMessage = await client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 3;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";
                        ViewBag.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;

                        return View("GetByRekananKJP", myDataForm);
                    }
                    return View("Error");
                case 2:
                    responseMessage = await client.GetAsync(urlKAP + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 2;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";

                        return View("GetByRekananKAP", myDataForm);
                    }
                    return View("Error");
                case 4:
                    ViewBag.JudulDetail = "Monitoring 3 Bulanan";
                    responseMessage = await client.GetAsync(urlAS + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 4;
                        }
                        myDataForm.DetailPekerjaanAS_3MColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";

                        return View("GetByRekananAS", myDataForm);
                    }
                    return View("Error");
                case 5:
                    ViewBag.JudulDetail = "Monitoring 3 Bulanan";
                    responseMessage = await client.GetAsync(urlAS + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 5;
                        }

                        myDataForm.DetailPekerjaanAS_3MColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";

                        return View("GetByRekananAS", myDataForm);
                    }
                    return View("Error");
                case 6:
                    responseMessage = await client.GetAsync(urlBLG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 6;
                        }
                        myDataForm.DetailPekerjaanBLGColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";

                        return View("GetByRekananBLG", myDataForm);
                    }
                    return View("Error");
                case 7:
                    responseMessage = await client.GetAsync(urlNOT + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        for (int i = 0; i < myData.Count; i++)
                        {
                            myData[i].IdTypeOfRekanan = 7;
                        }
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        ViewBag.ActionPartial = "_GetByRekanan";
                        ViewBag.RInfoVisible = "false";

                        return View("GetByRekananNOT", myDataForm);
                    }
                    return View("Error");
            }
            return View("Error");
        }

        public async Task<ActionResult> GetByRekananXLS(string strFilterExpression)
        {
            string[] arrResult = new string[] { };
            arrResult = ParseFilterEx(strFilterExpression);
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            switch ((int)tokenContainer.IdTypeOfRekanan)
            {
                case 1:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlKJP_KMG, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanKJP(strFilterExpression, "KJPP", false), myData, "XLSPekerjaanKJP_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
                case 3:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlKJP_KMG, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanKJP(strFilterExpression, "KMG", false), myData, "XLSPekerjaanKMG_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
                case 2:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlKAP, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanKAP(strFilterExpression, false), myData, "XLSPekerjaanKAP_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
                case 4:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlAS, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanAS_3M>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanAS(strFilterExpression, "Asuransi Jiwa", false), myData, "XLSPekerjaanAS_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
                case 5:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlAS, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanAS_3M>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanAS(strFilterExpression, "Asuransi Kerugian", false), myData, "XLSPekerjaanAS_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
                case 6:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlBLG, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaanBLG>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanBLG(strFilterExpression, false), myData, "XLSPekerjaanBGL_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
                case 7:
                    responseMessage = await client.GetAsync(string.Format("{0}/GetByRekananXLS/{1}/{2}/{3}", urlNOT, tokenContainer.IdRekananContact.ToString(), arrResult[0], arrResult[1]));
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                        XlsExportOptions xlsOption = new XlsExportOptions();
                        GridViewExtension.WriteXlsToResponse(GridSettingHelper.XLS_DaftarPekerjaanNOT(strFilterExpression, false), myData, "XLSPekerjaanNOT_Rek", xlsOption);

                        return new EmptyResult();
                    }
                    return View("Error");
            }
            return View("Error");
        }

        public ActionResult GetByRekananTab()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            switch ((int)tokenContainer.IdTypeOfRekanan)
            {
                case 1:
                    ViewBag.JudulDetail = "Data Pekerjaan KJPP";
                    responseMessage = client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananKJP";
                        ViewBag.IdTypeOfRekanan = 1;

                        return PartialView("GetByRekananKJP", myDataForm);
                    }
                    return View("Error");
                case 3:
                    ViewBag.JudulDetail = "Data Pekerjaan Konsultan Managemen";
                    responseMessage = client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananKAP";

                        return PartialView("GetByRekananKJP", myDataForm);
                    }
                    return View("Error");
                case 2:
                    responseMessage = client.GetAsync(urlKAP + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananKAP";
                        
                        return PartialView("GetByRekananKAP", myDataForm);
                    }
                    return View("Error");
                case 4:
                    ViewBag.JudulDetail = "Monitoring 3 Bulanan";
                    responseMessage = client.GetAsync(urlAS + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanAS_3MColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananAS";

                        return PartialView("GetByRekananAS", myDataForm);
                    }
                    return View("Error");
                case 5:
                    ViewBag.JudulDetail = "Monitoring 3 Bulanan";
                    responseMessage = client.GetAsync(urlAS + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanAS_3MColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananAS";

                        return PartialView("GetByRekananAS", myDataForm);
                    }
                    return View("Error");
                case 6:
                    responseMessage = client.GetAsync(urlBLG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanBLGByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanBLGColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananBLG";

                        return PartialView("GetByRekananBLG", myDataForm);
                    }
                    return View("Error");
                case 7:
                    responseMessage = client.GetAsync(urlNOT + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                        myDataForm.DetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananNOT";

                        return PartialView("GetByRekananNOT", myDataForm);
                    }
                    return View("Error");
            }
            return View("Error");
        }
        public ActionResult GetByRekananTab_1M()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanAS_1MMulti myDataForm = new trxDetailPekerjaanAS_1MMulti();
            //trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            switch ((int)tokenContainer.IdTypeOfRekanan)
            {
                case 4:
                    ViewBag.JudulDetail = "Monitoring Bulanan";
                    responseMessage = client.GetAsync(urlAS1M + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_1MByTypeOfRekanan_Result>>(responseData);
                        myDataForm.XLSDetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananAS_1M";
                        ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();

                        return PartialView("GetByRekananAS_1M", myDataForm);
                    }
                    return View("Error");
                case 5:
                    ViewBag.JudulDetail = "Monitoring Bulanan";
                    responseMessage = client.GetAsync(urlAS1M + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString()).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_1MByTypeOfRekanan_Result>>(responseData);
                        myDataForm.XLSDetailPekerjaanColls = myData;
                        myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                        myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                        ViewBag.ActionPartial = "_GetByRekananAS_1M";
                        ViewBag.IdTypeOfRekanan = 5;

                        return PartialView("GetByRekananAS_1M", myDataForm);
                    }
                    return View("Error");
            }
            return View("Error");
        }
        public ActionResult _GetAS_1MByRekanan(string IdRekanan)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanAS_1MMulti myDataForm = new trxDetailPekerjaanAS_1MMulti();
            //trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            //1	Kantor Jasa Penilai Publik (KJPP)
            //2	Kantor Akuntan Publik (KAP)
            //3	Konsultan Manajemen
            //4	Asuransi Jiwa
            //5	Asuransi Kerugian
            //6	Balai Lelang
            //7	Notaris
            ViewBag.JudulDetail = "Monitoring Bulanan";
            responseMessage = client.GetAsync(String.Format("{0}/GetLapAS1MByRekanan/{1}", urlAS1M, IdRekanan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fGetLapAS1MByRekanan_Result>>(responseData);
                myDataForm.XLSAS1MDetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetAS_1MByRekanan";
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();

                return PartialView("_GetAS_1MByRekanan", myDataForm);
            }
            return View("Error");
        }
        public ActionResult CustomDeleteAction(string selectedKeys)
        {
            string IdRekanan = tokenContainer.IdRekananContact.ToString();
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanAS_1MMulti myDataForm = new trxDetailPekerjaanAS_1MMulti();
            foreach (var item in selectedKeys.Split('|'))
            {
                int key;
                //if (int.TryParse(item, out key))
                //    clist.DeleteCustomer(key);
            }
            ViewBag.JudulDetail = "Monitoring Bulanan";
            responseMessage = client.GetAsync(String.Format("{0}/GetLapAS1MByRekanan/{1}", urlAS1M, IdRekanan)).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fGetLapAS1MByRekanan_Result>>(responseData);
                myDataForm.XLSAS1MDetailPekerjaanColls = myData;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                myDataForm.TypeOfSegmentasi5Colls = DataMasterProvider.Instance.TypeOfSegmentasi5Colls;
                ViewBag.ActionPartial = "_GetAS_1MByRekanan";
                ViewBag.IdRekanan = tokenContainer.IdRekananContact.ToString();

                return PartialView("_GetAS_1MByRekanan", myDataForm);
            }
            return View("Error");
        }

        public async Task<ActionResult> _GetByRekananKJP()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                ViewBag.ActionPartial = "_GetByRekananKJP";

                return PartialView("_GetByRekananKJP", myDataForm);
            }
            return View("Error");
        }

        public async Task<ActionResult> _GetByRekananKAP()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKAP + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                ViewBag.ActionPartial = "_GetByRekananKAP";

                return PartialView("_GetByRekananKAP", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekananAS()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlAS + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                ViewBag.ActionPartial = "_GetByRekananAS";

                return PartialView("_GetByRekananAS", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekananBLG()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlBLG + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                ViewBag.ActionPartial = "_GetByRekananBLG";

                return PartialView("_GetByRekananBLG", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekananNOT()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlNOT + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            trxDetailPekerjaanHeader myDataForm = new trxDetailPekerjaanHeader();
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanColls = myData;
                myDataForm.TypeTotalAsetColls = DataMasterProvider.Instance.TypeTotalAsetColls;
                myDataForm.TypeOfSegmentasi3Colls = DataMasterProvider.Instance.TypeOfSegmentasi3Colls;
                ViewBag.ActionPartial = "_GetByRekananNOT";

                return PartialView("_GetByRekananNOT", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan_ASR()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                return View(myDataList);
            }
            return View("Error");
        }

        //public async Task<ActionResult> _AddEditDetail(int IdData)
        //{
        //    HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/" + IdData);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanSingle>(responseData);

        //        if (IdData < 0)
        //        {
        //            myData.MaxYear = DateTime.Today.Year;
        //            myData.GuidHeader = Guid.Empty;
        //        }
        //        switch ((int)tokenContainer.IdTypeOfRekanan)
        //        {
        //            case 2: //KAP
        //                return View("_AddEditDetailKAP", myData);
        //            case 7: //NOTARIS
        //                return View("_AddEditDetailNOT", myData);
        //            default: //KJPP, Konsultan Management, Balai Lelang
        //                return View("_AddEditDetailOTH", myData);
        //        }
        //    }
        //    return View("Error");
        //}

        public async Task<ActionResult> _AddEditDetail(int IdData, int IdTypeOfRekanan)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            if((int)tokenContainer.IdTypeOfRekanan == 9)
            {
                ViewBag.IsPcp = true;
                ViewBag.ButSimpan = "hidden";
                ViewBag.ButBatal = "hidden";
                ViewBag.ButTutup = "visible";
            }
            else
            {
                ViewBag.IsPcp = false;
                ViewBag.ButSimpan = "visible";
                ViewBag.ButBatal = "visible";
                ViewBag.ButTutup = "hidden";
            }
            //switch ((int)tokenContainer.IdTypeOfRekanan)
            switch (IdTypeOfRekanan)
            {
                case 1: //KJPP
                case 3: //KMG
                    if (IdTypeOfRekanan == 1)
                    {
                        ViewBag.CallBackAction = "GetByTypeKJP";
                        ViewBag.PenandaTangan = "Penandatangan Laporan (Partner)";
                    }
                    else
                    {
                        ViewBag.CallBackAction = "GetByTypeKMG";
                        ViewBag.PenandaTangan = "Penandatangan Laporan";
                    }
                    responseMessage = await client.GetAsync(urlKJP_KMG + "/" + IdData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanSingle>(responseData);
                        myData.IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan;
                        if(IdData < 0)
                        {
                            myData.MaxYear = DateTime.Today.Year;
                            myData.GuidHeader = Guid.Empty;
                            if((int)tokenContainer.IdTypeOfRekanan == 1)
                            {
                                ViewBag.PenandaTangan = "Penandatangan Laporan (Partner)";
                            }
                            else
                            {
                                ViewBag.PenandaTangan = "Penandatangan Laporan";
                            }
                        }
                        return View("_AddEditDetailKJP", myData);
                    }
                    return View("Error");
                case 2: //KAP
                    responseMessage = await client.GetAsync(urlKAP + "/" + IdData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanSingle>(responseData);

                        if (IdData < 0)
                        {
                            myData.MaxYear = DateTime.Today.Year;
                            myData.GuidHeader = Guid.Empty;
                        }
                        return View("_AddEditDetailKAP", myData);
                    }
                    return View("Error");
                case 4: //ASJ
                case 5: //ASK
                    if (IdTypeOfRekanan == 4)
                    {
                        ViewBag.CallBackAction = "GetByTypeASJ";
                    }
                    else
                    {
                        ViewBag.CallBackAction = "GetByTypeASK";
                    }
                    //trxDetailPekerjaanAS_3MForm myDataForm = new trxDetailPekerjaanAS_3MForm();
                    responseMessage = await client.GetAsync(urlAS + "/" + IdData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_3M>(responseData);
                        myData.TriwulanColls = DataMasterProvider.Instance.TriwulanColls;
                        ImageContainerUpd.ImageBaseName = myData.GuidHeader;

                        if (IdData < 0)
                        {
                            myData.GuidHeader = Guid.NewGuid();
                            ImageContainerUpd.ImageBaseName = myData.GuidHeader;
                        }
                        return View("_AddEditDetailAS_3M", myData);
                    }
                    return View("Error");
                case 6: //BLG
                    responseMessage = await client.GetAsync(urlBLG + "/" + IdData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanBLGSingle>(responseData);

                        if (IdData < 0)
                        {
                            //myData.MaxYear = DateTime.Today.Year;
                            myData.GuidHeader = Guid.Empty;
                        }
                        return View("_AddEditDetailBLG", myData);
                    }
                    return View("Error");
                case 7: //NOT
                    responseMessage = await client.GetAsync(urlNOT + "/" + IdData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanSingle>(responseData);

                        if (IdData < 0)
                        {
                            myData.MaxYear = DateTime.Today.Year;
                            myData.GuidHeader = Guid.Empty;
                        }
                        return View("_AddEditDetailNOT", myData);
                    }
                    return View("Error");
                default:
                    return View("Error");
            }
        }

        public async Task<ActionResult> _AddEditDetailAS_3M(int IdData = -1)
        {
            trxDetailPekerjaanAS_3M myData = new trxDetailPekerjaanAS_3M();

            if (IdData > 0)
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                responseMessage = await client.GetAsync(urlAS + "/" + IdData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    myData = JsonConvert.DeserializeObject<trxDetailPekerjaanAS_3M>(responseData);

                    ImageContainerUpd.ImageBaseName = myData.GuidHeader;
                    return View("_AddEditDetailAS_3M", myData);
                }
                return View("Error");
            }
            else
            {
                myData.GuidHeader = Guid.NewGuid();
                ImageContainerUpd.ImageBaseName = myData.GuidHeader;
                return View("_AddEditDetailAS_3M", myData);
            }
        }

        public async Task<ActionResult> _AddEditDetailBLG(int IdData)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            responseMessage = await client.GetAsync(urlBLG + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanBLGSingle>(responseData);

                if (IdData < 0)
                {
                    //myData.MaxYear = DateTime.Today.Year;
                    myData.GuidHeader = Guid.Empty;
                }
                return View("_AddEditDetailBLG", myData);
            }
            return View("Error");
        }
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> _AddEditDetail(trxDetailPekerjaanSingle myDataForm)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaan myData = new trxDetailPekerjaan();
            trxDetailPekerjaanAS_3M myData3M = new trxDetailPekerjaanAS_3M();
            trxDetailPekerjaanBLG myDataBLG = new trxDetailPekerjaanBLG();
            myData.InjectFrom(myDataForm);
            if (myData.IdDetailPekerjaan > 0)
            {
                switch ((int)tokenContainer.IdTypeOfRekanan)
                {
                    case 1:
                    case 3:
                        responseMessage = await client.PutAsJsonAsync(urlKJP_KMG + "/" + myData.IdDetailPekerjaan, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    case 2:
                        responseMessage = await client.PutAsJsonAsync(urlKAP + "/" + myData.IdDetailPekerjaan, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    case 4: //ASJ
                    case 5: //ASK
                        responseMessage = await client.PutAsJsonAsync(urlAS + "/" + myData.IdDetailPekerjaan, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    case 6: //BLG
                        //responseMessage = await client.PutAsJsonAsync(urlBLG + "/" + myData.IdDetailPekerjaan, myData);
                        //if (responseMessage.IsSuccessStatusCode)
                        //{
                        //    return RedirectToAction("GetByRekanan");
                        //}
                        return RedirectToAction("Error");
                    case 7: //NOT
                        responseMessage = await client.PutAsJsonAsync(urlNOT + "/" + myData.IdDetailPekerjaan, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");

                    default:
                        return RedirectToAction("Error");
                }
            }
            else
            {
                switch ((int)tokenContainer.IdTypeOfRekanan)
                {
                    case 1:
                    case 3:
                        myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                        myData.CreatedDate = DateTime.Today;
                        myData.CreatedUser = tokenContainer.UserId.ToString();
                        responseMessage = await client.PostAsJsonAsync(urlKJP_KMG, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    case 2:
                        myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                        myData.CreatedDate = DateTime.Today;
                        myData.CreatedUser = tokenContainer.UserId.ToString();
                        responseMessage = await client.PostAsJsonAsync(urlKAP, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    case 4:
                    case 5:
                        myData3M.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                        myData3M.CreatedDate = DateTime.Today;
                        myData3M.CreatedUser = tokenContainer.UserId.ToString();
                        responseMessage = await client.PostAsJsonAsync(urlAS, myData3M);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    case 6:
                        //myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                        //myData.CreatedDate = DateTime.Today;
                        //myData.CreatedUser = tokenContainer.UserId.ToString();
                        //responseMessage = await client.PostAsJsonAsync(urlBLG, myData);
                        //if (responseMessage.IsSuccessStatusCode)
                        //{
                        //    return RedirectToAction("GetByRekanan");
                        //}
                        return RedirectToAction("Error");
                    case 7:
                        myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                        myData.CreatedDate = DateTime.Today;
                        myData.CreatedUser = tokenContainer.UserId.ToString();
                        responseMessage = await client.PostAsJsonAsync(urlNOT, myData);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetByRekanan");
                        }
                        return RedirectToAction("Error");
                    default:
                        return RedirectToAction("Error");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditDetailAS_3M(trxDetailPekerjaanAS_3M myDataForm)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanAS_3M myData = new trxDetailPekerjaanAS_3M();
            myData.InjectFrom(myDataForm);
            if (myData.IdDetailPekerjaan > 0)
            {
                //myData.FileName = ImageContainerUpd.ImageFileName;
                //myData.FileExt = ImageContainerUpd.ImageExtension;
                responseMessage = await client.PutAsJsonAsync(urlAS + "/" + myData.IdDetailPekerjaan, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                //myData.FileName = ImageContainerUpd.ImageFileName;
                //myData.FileExt = ImageContainerUpd.ImageExtension;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                responseMessage = await client.PostAsJsonAsync(urlAS, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditDetailBLG(trxDetailPekerjaanBLG myDataForm)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            trxDetailPekerjaanBLG myData = new trxDetailPekerjaanBLG();
            myData.InjectFrom(myDataForm);
            if (myData.IdDetailPekerjaan > 0)
            {
                responseMessage = await client.PutAsJsonAsync(urlBLG + "/" + myData.IdDetailPekerjaan, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.IdRekanan = (Guid)tokenContainer.IdRekananContact;
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                responseMessage = await client.PostAsJsonAsync(urlBLG, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> _Delete(int id, trxDetailPekerjaan Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlKAP + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> _Delete(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlKAP + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> _DeleteAS_3M(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlAS + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> _DeleteBLG(int IdData)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(urlBLG + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }
        //1	Kantor Jasa Penilai Publik (KJPP)
        //2	Kantor Akuntan Publik (KAP)
        //3	Konsultan Manajemen
        //4	Asuransi Jiwa
        //5	Asuransi Kerugian
        //7	Balai Lelang
        //8	Notaris
        public async Task<ActionResult> _RptKJPP()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptKJPP", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptKJPP()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptKJPP", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RptKAP()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/2");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptKAP", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptKAP()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/2");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptKAP", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RptKonsultanM()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/3");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptKonsultanM", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptKonsultanM()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/3");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptKonsultanM", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RptAsuransiJiwa()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptAsuransiJiwa", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptAsuransiJiwa()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptAsuransiJiwa", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RptAsuransiKerugian()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/5");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptAsuransiKerugian", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptAsuransiKerugian()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/5");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptAsuransiKerugian", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RptBalai()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/7");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptBalai", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptBalai()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/7");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptBalai", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RptNotaris()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/8");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return PartialView("_RptNotaris", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> RptNotaris()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(urlKJP_KMG + "/PekerjaanByTypeOfRekanan/8");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanMulti>(responseData);
                return View("RptNotaris", myData);
            }
            return View("Error");
        }
    }
}
