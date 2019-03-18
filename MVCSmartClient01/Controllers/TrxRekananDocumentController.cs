using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using ApiHelper;
using System.Configuration;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxRekananDocumentController : Controller
    {
        HttpClient client;
        private readonly ITokenContainer tokenContainer;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxRekananDocument";
        string url = string.Empty;
        string urlBranch = string.Empty;
        string urlBranchHeader = string.Empty;
        string urlDetail = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxRekananDocumentController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxRekananDocument", SmartAPIUrl);
            urlDetail = string.Format("{0}/api/TrxDocMandatoryDetail", SmartAPIUrl);
            urlBranch = string.Format("{0}/api/TrxBranchOffice", SmartAPIUrl);
            urlBranchHeader = string.Format("{0}/api/TrxBranchOfficeHeader", SmartAPIUrl);
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
                var myData = JsonConvert.DeserializeObject<List<trxRekananDocument>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> GetByRekanan()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdTypeOfRekanan.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                return View(myData);
                //return PartialView(myData);
            }
            return View("Error");
        }
        public ActionResult GetByRekananTab()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdTypeOfRekanan.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                return PartialView("GetByRekanan", myData);
            }
            return View("Error");
        }
        //public async Task<ActionResult> _RekananDocByRek()
        //{
        //    HttpResponseMessage responseMessage = await client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdTypeOfRekanan.ToString());
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //        var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
        //        return PartialView("_RekananDocByRek", myData);
        //    }
        //    return View("Error");
        //}
        //public async Task<ActionResult> _RekananDocDetailByRek(int IdTypeOfDocument)
        //{
        //    if (tokenContainer.IdRekananContact != null)
        //    {
        //        HttpResponseMessage responseMessage = await client.GetAsync(string.Format(url + "/GetDocDetailByRekanan/{0}/{1}",tokenContainer.IdRekananContact.ToString(), IdTypeOfDocument.ToString()));
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
        //            var myData = JsonConvert.DeserializeObject<IEnumerable<trxDocMandatoryDetail>>(responseData);
        //            return PartialView("_RekananDocDetailByRek", myData);
        //        }
        //    }
        //    return View("Error");
        //}
        public async Task<ActionResult> _RekananDocByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetByRekanan/" + tokenContainer.IdTypeOfRekanan.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                return PartialView("_RekananDocByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _RekananDocDetailByRek(int IdTypeOfDocument)
        {
            if (tokenContainer.IdRekananContact != null)
            {
                if (IdTypeOfDocument == 49)
                {
                    if (tokenContainer.IdTypeOfRekanan.ToString() == "1" || tokenContainer.IdTypeOfRekanan.ToString() == "2"
                        || tokenContainer.IdTypeOfRekanan.ToString() == "4" || tokenContainer.IdTypeOfRekanan.ToString() == "5")
                    {
                        HttpResponseMessage responseMessage = client.GetAsync(string.Format(urlBranch + "/GetByRekanan/{0}", tokenContainer.IdRekananContact.ToString())).Result;
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                            var myData = JsonConvert.DeserializeObject<IEnumerable<trxBranchOffice>>(responseData);
                            //49 Kantor Cabang
                            return PartialView("_BranchByRekanan", myData);
                        }
                    }
                    else
                    {
                        HttpResponseMessage responseMessage = client.GetAsync(string.Format(urlBranchHeader + "/GetByRekanan/{0}", tokenContainer.IdRekananContact.ToString())).Result;
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                            var myData = JsonConvert.DeserializeObject<IEnumerable<trxBranchOfficeHeader>>(responseData);
                            //49 Kantor Cabang
                            return PartialView("_BranchHeaderByRek", myData);
                        }
                    }
                    return View("Error");
                }
                else
                {
                    HttpResponseMessage responseMessage = client.GetAsync(string.Format(url + "/GetDocDetailByRekanan/{0}/{1}", tokenContainer.IdRekananContact.ToString(), IdTypeOfDocument.ToString())).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                        var myData = JsonConvert.DeserializeObject<IEnumerable<trxDocMandatoryDetail>>(responseData);
                        ViewBag.IdTypeOfDocument = IdTypeOfDocument;
                        ViewData["IdTypeOfDocument"] = IdTypeOfDocument;
                        //return PartialView("_RekananDocDetailByRek", myData);
                        switch (IdTypeOfDocument)
                        {
                            case 1: //1	Surat Permohonan
                                return PartialView("_RekananDD_1", myData);
                            case 2: //2	Company Profile
                            case 18:
                            case 19:
                            case 62:
                            case 63:
                            case 64:
                                ViewBag.NomorDokumen = "Nomor Dokumen";
                                return PartialView("_RekananDD_2", myData);
                            case 60:
                                ViewBag.NomorDokumen = "Nomor Rekening";
                                return PartialView("_RekananDD_2", myData);
                            case 61:
                                ViewBag.NomorDokumen = "Judul Penghargaan";
                                return PartialView("_RekananDD_2", myData);

                            case 3: //3	Akta Pendirian dan Perubahan
                            case 5: //5	Izin Usaha/Operasional Dari Instansi yang Berwenang
                            case 7: //7	Surat Tanda Daftar Perusahaan
                                return PartialView("_RekananDD_3", myData);

                            case 8: //7	Surat Tanda Daftar Perusahaan
                                return PartialView("_RekananDD_8", myData);

                            case 32: //32	Bukti Kepemilikan / Sewa / Kontrak Tempat Usaha
                                return PartialView("_RekananDD_32", myData);
                            case 38: //38	Contoh Format Laporan
                                return PartialView("_RekananDD_38", myData);
                            //case 30: //30	Curriculum Vitae Pimpinan Rekan dan Rekan
                            //    return PartialView("_RekananDD_30", myData);
                            case 33: //33	Daftar Inventaris Perusahaan
                                return PartialView("_RekananDD_33", myData);

                            case 27: //27	Daftar Tenaga Ahli Tetap

                            case 28: //28	Daftar Tenaga Ahli Tidak Tetap
                            case 29: //29	Daftar Tenaga Pendukung
                                return PartialView("_RekananDD_27", myData);

                            case 15: //15	Dokumen Sebagai Rekanan dari Bank Lembaga/Instansi Lain
                                return PartialView("_RekananDD_15", myData);
                            //case 26: //26	Ijin sebagai Akuntan Publik
                            //    return PartialView("_RekananDD_26", myData);
                            //case 35: //35	Kartu Anggota Asosiasi
                            //    return PartialView("_RekananDD_35", myData);
                            //case 13: //13	Kartu Tanda Penduduk (KTP) Perorangan dan atau Persekutuan
                            //    return PartialView("_RekananDD_13", myData);
                            case 25: //25	Perjanjian Kerjasama dengan Kantor Akuntan Asing
                                return PartialView("_RekananDD_25", myData);
                            case 24: //24	Perjanjian Persekutuan
                                return PartialView("_RekananDD_24", myData);
                            case 16: //16	Surat Keterangan Belum/Pernah Dikenakan Sanksi
                            case 10: //10	Surat Keterangan Domisili Perusahaan (SKDP)
                                return PartialView("_RekananDD_16", myData);

                            case 36: //36	Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KAP lain
                                return PartialView("_RekananDD_36", myData);
                            case 31: //31	Daftar Pengalaman (minimal 2 tahun terakhir)
                                return PartialView("_RekananDD_31", myData);
                            case 41: //33	Daftar Inventaris Perusahaan
                            case 43: //33	Daftar Inventaris Perusahaan
                                return PartialView("_RekananDD_41", myData);
                            case 50: //50	Pakta Integritas
                                return PartialView("_RekananDD_50", myData);
                            case 54: //31	Laporan Keuangan (minimal 2 tahun terakhir)
                                return PartialView("_RekananDD_54", myData);
                            default:
                                return PartialView("_RekananDocDetailByRek", myData);
                        }
                    }
                }
            }
            return View("Error");
        }
        public async Task<ActionResult> _RekananDocFileByRek(int IdDocMandatoryDetail)
        {
            HttpResponseMessage responseMessage = client.GetAsync(string.Format(url + "/GetFileByDetail/{0}", IdDocMandatoryDetail.ToString())).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<IEnumerable<trxDocMandatoryFile>>(responseData);
                return PartialView("_RekananDocFileByRek", myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditDocDetail(int IdDetDocument = -1, int IdTypeOfDocument = -1)
        {
            if (IdDetDocument > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(urlDetail + "/" + IdDetDocument);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxRekananDocument>(responseData);
                    return View(myData);
                }
                return View("Error");
            }
            else
            {
                Guid myImageBaseName = Guid.NewGuid();
                trxDocMandatoryDetail myData = new trxDocMandatoryDetail();
                ImageContainerUpd.ImageBaseName = myImageBaseName;
                myData.ImageBaseName = myImageBaseName;
                myData.IdTypeOfDocument = IdTypeOfDocument;
                return View(myData);
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditDocDetail(trxDocMandatoryDetail myData)
        {
            if (myData.IdDocMandatoryDetail > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(urlDetail + "/" + myData.IdDocMandatoryDetail, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(urlDetail, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }

        public ActionResult Create()
        {
            Guid myImageBaseName = Guid.NewGuid();
            trxRekananDocument myData = new trxRekananDocument();
            ImageContainerUpd.ImageBaseName = myImageBaseName;
            myData.ImageBaseName = myImageBaseName;
            return View(myData);
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxRekananDocument myData)
        {
            myData.CreatedDate = DateTime.Today;
            myData.CreatedUser = tokenContainer.UserId.ToString();
            myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocument>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int id, trxRekananDocument Emp)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + id, Emp);
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
                var myData = JsonConvert.DeserializeObject<trxRekananDocument>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, trxRekananDocument Emp)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> _EditRekDocument(int idDocument = -1)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + idDocument.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentForm>(responseData);
                return View("EditRekDocument", myData);
            }
            return PartialView("Error");
        }
        public async Task<ActionResult> _EditDocDetail(int IdDocMandatoryDetail = -1)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdDocMandatoryDetail.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentForm>(responseData);
                return View("EditRekDocument", myData);
            }
            return PartialView("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> _EditRekDocument(trxRekananDocumentForm rekDocument)
        {
            rekDocument.CreatedDate = DateTime.Today;
            rekDocument.CreatedUser = tokenContainer.UserId.ToString();
            rekDocument.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
            if (rekDocument.IdDocument == -1)
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, rekDocument);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
            else
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + rekDocument.IdDocument, rekDocument);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetByRekanan");
                }
                return RedirectToAction("Error");
            }
        }
        public async Task<ActionResult> _DeleteRekDocument(int idDocument)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + idDocument.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan");
            }
            return RedirectToAction("Error");
        }
        public ActionResult ImageUpload()
        {
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles("uploadControl", UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);
            //TempData["ContentType"] = arrUploaded[0].ContentType;
            return null;
        }

    }

}
