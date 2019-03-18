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

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxDetailPekerjaanAS_3MController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxDetailPekerjaan";
        string url = string.Empty;
 
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxDetailPekerjaanAS_3MController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxDetailPekerjaan", SmartAPIUrl);

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
                var Employees = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData); 
                return View(Employees);
        }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekanan()
        {
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
                case 7:
                    myDataForm.IsNotaris = true;
                    break;
                case 4:
                case 5:
                    myDataForm.IsAsuransi = true;
                    break;
                default:
                    myDataForm.IsGrupNonNotaris = true;
                    break;
            }
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanAS_3MColls = myData;

                return PartialView("_GetByRekanan", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetByRekanan_ASR()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
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
        public async Task<ActionResult> GetByRekanan()
        {
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
                case 7:
                    myDataForm.IsNotaris = true;
                    break;
                case 4:
                case 5:
                    myDataForm.IsAsuransi = true;
                    break;
                default:
                    myDataForm.IsGrupNonNotaris = true;
                    break;
            }
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdRekananContact.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<fPekerjaanAS_3MByTypeOfRekanan_Result>>(responseData);
                myDataForm.DetailPekerjaanAS_3MColls = myData;

                return View("GetByRekanan", myDataForm);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan_ASR()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myDataList = JsonConvert.DeserializeObject<List<trxDetailPekerjaan>>(responseData);
                return View(myDataList);
            }
            return View("Error");
        }
        public FilePathResult DownloadTempPekerjaan(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }
        public ActionResult Create()
        {
            return View(new trxDetailPekerjaan());
        }
 
        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxDetailPekerjaan Emp)
        {
           HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(url,Emp);
           if (responseMessage.IsSuccessStatusCode)
           {
               return RedirectToAction("Index");
           }
           return RedirectToAction("Error");
        }

        public async Task<ActionResult> _AddEditDetail(int IdData)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdData);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDetailPekerjaanSingle>(responseData);

                if(IdData < 0)
                {
                    myData.MaxYear = DateTime.Today.Year;
                    myData.GuidHeader = Guid.Empty;
                }
                switch ((int)tokenContainer.IdTypeOfRekanan)
                {
                    case 2: //KAP
                        return View("_AddEditDetailKAP", myData);
                    case 7: //NOTARIS
                        return View("_AddEditDetailNOT", myData);
                    default: //KJPP, Konsultan Management, Balai Lelang
                        return View("_AddEditDetailOTH", myData);
                }
            }
            return View("Error");

        }
 
        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> _AddEditDetail(trxDetailPekerjaanSingle myDataForm)
        {
            trxDetailPekerjaan myData = new trxDetailPekerjaan();
            myData.InjectFrom(myDataForm);
            if (myData.IdDetailPekerjaan > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdDetailPekerjaan, myData);
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }
 
        public async Task<ActionResult> _Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }
 
        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> _Delete(int id, trxDetailPekerjaan Emp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/1");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/1");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/2");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/2");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/3");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/3");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/4");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/4");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/5");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/5");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/7");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/7");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/8");
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
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/PekerjaanByTypeOfRekanan/8");
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
