using System;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.Http;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using DevExpress.Web.Mvc;

namespace MVCSmartClient01.Controllers
{

    //public sealed class ImageContainer
    //{
    //    private static ImageContainer single = new ImageContainer();
    //    public static ImageContainer Instance
    //    {
    //        get { return single; }
    //    }
    //    public Guid? ImageBaseName { get; set; }
    //    public string ImageScanned { get; set; }
    //    public string ImageExtension { get; set; }
    //    public string ImageExtensionKTP { get; set; }
    //    public string ImageExtensionNPWP { get; set; }
    //    public string ImageExtensionIAPI { get; set; }
    //    public string ImageExtension1 { get; set; }
    //    public string ImageExtension2 { get; set; }
    //    public string ImageExtension3 { get; set; }
    //}

    public class ImageContainerUpd
    {
        public static Guid? ImageBaseName
        {
            get
            {
                Guid ImgNew = Guid.NewGuid();
                ImgNew = (System.Web.HttpContext.Current.Session["ImageBaseName"] == null ? Guid.NewGuid() : (Guid)System.Web.HttpContext.Current.Session["ImageBaseName"]);
                return ImgNew;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageBaseName"] = value;
            }
        }
        //public string ImageScanned { get; set; }
        public static string ImageScanned
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageScanned"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageScanned"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageScanned"] = value;
            }
        }
        //public string ImageExtension { get; set; }
        //public static string ImageFileName
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFileName"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFileName"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFileName"] = value;
        //    }
        //}
        public static string ImageExtension
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtension"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtension"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtension"] = value;
            }
        }
        //public string ImageExtensionKTP { get; set; }
        //public static string ImageFileKTP
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFileKTP"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFileKTP"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFileKTP"] = value;
        //    }
        //}
        public static string ImageExtensionKTP
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionKTP"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionKTP"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionKTP"] = value;
            }
        }
        //public string ImageExtensionNPWP { get; set; }
        //public static string ImageFileNPWP
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFileNPWP"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFileNPWP"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFileNPWP"] = value;
        //    }
        //}
        public static string ImageExtensionNPWP
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionNPWP"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionNPWP"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionNPWP"] = value;
            }
        }
        //public string ImageExtensionIAPI { get; set; }
        //public static string ImageFileIAPI
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFileIAPI"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFileIAPI"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFileIAPI"] = value;
        //    }
        //}
        public static string ImageExtensionIAPI
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionIAPI"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionIAPI"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionIAPI"] = value;
            }
        }
        public static string ImageExtensionCV
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionCV"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionCV"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionCV"] = value;
            }
        }
        public static string ImageExtensionIzin
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionIzin"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionIzin"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionIzin"] = value;
            }
        }
        public static string ImageExtensionGelar
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionGelar"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionGelar"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionGelar"] = value;
            }
        }
        //public string ImageExtension1 { get; set; }
        //public static string ImageFile1
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFile1"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFile1"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFile1"] = value;
        //    }
        //}
        public static string ImageExtension1
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtension1"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtension1"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtension1"] = value;
            }
        }
        //public string ImageExtension2 { get; set; }
        //public static string ImageFile2
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFile2"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFile2"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFile2"] = value;
        //    }
        //}
        public static string ImageExtension2
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtension2"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtension2"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtension2"] = value;
            }
        }
        //public string ImageExtension3 { get; set; }
        //public static string ImageFile3
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFile3"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFile3"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFile3"] = value;
        //    }
        //}
        public static string ImageExtension3
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtension3"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtension3"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtension3"] = value;
            }
        }
        //public static string ImageFileKOP
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFileKOP"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFileKOP"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFileKOP"] = value;
        //    }
        //}
        public static string ImageExtensionKOP
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionKOP"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionKOP"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionKOP"] = value;
            }
        }
        //public static string ImageFilePAS
        //{
        //    get
        //    {
        //        string strReturn = string.Empty;
        //        strReturn = (System.Web.HttpContext.Current.Session["ImageFilePAS"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageFilePAS"]);
        //        return strReturn;
        //    }
        //    set
        //    {
        //        System.Web.HttpContext.Current.Session["ImageFilePAS"] = value;
        //    }
        //}
        public static string ImageExtensionPAS
        {
            get
            {
                string strReturn = string.Empty;
                strReturn = (System.Web.HttpContext.Current.Session["ImageExtensionPAS"] == null ? string.Empty : (string)System.Web.HttpContext.Current.Session["ImageExtensionPAS"]);
                return strReturn;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ImageExtensionPAS"] = value;
            }
        }

    }

    public class UploadControlHelper
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

            e.UploadedFile.SaveAs(HttpContext.Current.Request.MapPath(resultFilePath));

            IUrlResolutionService urlResolver = sender as IUrlResolutionService;
            if (urlResolver != null)
                e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath) + "?refresh=" + Guid.NewGuid().ToString();
        }
    }
}
