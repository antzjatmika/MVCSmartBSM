using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MVCSmartClient01.Models;
using System.Configuration;
using ApiHelper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using Omu.ValueInjecter;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace MVCSmartClient01.Controllers
{
    using ApiInfrastructure;
    public class TrxDocMandatoryDetailController : Controller
    {
        private readonly ITokenContainer tokenContainer;
        HttpClient client;
        //The URL of the WEB API Service
        //string url = "http://localhost:2070/api/TrxDocMandatoryDetail";
        string url = string.Empty;

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public TrxDocMandatoryDetailController()
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            url = string.Format("{0}/api/TrxDocMandatoryDetail", SmartAPIUrl);

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
                var myData = JsonConvert.DeserializeObject<List<trxDocMandatoryDetail>>(responseData);
                return View(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetTenagaAhliByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTenagaAhliByRek/" + tokenContainer.IdRekananContact).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDocMandatoryDetail>>(responseData);
                //if (myData.Count == 0)
                //{
                //    myData = new List<trxDocMandatoryDetail>();
                //}
                ViewData["IdTypeOfDocument"] = 27;
                return PartialView(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _GetTenagaAhliTTByRek()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTenagaAhliTTByRek/" + tokenContainer.IdRekananContact).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<List<trxDocMandatoryDetail>>(responseData);
                //if (myData.Count == 0)
                //{
                //    myData = new List<trxDocMandatoryDetail>();
                //}
                ViewData["IdTypeOfDocument"] = 28;
                return PartialView(myData);
            }
            return View("Error");
        }
        public async Task<ActionResult> _AddEditDocDetail(int IdDetDocument = -1, int IdTypeOfDocument = -1)
        {
            trxDMDTenagaAhliTetap myDataImp = new trxDMDTenagaAhliTetap();
            if (IdDetDocument > 0)
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdDetDocument);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxDocMandatoryDetail>(responseData);
                    ImageContainerUpd.ImageBaseName = myData.ImageBaseName;

                    ImageContainerUpd.ImageExtension1 = myData.FileExt;
                    ImageContainerUpd.ImageExtension2 = myData.FileExt2;

                    ImageContainerUpd.ImageExtension = myData.FileExt;
                    //return View(myData);

                    #region Switch Detail Dokumen
                    myData.JenisPermohonanColls = DataMasterProvider.Instance.JenisPermohonanColls.ToList();
                    myData.ProcInfo = "Update: UNKNOWN";
		            switch (myData.IdTypeOfDocument)
                    {
                        case 1: //1	Surat Permohonan
                            ViewBag.JudulDetail = "Surat Permohonan";
                            myData.ProcInfo = "Update: Surat Permohonan";
                            return View("_AddEditDD_1", myData);
                        case 2: //2	Company Profile
                            ViewBag.JudulDetail = "Company Profile";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Company Profile";
                            return View("_AddEditDD_2", myData);
                        case 18: //18	Standard Operating Procedure
                            ViewBag.JudulDetail = "Standard Operating Procedure";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Standard Operating Procedure";
                            return View("_AddEditDD_2", myData);
                        case 63: //18	Standard Operating Procedure
                            ViewBag.JudulDetail = "Daftar Pekerjaan 2 Tahun Terakhir";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Daftar Pekerjaan 2 Tahun Terakhir";
                            return View("_AddEditDD_2", myData);
                        case 64: //18	Standard Operating Procedure
                            ViewBag.JudulDetail = "Surat Pernyataan Bahwa Dokumen Dan Data Adalah Benar";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Surat Pernyataan Bahwa Dokumen Dan Data Adalah Benar";
                            return View("_AddEditDD_2", myData);
                        //50	Pakta Integritas
                        //51	Standar Pengendalian Mutu
                        //52	Surat Pernyataan dan Ketentuan Umum
                        case 50: //50	Pakta Integritas
                            ViewBag.JudulDetail = "Pakta Integritas";
                            myData.ProcInfo = "Update: Pakta Integritas";
                            return View("_AddEditDD_50", myData);
                        case 51: //51	Standar Pengendalian Mutu
                            ViewBag.JudulDetail = "Standar Pengendalian Mutu";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Standar Pengendalian Mutu";
                            return View("_AddEditDD_2", myData);
                        case 52: //52	Surat Pernyataan dan Ketentuan Umum
                            ViewBag.JudulDetail = "Surat Pernyataan dan Ketentuan Umum";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Surat Pernyataan dan Ketentuan Umum";
                            return View("_AddEditDD_2", myData);
                        case 19: //19	Struktur Organisasi Perusahaan
                            ViewBag.JudulDetail = "Struktur Organisasi Perusahaan";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Struktur Organisasi Perusahaan";
                            return View("_AddEditDD_2", myData);
                        case 60:
                            ViewBag.JudulDetail = "Bukti Rekening BSM";
                            ViewBag.NomorDokumen = "Nomor Rekening";
                            myData.ProcInfo = "Update: Bukti Rekening BSM";
                            return View("_AddEditDD_2", myData);
                        case 61:
                            ViewBag.JudulDetail = "Penghargaan Yang Diterima";
                            ViewBag.NomorDokumen = "Judul Penghargaan";
                            myData.ProcInfo = "Update: Penghargaan Yang Diterima";
                            return View("_AddEditDD_2", myData);
                        case 21: //21	SK Pengangkatan PPAT
                            ViewBag.JudulDetail = "SK Pengangkatan PPAT";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: SK Pengangkatan PPAT";
                            return View("_AddEditDD_2", myData);
                        case 22: //22	Berita Acara Sumpah Notaris
                            ViewBag.JudulDetail = "Berita Acara Sumpah Notaris";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Berita Acara Sumpah Notaris";
                            return View("_AddEditDD_2", myData);
                        case 23: //23	Berita Acara Sumpah PPAT
                            ViewBag.JudulDetail = "Berita Acara Sumpah PPAT";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Berita Acara Sumpah PPAT";
                            return View("_AddEditDD_2", myData);

                        case 3: //3	Akta Pendirian
                            ViewBag.JudulDetail = "Akta Pendirian";
                            myData.ProcInfo = "Update: Akta Pendirian";
                            ViewBag.JudulCatatan = "Penjelasan Akta";
                            return View("_AddEditDD_3", myData);
                        case 65: //3	Akta Perubahan
                            ViewBag.JudulDetail = "Akta Perubahan";
                            myData.ProcInfo = "Update: Akta Perubahan";
                            ViewBag.JudulCatatan = "Inti Perubahan";
                            return View("_AddEditDD_3", myData);
                        case 5: //5	Izin Usaha/Operasional Dari Instansi yang Berwenang
                            ViewBag.JudulDetail = "Izin Usaha/Operasional Dari Instansi yang Berwenang";
                            myData.ProcInfo = "Update: Izin Usaha/Operasional Dari Instansi yang Berwenang";
                            return View("_AddEditDD_3", myData);
                        case 7: //3	Surat Tanda Daftar Perusahaan
                            ViewBag.JudulDetail = "Surat Tanda Daftar Perusahaan";
                            ViewBag.JudulCatatan = "Catatan";
                            myData.ProcInfo = "Update: Surat Tanda Daftar Perusahaan";
                            return View("_AddEditDD_3", myData);
                        case 75: //3	Berita Negara Republik Indonesia
                            ViewBag.JudulDetail = "Berita Negara Republik Indonesia";
                            ViewBag.JudulCatatan = "Catatan";
                            myData.ProcInfo = "Update: Berita Negara Republik Indonesia";
                            return View("_AddEditDD_3", myData);
                        case 76: //3	Ijin Perubahan Nama dari OJK
                            ViewBag.JudulDetail = "Ijin Perubahan Nama Dari OJK";
                            myData.ProcInfo = "Update: Ijin Perubahan Nama Dari OJK";
                            return View("_AddEditDD_3", myData);
                        case 56: //56	Surat Ijin Otoritas Jasa Keuangan
                            ViewBag.JudulDetail = "Surat Ijin Otoritas Jasa Keuangan";
                            myData.ProcInfo = "Update: Surat Ijin Otoritas Jasa Keuangan";
                            ViewBag.JudulCatatan = "Catatan";
                            return View("_AddEditDD_3", myData);

                        case 8: //3	NPWP Perusahaan
                            ViewBag.JudulDetail = "NPWP Perusahaan";
                            myData.ProcInfo = "Update: NPWP Perusahaan";
                            return View("_AddEditDD_8", myData);

                        case 32: //32	Bukti Kepemilikan / Sewa / Kontrak Tempat Usaha
                            myData.ProcInfo = "Update: Bukti Kepemilikan / Sewa / Kontrak Tempat Usaha";
                            return View("_AddEditDD_32", myData);
                        case 38: //38	Contoh Format Laporan
                            myData.ProcInfo = "Update: Contoh Format Laporan";
                            return View("_AddEditDD_38", myData);
                        //case 30: //30	Curriculum Vitae Pimpinan Rekan dan Rekan
                        //    return View("_AddEditDD_30", myData);
                        case 33: //33	Daftar Inventaris Perusahaan
                            myData.ProcInfo = "Update: Daftar Inventaris Perusahaan";
                            return View("_AddEditDD_33", myData);

                        case 27: //27	Daftar Tenaga Ahli Tetap
                            //if (tokenContainer.IdTypeOfRekanan.ToString() == "4" || tokenContainer.IdTypeOfRekanan.ToString() == "5")
                            //{
                                ViewBag.JudulDetail = "Daftar Tenaga Ahli";
                                myData.ProcInfo = "Update: Daftar Tenaga Ahli";
                                return View("_AddEditDD_41", myData);
                            //}
                            //else
                            //{
                            //    ViewBag.JudulDetail = "Daftar Tenaga Ahli Tetap";
                            //    myDataImp.InjectFrom(myData);
                            //    myData.ProcInfo = "Update: Daftar Tenaga Ahli Tetap";
                            //    return View("_AddEditDD_27", myDataImp);
                            //}
                        case 28: //28	Daftar Tenaga Ahli Tidak Tetap
                            ViewBag.JudulDetail = "Daftar Tenaga Ahli Tidak Tetap";
                            myData.ProcInfo = "Update: Daftar Tenaga Ahli Tidak Tetap";
                            //return View("_AddEditDD_28", myData);
                            return View("_AddEditDD_41", myData);
                        case 29: //29	Daftar Tenaga Pendukung
                            ViewBag.JudulDetail = "Daftar Tenaga Pendukung";
                            myData.ProcInfo = "Update: Daftar Tenaga Pendukung";
                            return View("_AddEditDD_29", myData);

                        case 15: //15	Undang - Undang Gangguan
                            ViewBag.JudulDetail = "Undang - Undang Gangguan";
                            ViewBag.JudulCatatan = "Catatan";
                            myData.ProcInfo = "Undang - Undang Gangguan";
                            return View("_AddEditDD_3", myData);
                        case 25: //25	Perjanjian Kerjasama dengan Kantor Akuntan Asing
                            myData.ProcInfo = "Update: Perjanjian Kerjasama dengan Kantor Akuntan Asing";
                            return View("_AddEditDD_25", myData);
                        case 24: //24	Perjanjian Persekutuan
                            myData.ProcInfo = "Update: Perjanjian Persekutuan";
                            return View("_AddEditDD_24", myData);
                        case 16: //16	Surat Keterangan Belum/Pernah Dikenakan Sanksi
                            ViewBag.JudulDetail = "Surat Keterangan Belum/Pernah Dikenakan Sanksi";
                            myData.ProcInfo = "Update: Surat Keterangan Belum/Pernah Dikenakan Sanksi";
                            return View("_AddEditDD_16", myData);
                        case 10: //10	Surat Keterangan Domisili Perusahaan (SKDP)
                            ViewBag.JudulDetail = "Surat Keterangan Domisili Perusahaan (SKDP)";
                            myData.ProcInfo = "Update: Surat Keterangan Domisili Perusahaan (SKDP)";
                            return View("_AddEditDD_16", myData);

                        case 1065: //1065	Daftar Rekanan Bank
                            ViewBag.JudulDetail = "Daftar Rekanan Bank";
                            ViewBag.JudulNama = "Rekanan Bank";
                            myData.ProcInfo = "Update: Daftar Rekanan Bank";
                            return View("_AddEditDD_30", myData);
                        case 1066: //1066	Daftar Rekanan Non Bank
                            ViewBag.JudulDetail = "Daftar Rekanan Non Bank";
                            ViewBag.JudulNama = "Rekanan Non Bank";
                            myData.ProcInfo = "Update: Daftar Rekanan Non Bank";
                            return View("_AddEditDD_30", myData);
                        case 1067: //1067	Daftar Pengusul
                            ViewBag.JudulDetail = "Daftar Pengusul";
                            ViewBag.JudulNama = "Nama Pengusul";
                            myData.ProcInfo = "Update: Daftar Pengusul";
                            return View("_AddEditDD_30", myData);
                        case 1069: //1069	Daftar Penilai Publik
                            ViewBag.JudulDetail = "Daftar Penilai Publik";
                            ViewBag.JudulNama = "Penilai Publik";
                            myData.ProcInfo = "Update: Daftar Penilai Publik";
                            return View("_AddEditDD_30", myData);

                        case 31: //31	Daftar Pengalaman (minimal 2 tahun terakhir)
                            myData.ProcInfo = "Update: Daftar Pengalaman (minimal 2 tahun terakhir)";
                            return View("_AddEditDD_31", myData);
                        case 34: //34	Laporan Keuangan (2 tahun terakhir)
                            ViewBag.JudulDetail = "Laporan Keuangan (2 tahun terakhir)";
                            myData.ProcInfo = "Update: Laporan Keuangan (2 tahun terakhir)";
                            return View(myData);
                        case 36: //36	Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KAP lain
                            myData.ProcInfo = "Update: Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KAP lain";
                            return View("_AddEditDD_36", myData);
                        case 37: //37	Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KJPP lain
                            ViewBag.JudulDetail = "Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KJPP lain";
                            myData.ProcInfo = "Update: Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KJPP lain";
                            return View(myData);
                        case 39: //39	Alur Bagan Pembuatan Laporan
                            ViewBag.JudulDetail = "Alur Bagan Pembuatan Laporan";
                            myData.ProcInfo = "Update: Alur Bagan Pembuatan Laporan";
                            return View(myData);
                        case 40: //40	Laporan Solvabilitas Lengkap Disertai Tanda Terima OJK
                            ViewBag.JudulDetail = "Laporan Solvabilitas Lengkap Disertai Tanda Terima OJK";
                            myData.ProcInfo = "Update: Laporan Solvabilitas Lengkap Disertai Tanda Terima OJK";
                            return View("_AddEditDD_40", myData);
                        case 41: //41	Daftar Rincian Nilai Treaty dari perusahaan ReAsuransi
                            ViewBag.JudulDetail = "Daftar Rincian Nilai Treaty dari perusahaan ReAsuransi";
                            myData.ProcInfo = "Update: Daftar Rincian Nilai Treaty dari perusahaan ReAsuransi";
                            return View("_AddEditDD_41", myData);
                        case 43: //43	Daftar ReAsuransi
                            ViewBag.JudulDetail = "Daftar ReAsuransi dan Rating";
                            myData.ProcInfo = "Update: Daftar ReAsuransi dan Rating";
                            return View("_AddEditDD_41", myData);
                        case 44: //44	Copy Keanggotaan Ikatan Notaris Indonesia (INI)
                            ViewBag.JudulDetail = "Copy Keanggotaan Ikatan Notaris Indonesia (INI)";
                            myData.ProcInfo = "Update: Copy Keanggotaan Ikatan Notaris Indonesia (INI)";
                            return View(myData);
                        case 45: //45	Copy Keanggotaan Ikatan Pejabat Pembuat Akta Tanah (IPPAT)
                            ViewBag.JudulDetail = "Copy Keanggotaan Ikatan Pejabat Pembuat Akta Tanah (IPPAT)";
                            myData.ProcInfo = "Update: Copy Keanggotaan Ikatan Pejabat Pembuat Akta Tanah (IPPAT)";
                            return View(myData);
                        case 46: //46	Keputusan Pengangkatan Notaris
                            ViewBag.JudulDetail = "Keputusan Pengangkatan Notaris";
                            myData.ProcInfo = "Update: Keputusan Pengangkatan Notaris";
                            return View(myData);
                        case 48: //48	Bukti Sertifikasi Tenaga Penilai
                            ViewBag.JudulDetail = "Bukti Sertifikasi Tenaga Penilai";
                            myData.ProcInfo = "Update: Bukti Sertifikasi Tenaga Penilai";
                            return View(myData);
                        case 54: //54	Laporan Keuangan (2 tahun terakhir) ASURANSI
                            ViewBag.JudulDetail = "Laporan Keuangan (2 tahun terakhir)";
                            myData.ProcInfo = "Update: Laporan Keuangan (2 tahun terakhir)";
                            return View("_AddEditDD_54", myData);
                        case 55: //55	Surat Pernyataan Tidak Menjabat Rangkap di KAP/KJPP lain
                            ViewBag.JudulDetail = "Surat Pernyataan Tidak Menjabat Rangkap di KAP/KJPP lain";
                            myData.ProcInfo = "Update: Surat Pernyataan Tidak Menjabat Rangkap di KAP/KJPP lain";
                            return View("_AddEditDD_41", myData);
                        case 57: //
                            ViewBag.JudulDetail = "Informasi Kepemilikan Saham Ultimate Shareholder di Perusahaan Lain (oleh Owner)";
                            myData.ProcInfo = "Update: Informasi Kepemilikan Saham Ultimate Shareholder di Perusahaan Lain (oleh Owner)";
                            return View("_AddEditDD_57", myData);
                        case 58: //58	Bukti Penggunaan Produk BSM
                            ViewBag.JudulDetail = "Bukti Penggunaan Produk BSM";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Bukti Penggunaan Produk BSM";
                            return View("_AddEditDD_2", myData);
                        case 59: //59	Surat Pernyataan Tidak Pernah Melanggar POJK no.73/POJK.05/2016
                            ViewBag.JudulDetail = "Surat Pernyataan Tidak Pernah Melanggar POJK no.73/POJK.05/2016";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Surat Pernyataan Tidak Pernah Melanggar POJK";
                            return View("_AddEditDD_2", myData);
                        case 62: //62	Copy Bukti Gelar
                            ViewBag.JudulDetail = "Copy Bukti Gelar";
                            ViewBag.NomorDokumen = "Nomor Dokumen";
                            myData.ProcInfo = "Update: Copy Bukti Gelar";
                            return View("_AddEditDD_2", myData);
                        default:
                            myData.ProcInfo = "Update: Other";
                            return View(myData);
                    }

	                #endregion                

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
                myData.JenisPermohonanColls = DataMasterProvider.Instance.JenisPermohonanColls.ToList();

                #region Switch Detail Dokumen

                myData.ProcInfo = "Update: UNKNOWN";
                switch (myData.IdTypeOfDocument)
                {
                    case 1: //1	Surat Permohonan
                        ViewBag.JudulDetail = "Surat Permohonan";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Surat Permohonan";
                        return View("_AddEditDD_1", myData);

                    case 2: //2	Company Profile
                        ViewBag.JudulDetail = "Company Profile";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Company Profile";
                        return View("_AddEditDD_2", myData);
                    case 18: //18	Standard Operating Procedure
                        ViewBag.JudulDetail = "Standard Operating Procedure";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Standard Operating Procedure";
                        return View("_AddEditDD_2", myData);
                    case 63: //18	Standard Operating Procedure
                        ViewBag.JudulDetail = "Daftar Pekerjaan 2 Tahun Terakhir";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Daftar Pekerjaan 2 Tahun Terakhir";
                        return View("_AddEditDD_2", myData);
                    case 64: //18	Standard Operating Procedure
                        ViewBag.JudulDetail = "Surat Pernyataan Bahwa Dokumen Dan Data Adalah Benar";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Surat Pernyataan Bahwa Dokumen Dan Data Adalah Benar";
                        return View("_AddEditDD_2", myData);
                    //50	Pakta Integritas
                    //51	Standar Pengendalian Mutu
                    //52	Surat Pernyataan dan Ketentuan Umum
                    case 50: //50	Pakta Integritas
                        ViewBag.JudulDetail = "Pakta Integritas";
                        myData.ProcInfo = "Update: Pakta Integritas";
                        return View("_AddEditDD_50", myData);
                    case 51: //51	Standar Pengendalian Mutu
                        ViewBag.JudulDetail = "Standar Pengendalian Mutu";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Standar Pengendalian Mutu";
                        return View("_AddEditDD_2", myData);
                    case 52: //52	Surat Pernyataan dan Ketentuan Umum
                        ViewBag.JudulDetail = "Surat Pernyataan dan Ketentuan Umum";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Surat Pernyataan dan Ketentuan Umum";
                        return View("_AddEditDD_2", myData);
                    case 19: //19	Struktur Organisasi Perusahaan
                        ViewBag.JudulDetail = "Struktur Organisasi Perusahaan";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Struktur Organisasi Perusahaan";
                        return View("_AddEditDD_2", myData);
                    case 60:
                        ViewBag.JudulDetail = "Bukti Rekening BSM";
                        ViewBag.NomorDokumen = "Nomor Rekening";
                        myData.ProcInfo = "Update: Bukti Rekening BSM";
                        return View("_AddEditDD_2", myData);
                    case 61:
                        ViewBag.JudulDetail = "Penghargaan Yang Diterima";
                        ViewBag.NomorDokumen = "Judul Penghargaan";
                        myData.ProcInfo = "Update: Penghargaan Yang Diterima";
                        return View("_AddEditDD_2", myData);
                    case 21: //21	SK Pengangkatan PPAT
                        ViewBag.JudulDetail = "SK Pengangkatan PPAT";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: SK Pengangkatan PPAT";
                        return View("_AddEditDD_2", myData);
                    case 22: //22	Berita Acara Sumpah Notaris
                        ViewBag.JudulDetail = "Berita Acara Sumpah Notaris";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Berita Acara Sumpah Notaris";
                        return View("_AddEditDD_2", myData);
                    case 23: //23	Berita Acara Sumpah PPAT
                        ViewBag.JudulDetail = "Berita Acara Sumpah PPAT";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Berita Acara Sumpah PPAT";
                        return View("_AddEditDD_2", myData);

                    case 3: //3	Akta Pendirian
                        ViewBag.JudulDetail = "Akta Pendirian";
                        myData.ProcInfo = "Update: Akta Pendirian";
                        ViewBag.JudulCatatan = "Penjelasan Akta";
                        return View("_AddEditDD_3", myData);
                    case 65: //3	Akta Perubahan
                        ViewBag.JudulDetail = "Akta Perubahan";
                        myData.ProcInfo = "Update: Akta Perubahan";
                        ViewBag.JudulCatatan = "Inti Perubahan";
                        return View("_AddEditDD_3", myData);
                    case 5: //5	Izin Usaha/Operasional Dari Instansi yang Berwenang
                        ViewBag.JudulDetail = "Izin Usaha/Operasional Dari Instansi yang Berwenang";
                        myData.ProcInfo = "Update: Izin Usaha/Operasional Dari Instansi yang Berwenang";
                        return View("_AddEditDD_3", myData);
                    case 7: //3	Surat Tanda Daftar Perusahaan
                        ViewBag.JudulDetail = "Surat Tanda Daftar Perusahaan";
                        ViewBag.JudulCatatan = "Catatan";
                        myData.ProcInfo = "Update: Surat Tanda Daftar Perusahaan";
                        return View("_AddEditDD_3", myData);
                    case 75: //3	Surat Tanda Daftar Perusahaan
                        ViewBag.JudulDetail = "Berita Negara Republik Indonesia";
                        ViewBag.JudulCatatan = "Catatan";
                        myData.ProcInfo = "Update: Berita Negara Republik Indonesia";
                        return View("_AddEditDD_3", myData);
                    case 76: //3	Ijin Perubahan Nama dari OJK
                        ViewBag.JudulDetail = "Ijin Perubahan Nama Dari OJK";
                        myData.ProcInfo = "Update: Ijin Perubahan Nama Dari OJK";
                        return View("_AddEditDD_3", myData);
                    case 56: //5	Surat Ijin Otoritas Jasa Keuangan
                        ViewBag.JudulDetail = "Surat Ijin Otoritas Jasa Keuangan";
                        myData.ProcInfo = "Update: Surat Ijin Otoritas Jasa Keuangan";
                        ViewBag.JudulCatatan = "Catatan";
                        return View("_AddEditDD_3", myData);

                    case 8: //3	NPWP Perusahaan
                        ViewBag.JudulDetail = "NPWP Perusahaan";
                        myData.ProcInfo = "Update: NPWP Perusahaan";
                        return View("_AddEditDD_8", myData);

                    case 32: //32	Bukti Kepemilikan / Sewa / Kontrak Tempat Usaha
                        myData.ProcInfo = "Update: Bukti Kepemilikan / Sewa / Kontrak Tempat Usaha";
                        return View("_AddEditDD_32", myData);
                    case 38: //38	Contoh Format Laporan
                        myData.ProcInfo = "Update: Contoh Format Laporan";
                        return View("_AddEditDD_38", myData);
                    //case 30: //30	Curriculum Vitae Pimpinan Rekan dan Rekan
                    //    return View("_AddEditDD_30", myData);
                    case 33: //33	Daftar Inventaris Perusahaan
                        myData.ProcInfo = "Update: Daftar Inventaris Perusahaan";
                        return View("_AddEditDD_33", myData);

                    case 27: //27	Daftar Tenaga Ahli Tetap
                        //if (tokenContainer.IdTypeOfRekanan.ToString() == "4" || tokenContainer.IdTypeOfRekanan.ToString() == "5")
                        //{
                            ViewBag.JudulDetail = "Daftar Tenaga Ahli";
                            myData.ProcInfo = "Update: Daftar Tenaga Ahli";
                            return View("_AddEditDD_41", myData);
                        //}
                        //else
                        //{
                        //    ViewBag.JudulDetail = "Daftar Tenaga Ahli Tetap";
                        //    myDataImp.InjectFrom(myData);
                        //    myData.ProcInfo = "Update: Daftar Tenaga Ahli Tetap";
                        //    return View("_AddEditDD_27", myDataImp);
                        //}
                    case 28: //28	Daftar Tenaga Ahli Tidak Tetap
                        ViewBag.JudulDetail = "Daftar Tenaga Ahli Tidak Tetap";
                        myData.ProcInfo = "Update: Daftar Tenaga Ahli Tidak Tetap";
                        //return View("_AddEditDD_28", myData);
                        return View("_AddEditDD_41", myData);
                    case 29: //29	Daftar Tenaga Pendukung
                        ViewBag.JudulDetail = "Daftar Tenaga Pendukung";
                        myData.ProcInfo = "Update: Daftar Tenaga Pendukung";
                        return View("_AddEditDD_29", myData);

                    case 15: //15	Undang - Undang Gangguan
                        ViewBag.JudulDetail = "Undang - Undang Gangguan";
                        ViewBag.JudulCatatan = "Catatan";
                        myData.ProcInfo = "Undang - Undang Gangguan";
                        return View("_AddEditDD_3", myData);
                    case 25: //25	Perjanjian Kerjasama dengan Kantor Akuntan Asing
                        myData.ProcInfo = "Update: Perjanjian Kerjasama dengan Kantor Akuntan Asing";
                        return View("_AddEditDD_25", myData);
                    case 24: //24	Perjanjian Persekutuan
                        myData.ProcInfo = "Update: Perjanjian Persekutuan";
                        return View("_AddEditDD_24", myData);
                    case 16: //16	Surat Keterangan Belum/Pernah Dikenakan Sanksi
                        ViewBag.JudulDetail = "Surat Keterangan Belum/Pernah Dikenakan Sanksi";
                        myData.ProcInfo = "Update: Surat Keterangan Belum/Pernah Dikenakan Sanksi";
                        return View("_AddEditDD_16", myData);
                    case 10: //10	Surat Keterangan Domisili Perusahaan (SKDP)
                        ViewBag.JudulDetail = "Surat Keterangan Domisili Perusahaan (SKDP)";
                        myData.ProcInfo = "Update: Surat Keterangan Domisili Perusahaan (SKDP)";
                        return View("_AddEditDD_16", myData);

                    case 1065: //1065	Daftar Rekanan Bank
                        ViewBag.JudulDetail = "Daftar Rekanan Bank";
                        ViewBag.JudulNama = "Rekanan Bank";
                        myData.ProcInfo = "Update: Daftar Rekanan Bank";
                        return View("_AddEditDD_30", myData);
                    case 1066: //1066	Daftar Rekanan Non Bank
                        ViewBag.JudulDetail = "Daftar Rekanan Non Bank";
                        ViewBag.JudulNama = "Rekanan Non Bank";
                        myData.ProcInfo = "Update: Daftar Rekanan Non Bank";
                        return View("_AddEditDD_30", myData);
                    case 1067: //1067	Daftar Pengusul
                        ViewBag.JudulDetail = "Daftar Pengusul";
                        ViewBag.JudulNama = "Nama Pengusul";
                        myData.ProcInfo = "Update: Daftar Pengusul";
                        return View("_AddEditDD_30", myData);
                    case 1069: //1069	Daftar Penilai Publik
                        ViewBag.JudulDetail = "Daftar Penilai Publik";
                        ViewBag.JudulNama = "Penilai Publik";
                        myData.ProcInfo = "Update: Daftar Penilai Publik";
                        return View("_AddEditDD_30", myData);

                    case 31: //31	Daftar Pengalaman (minimal 2 tahun terakhir)
                        myData.ProcInfo = "Update: Daftar Pengalaman (minimal 2 tahun terakhir)";
                        return View("_AddEditDD_31", myData);
                    case 34: //34	Laporan Keuangan (2 tahun terakhir)
                        ViewBag.JudulDetail = "Laporan Keuangan (2 tahun terakhir)";
                        myData.ProcInfo = "Update: Laporan Keuangan (2 tahun terakhir)";
                        return View(myData);
                    case 36: //36	Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KAP lain
                        myData.ProcInfo = "Update: Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KAP lain";
                        return View("_AddEditDD_36", myData);
                    case 37: //37	Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KJPP lain
                        ViewBag.JudulDetail = "Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KJPP lain";
                        myData.ProcInfo = "Update: Surat Pernyataan Tidak Ada Tenaga Ahli yang rangkap pekerjaan di KJPP lain";
                        return View(myData);
                    case 39: //39	Alur Bagan Pembuatan Laporan
                        ViewBag.JudulDetail = "Alur Bagan Pembuatan Laporan";
                        myData.ProcInfo = "Update: Alur Bagan Pembuatan Laporan";
                        return View(myData);
                    case 40: //40	Laporan Solvabilitas Lengkap Disertai Tanda Terima OJK
                        ViewBag.JudulDetail = "Laporan Solvabilitas Lengkap Disertai Tanda Terima OJK";
                        myData.ProcInfo = "Update: Laporan Solvabilitas Lengkap Disertai Tanda Terima OJK";
                        return View("_AddEditDD_40", myData);
                    case 41: //41	Daftar Rincian Nilai Treaty dari perusahaan ReAsuransi
                        ViewBag.JudulDetail = "Daftar Rincian Nilai Treaty dari perusahaan ReAsuransi";
                        myData.ProcInfo = "Update: Daftar Rincian Nilai Treaty dari perusahaan ReAsuransi";
                        return View("_AddEditDD_41",myData);
                    case 43: //43	Daftar ReAsuransi
                        ViewBag.JudulDetail = "Daftar ReAsuransi dan Rating";
                        myData.ProcInfo = "Update: Daftar ReAsuransi dan Rating";
                        return View("_AddEditDD_41", myData);
                    case 44: //44	Copy Keanggotaan Ikatan Notaris Indonesia (INI)
                        ViewBag.JudulDetail = "Copy Keanggotaan Ikatan Notaris Indonesia (INI)";
                        myData.ProcInfo = "Update: Copy Keanggotaan Ikatan Notaris Indonesia (INI)";
                        return View(myData);
                    case 45: //45	Copy Keanggotaan Ikatan Pejabat Pembuat Akta Tanah (IPPAT)
                        ViewBag.JudulDetail = "Copy Keanggotaan Ikatan Pejabat Pembuat Akta Tanah (IPPAT)";
                        myData.ProcInfo = "Update: Copy Keanggotaan Ikatan Pejabat Pembuat Akta Tanah (IPPAT)";
                        return View(myData);
                    case 46: //46	Keputusan Pengangkatan Notaris
                        ViewBag.JudulDetail = "Keputusan Pengangkatan Notaris";
                        myData.ProcInfo = "Update: Keputusan Pengangkatan Notaris";
                        return View(myData);
                    case 48: //48	Bukti Sertifikasi Tenaga Penilai
                        ViewBag.JudulDetail = "Bukti Sertifikasi Tenaga Penilai";
                        myData.ProcInfo = "Update: Bukti Sertifikasi Tenaga Penilai";
                        return View(myData);
                    case 54: //54	Laporan Keuangan (2 tahun terakhir) ASURANSI
                        ViewBag.JudulDetail = "Laporan Keuangan (2 tahun terakhir)";
                        myData.ProcInfo = "Update: Laporan Keuangan (2 tahun terakhir)";
                        return View("_AddEditDD_54", myData);
                    case 55: //55	Surat Pernyataan Tidak Menjabat Rangkap di KAP/KJPP lain
                        ViewBag.JudulDetail = "Surat Pernyataan Tidak Menjabat Rangkap di KAP/KJPP lain";
                        myData.ProcInfo = "Update: Surat Pernyataan Tidak Menjabat Rangkap di KAP/KJPP lain";
                        return View("_AddEditDD_41", myData);
                    case 57: //
                        ViewBag.JudulDetail = "Informasi Kepemilikan Saham Ultimate Shareholder di Perusahaan Lain (oleh Owner)";
                        myData.ProcInfo = "Update: Informasi Kepemilikan Saham Ultimate Shareholder di Perusahaan Lain (oleh Owner)";
                        return View("_AddEditDD_57", myData);
                    case 58: //58	Bukti Penggunaan Produk BSM
                        ViewBag.JudulDetail = "Bukti Penggunaan Produk BSM";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Bukti Penggunaan Produk BSM";
                        return View("_AddEditDD_2", myData);
                    case 59: //59	Surat Pernyataan Tidak Pernah Melanggar POJK no.73/POJK.05/2016
                        ViewBag.JudulDetail = "Surat Pernyataan Tidak Pernah Melanggar POJK no.73/POJK.05/2016";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Surat Pernyataan Tidak Pernah Melanggar POJK";
                        return View("_AddEditDD_2", myData);
                    case 62: //62	Copy Bukti Gelar
                        ViewBag.JudulDetail = "Copy Bukti Gelar";
                        ViewBag.NomorDokumen = "Nomor Dokumen";
                        myData.ProcInfo = "Update: Copy Bukti Gelar";
                        return View("_AddEditDD_2", myData);
                    default:
                        myData.ProcInfo = "Update: Other";
                        return View(myData);
                }

                #endregion
                //return View(myData);
            }
        }

        [HttpPost]
        public async Task<ActionResult> _AddEditDocDetail(trxDocMandatoryDetail myData)
        {
            //myData.FileExt = ImageContainerUpd.ImageExtension;
            //myData.FileExt2 = ImageContainerUpd.ImageExtension2;

            if (myData.IdDocMandatoryDetail > 0)
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + myData.IdDocMandatoryDetail, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    //jika tipenya asuransi dan dokumen tenaga ahli maka redirect ke data organisasi
                    //if ((tokenContainer.IdTypeOfRekanan.ToString() == "4" || tokenContainer.IdTypeOfRekanan.ToString() == "5") && myData.IdTypeOfDocument == 27)
                    if (myData.IdTypeOfDocument == 27 || myData.IdTypeOfDocument == 28)
                    {
                        return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                    }
                    else
                    {
                        return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
                    }
                }
                return RedirectToAction("Error");
            }
            else
            {
                myData.CreatedDate = DateTime.Today;
                myData.CreatedUser = tokenContainer.UserId.ToString();
                myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    //jika tipenya asuransi dan dokumen tenaga ahli maka redirect ke data organisasi
                    //if ((tokenContainer.IdTypeOfRekanan.ToString() == "4" || tokenContainer.IdTypeOfRekanan.ToString() == "5") && myData.IdTypeOfDocument == 27)
                    if (myData.IdTypeOfDocument == 27 || myData.IdTypeOfDocument == 28)
                    {
                        return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                    }
                    else
                    {
                        return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
                    }
                }
                return RedirectToAction("Error");
            }
        }

        public ActionResult Create()
        {
            Guid myImageBaseName = Guid.NewGuid();
            trxDocMandatoryDetail myData = new trxDocMandatoryDetail();
            ImageContainerUpd.ImageBaseName = myImageBaseName;
            myData.ImageBaseName = myImageBaseName;
            return View(myData);
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(trxDocMandatoryDetail myData)
        {
            myData.CreatedDate = DateTime.Today;
            myData.CreatedUser = tokenContainer.UserId.ToString();
            myData.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, myData);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int IdDetDocument)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + IdDetDocument);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxDocMandatoryDetail>(responseData);
                return View(myData);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(int IdDetDocument, trxDocMandatoryDetail Emp)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + IdDetDocument, Emp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxRekananDocument");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int IdDetDocument)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdDetDocument.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                //if (myData.IdTypeOfDocument == 27 || myData.IdTypeOfDocument == 28)
                //{
                //    return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
                //}
                //else
                //{
                //    return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
                //}

                //return RedirectToAction("GetByRekanan", "TrxRekananDocument");
                return RedirectToAction("GetByRekananTab", "TrxDocMandatoryDetail");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> DeleteTA(int IdDetDocument)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + IdDetDocument.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetByRekanan", "TrxDataOrganisasi");
            }
            return RedirectToAction("Error");
        }
        public FileResult DisplayPDF()
        {
            string PdfFileName = Server.MapPath("~/Content/DocumentImages/172283c6-ef5e-42f6-98ae-50c55523d414.pdf");
            return File(PdfFileName, "application/pdf");
        }

        //public PartialViewResult PDFInCrome()
        //{
        //    string filepath = Server.MapPath("~/Content/DocumentImages/172283c6-ef5e-42f6-98ae-50c55523d414.pdf");
        //    byte[] pdfByte = Helper.GetBytesFromFile(filepath);
        //    var strBase64 = Convert.ToBase64String(pdfByte);
        //    PDFCrome pdfCrome = new PDFCrome();
        //    pdfCrome.Content = string.Format("data:application/pdf;base64,{0}", strBase64);
        //    return PartialView(pdfCrome);
        //}

        public FilePathResult DownloadTempPekerjaan(string fileName)
        {
            string strFileLocation = Server.MapPath("~/Content/FileDownload/");
            strFileLocation = strFileLocation + fileName + ".xlsx";
            return new FilePathResult(strFileLocation, "application/vnd.ms-excel");
        }

        public ActionResult _ImportTenagaAhliTetap()
        {
            List<trxTenagaAhliTetapImp> myDataList = new List<trxTenagaAhliTetapImp>();
            return PartialView(myDataList);
        }

        [HttpPost]
        public ActionResult _ImportTenagaAhliTetap(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/FileUpload/") + Request.Files["file"].FileName;
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
                //if (fileExtension.ToString().ToLower().Equals(".xml"))
                //{
                //    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                //    if (System.IO.File.Exists(fileLocation))
                //    {
                //        System.IO.File.Delete(fileLocation);
                //    }

                //    Request.Files["FileUpload"].SaveAs(fileLocation);
                //    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                //    // DataSet ds = new DataSet();
                //    ds.ReadXml(xmlreader);
                //    xmlreader.Close();
                //}

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    //SqlConnection con = new SqlConnection(conn);
                    //string query = "Insert into Person(Name,Email,Mobile) Values('" + ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "')";
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.ExecuteNonQuery();
                    //con.Close();
                }
                return PartialView();
            }
            return View("Error");
        }

        public ActionResult ImageUpload(string UploaderCtl = "uploadControl")
        {
            switch (UploaderCtl)
            {
                case "uploadControl1":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_1";
                    break;
                case "uploadControl2":
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName + "_2";
                    break;
                default:
                    ImageContainerUpd.ImageScanned = ImageContainerUpd.ImageBaseName.ToString();
                    break;
            }
            //UploadedFile[] arrUploaded = UploadControlExtension.GetUploadedFiles(UploaderCtl, UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);
            return null;
        }
        public ActionResult GetByRekananTab()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTotDocumentDetailByRek/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                if (tokenContainer.RoleName.ToString() == "PCPKAR")
                {
                    //MstRekanan/RekananDetailedInfo?IdRekanan=2e70c9a3-36c7-4f5c-b2f8-a78c414d598f&IdTypeOfRekanan=4&RegistrationNumber=ASJ123
                    return RedirectToAction("RekananDetailedInfo", "MstRekanan"
                        , new { IdRekanan = tokenContainer.IdRekananContact.ToString(), IdTypeOfRekanan = (int)tokenContainer.IdTypeOfRekanan, RegistrationNumber = tokenContainer.Keterangan });
                }
                else
                {
                    return View(myData);
                }
            }
            return View("Error");
        }
        public ActionResult _GetByRekananTab()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTotDocumentDetailByRek/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                return PartialView("GetByRekananTab", myData);
            }
            return View("Error");
        }

        public ActionResult _GetByRekananTab_Admin()
        {
            HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTotDocumentDetailByRek/" + tokenContainer.IdRekananContact.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                return PartialView("GetByRekananTab_Admin", myData);
            }
            return View("Error");
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UpdateLineVerified(fTotDocumentDetailByRek_Result myVerificationData)
        {
            trxDocMandatoryVerification mySingle = new trxDocMandatoryVerification();
            mySingle.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
            mySingle.IdTypeOfDocument = myVerificationData.IdTypeOfDocument;
            mySingle.IsVerified = myVerificationData.IsVerified;
            mySingle.Catatan = myVerificationData.Catatan;
            mySingle.CreatedUser = tokenContainer.UserId.ToString();
            mySingle.CreatedDate = DateTime.Now;

            HttpResponseMessage responseMessage2 = client.PostAsJsonAsync(string.Format("{0}/StoreVerificationAdmin", url), mySingle).Result;
            if (responseMessage2.IsSuccessStatusCode)
            {
                //49e48288-247a-4173-adb8-18602e1169a2
                HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTotDocumentDetailByRek/" + tokenContainer.IdRekananContact.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                    return PartialView("GetByRekananTab_Admin", myData);
                }
            }
            return RedirectToAction("Error");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchUpdatePartial(MVCxGridViewBatchUpdateValues<fTotDocumentDetailByRek_Result, int> batchValues)
        {
            List<trxDocMandatoryVerification> myList = new List<trxDocMandatoryVerification>();
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                {
                    trxDocMandatoryVerification mySingle = new trxDocMandatoryVerification();
                    mySingle.IdRekanan = (System.Guid)tokenContainer.IdRekananContact;
                    mySingle.IdTypeOfDocument = item.IdTypeOfDocument;
                    mySingle.IsVerified = item.IsVerified;
                    mySingle.Catatan = item.Catatan;
                    mySingle.CreatedUser = tokenContainer.UserId.ToString();
                    mySingle.CreatedDate = DateTime.Now;
                    myList.Add(mySingle);
                }
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            HttpResponseMessage responseMessage2 = client.PostAsJsonAsync(string.Format("{0}/StoreVerificationAdmin", url), myList).Result;
            if (responseMessage2.IsSuccessStatusCode)
            {
                //49e48288-247a-4173-adb8-18602e1169a2
                HttpResponseMessage responseMessage = client.GetAsync(url + "/GetTotDocumentDetailByRek/" + tokenContainer.IdRekananContact.ToString()).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var myData = JsonConvert.DeserializeObject<trxRekananDocumentMulti>(responseData);
                    return PartialView("GetByRekananTab_Admin", myData);
                }
            }
            return View("Error");
        }
    }

    public static class Helper
    {
        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }
    }
}
