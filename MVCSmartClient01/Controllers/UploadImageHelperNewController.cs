using System;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.Http;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using DevExpress.Web.Mvc;
using System.Linq;
using MVCSmartClient01.Models;

namespace MVCSmartClient01.Controllers
{
    public class UploadControlHelperNewController : BaseController
    {
        //private readonly ITokenContainer tokenContainer;
        public static readonly DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings =
            new DevExpress.Web.ASPxUploadControl.ValidationSettings
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".png", ".pdf" },
                MaxFileSize = 4000000
            };

        public static void uploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            //string strFileNameBase = ImageContainerUpd.ImageBaseName.ToString();
            string strUpload = ((DevExpress.Web.Mvc.MVCxUploadControl)sender).ClientID;
            string strFileNameBase = ImageContainerUpd.ImageScanned.ToString();
            string strFileName = e.UploadedFile.FileName;
            string strImageExtension = Path.GetExtension(e.UploadedFile.FileName);
            //ImageContainerUpd.ImageExtension = strImageExtension;

            switch (strUpload)
            {
                case "uploadControlKTP":
                    //ImageContainerUpd.ImageFileKTP = strFileName;
                    ImageContainerUpd.ImageExtensionKTP = strImageExtension;
                    break;
                case "uploadControlNPWP":
                    //ImageContainerUpd.ImageFileNPWP = strFileName;
                    ImageContainerUpd.ImageExtensionNPWP = strImageExtension;
                    break;
                case "uploadControlIAPI":
                    //ImageContainerUpd.ImageFileIAPI = strFileName;
                    ImageContainerUpd.ImageExtensionIAPI = strImageExtension;
                    break;
                case "uploadControl1":
                    //ImageContainerUpd.ImageFileName = strFileName; //untuk DocMandatoryDetail
                    //ImageContainerUpd.ImageFile1 = strFileName;

                    ImageContainerUpd.ImageExtension = strImageExtension; //untuk DocMandatoryDetail
                    ImageContainerUpd.ImageExtension1 = strImageExtension;
                    break;
                case "uploadControl2":
                    //ImageContainerUpd.ImageFile2 = strFileName;
                    ImageContainerUpd.ImageExtension2 = strImageExtension;
                    break;
                case "uploadControl3":
                    //ImageContainerUpd.ImageFile3 = strFileName;
                    ImageContainerUpd.ImageExtension3 = strImageExtension;
                    break;
                case "UCKeputusanMenKop":
                    //ImageContainerUpd.ImageFileKOP = strFileName;
                    ImageContainerUpd.ImageExtensionKOP = strImageExtension;
                    break;
                case "UCSTTNPasarModal":
                    //ImageContainerUpd.ImageFilePAS = strFileName;
                    ImageContainerUpd.ImageExtensionPAS = strImageExtension;
                    break;
                default:
                    //ImageContainerUpd.ImageFileName = strFileName;
                    ImageContainerUpd.ImageExtension = strImageExtension;
                    break;
            }

            string resultFilePath = "~/Content/DocumentImages/" + string.Format("{0}{1}", strFileNameBase, strImageExtension);

            //e.UploadedFile.SaveAs(HttpContext.Current.Request.MapPath(resultFilePath));

            IUrlResolutionService urlResolver = sender as IUrlResolutionService;
            if (urlResolver != null)
                e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath) + "?refresh=" + Guid.NewGuid().ToString();
        }


        private string videoAddress = "~/Content/DocumentImages";

        [System.Web.Mvc.HttpPost]
        public string MultiUpload(string id, string fileNameOri, string myFileName)
        {
            var chunkNumber = id;
            var chunks = Request.InputStream;
            string path = Server.MapPath(videoAddress + "/Temp");
            string newpath = Path.Combine(path, myFileName + chunkNumber);
            using (FileStream fs = System.IO.File.Create(newpath))
            {
                byte[] bytes = new byte[3757000];
                int bytesRead;
                while ((bytesRead = Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, bytesRead);
                }
            }
            return "done";
        }

        public ActionResult _DaftarFile()
        {
            return PartialView("_DaftarFile");
        }

        [System.Web.Mvc.HttpPost]
        //public string UploadComplete(string fileName, string complete)
        public ActionResult UploadComplete(string fileName, string complete)
        {
            string tempPath = Server.MapPath(videoAddress + "/Temp");
            string videoPath = Server.MapPath(videoAddress);
            string newPath = Path.Combine(tempPath, fileName);
            if (complete == "1")
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(tempPath).Where(p => p.Contains(fileName)).OrderBy(p => Int32.Parse(p.Replace(fileName, "$").Split('$')[1])).ToArray();
                    foreach (string filePath in filePaths)
                    {
                        MergeFiles(newPath, filePath);
                    }
                }
                catch(Exception ex)
                {
                    string aaa = ex.Message;
                }
            }

            try
            {
                System.IO.File.Copy(Path.Combine(tempPath, fileName), Path.Combine(videoPath, fileName), true);
                System.IO.File.Delete(Path.Combine(tempPath, fileName));

                //System.IO.File.Move(Path.Combine(tempPath, fileName), Path.Combine(videoPath, fileName));
            }
            catch(Exception ex)
            {
                string bbb = ex.Message;
            }
            //return "success";
            //jika sukses maka load daftar filenya
            ViewBag.ImageBaseName = newPath;
            return PartialView("_DaftarFile");
        }

        private static void MergeFiles(string file1, string file2)
        {
            FileStream fs1 = null;
            FileStream fs2 = null;
            try
            {
                fs1 = System.IO.File.Open(file1, FileMode.Append);
                fs2 = System.IO.File.Open(file2, FileMode.Open);
                byte[] fs2Content = new byte[fs2.Length];
                fs2.Read(fs2Content, 0, (int)fs2.Length);
                fs1.Write(fs2Content, 0, (int)fs2.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            }
            finally
            {
                if (fs1 != null) fs1.Close();
                if (fs2 != null) fs2.Close();
                System.IO.File.Delete(file2);
            }
        }
    
    
    }
}
