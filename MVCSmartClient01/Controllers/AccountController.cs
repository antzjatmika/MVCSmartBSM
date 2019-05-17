using System;
using System.Web.Mvc;
using MVCSmartClient01.Models;
using System.Web.Routing;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MVCSmartClient01.Controllers
{
    using System.Threading.Tasks;
    using ApiHelper;
    using ApiHelper.Client;
    using ApiInfrastructure;
    using ApiInfrastructure.Client;

    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    public class AccountController : BaseController
    {
        //string url = "http://localhost:2070";
        private readonly ILoginClient loginClient;
        private readonly ITokenContainer tokenContainer;
        HttpClient client;
        /// <summary>
        /// Default parameterless constructor. 
        /// Delete this if you are using a DI container.
        /// </summary>
        public AccountController()
        {
            client = new HttpClient(); 
            tokenContainer = new TokenContainer();
            var apiClient = new ApiClient(HttpClientInstance.Instance, tokenContainer);
            loginClient = new LoginClient(apiClient);
        }

        /// <summary>
        /// Default constructor with dependency.
        /// Delete this if you aren't planning on using a DI container.
        /// </summary>
        /// <param name="loginClient">The login client.</param>
        /// <param name="tokenContainer">The token container.</param>
        public AccountController(ILoginClient loginClient, ITokenContainer tokenContainer)
        {
            client = new HttpClient();  
            this.loginClient = loginClient;
            this.tokenContainer = tokenContainer;
        }

        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var loginSuccess = false;
            try
            {
                loginSuccess = await PerformLoginActions(model.UserName, model.Password);
            }
            catch(Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "The username or password is incorrect");
                return View(model);
            }
            vwUserRole currentRole = new vwUserRole();
            if (loginSuccess)
            {
                string urlModel = ConfigurationManager.AppSettings["SmartAPIUrl"];
                List<vwUserRole> myUserRole = new List<vwUserRole>() { };
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/api/VwUserRole/{1}", urlModel, model.UserName));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    System.Guid aaa = System.Guid.Empty;

                    myUserRole = JsonConvert.DeserializeObject<List<vwUserRole>>(responseData);
                    if (myUserRole.Count > 0)
                    {
                        currentRole = myUserRole[0];
                        tokenContainer.TokenRole = new ApiHelper.Models.vwUserRole
                        {
                            id = currentRole.id
                            , Email = currentRole.Email
                            , UserName = currentRole.UserName
                            , IdSupervisor = currentRole.IdSupervisor
                            , RoleId = currentRole.RoleId
                            , Name = currentRole.Name
                            , IdRekananContact = currentRole.IdRekananContact
                            , IdNotaris = currentRole.IdNotaris
                            , IdOrganisasi = currentRole.IdOrganisasi
                        };
                        tokenContainer.RoleName = currentRole.Name;
                        tokenContainer.UserId = currentRole.id;
                        tokenContainer.SupervisorId = currentRole.IdSupervisor;
                        tokenContainer.IdRekananContact = currentRole.IdRekananContact;
                        tokenContainer.IdNotaris = currentRole.IdNotaris;
                        tokenContainer.IdOrganisasi = currentRole.IdOrganisasi;
                        tokenContainer.UserName = currentRole.UserName;
                        tokenContainer.UserEmail = currentRole.Email;
                        tokenContainer.IdTypeOfRekanan = currentRole.IdTypeOfRekanan;
                    }
                }

                string strRoleName = tokenContainer.RoleName.ToString();
                if (tokenContainer.RoleName.ToString() == "Rekanan")
                {
                    strRoleName = tokenContainer.RoleName.ToString() + tokenContainer.IdTypeOfRekanan.ToString();
                    tokenContainer.RoleName = strRoleName;
                }
                //rekanan
                //pcp 
                //kar
                //admin sistem
                //portfolio
                //return RedirectToAction("Index", "HomeRekanan", new { id = tokenContainer.RoleName, idType = tokenContainer.IdTypeOfRekanan });
                return RedirectToAction("Index", new RouteValueDictionary(
                                        new { controller = "HomeRekanan", action = "Index", id = strRoleName }));
            }

            ModelState.Clear();
            ModelState.AddModelError("", "The username or password is incorrect");
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            var logoutSuccess = await PerformLogout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var myTypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
            var model = new RegisterBindingModel();
            model.TypeOfRekananColls = myTypeOfRekananColls.Where(x => x.IdTypeOfRekanan <= 7).ToList();
            //model.NamaRekanan = "awalan123";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterBindingModel model)
        {

            model.Password = "A@123_qwe";
            model.ConfirmPassword = "A@123_qwe";
            var response = await loginClient.Register(model);
            if (response.StatusIsSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }

            AddResponseErrorsToModelState(response);

            var myTypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
            model.TypeOfRekananColls = myTypeOfRekananColls.Where(x => x.IdTypeOfRekanan <= 7).ToList();

            return View(model);
        }
        //REGISTER CALON REKANAN LEWAT KIOKS
        public ActionResult RegisterByKiosk()
        {
            var myTypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
            var model = new RegisterBindingModel();
            model.TypeOfRekananColls = myTypeOfRekananColls.Where(x => x.IdTypeOfRekanan <= 7 || x.IdTypeOfRekanan == 11).ToList();
            return View(model);
        }
        public ActionResult AfterRegister()
        {
            return View("AfterRegister");
        }
        //REGISTER CALON REKANAN LEWAT KIOKS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterByKiosk(RegisterBindingModel model)
        {
            string strBarePassword = GenerateKataKunci();
            string strPassKey = string.Concat(strBarePassword, "@Pcp");
            model.NomorNPWP = Guid.NewGuid().ToString();
            model.Password = strPassKey;
            model.ConfirmPassword = strPassKey;
            model.BarePassword = MaskPasskey(strBarePassword, true);
            model.IsActive = 2;
            var response = await loginClient.RegisterByAdmin(model);
            if (response.StatusIsSuccessful)
            {
                //return RedirectToAction("Index", "Home");
                return View("AfterRegister");
            }

            AddResponseErrorsToModelState(response);

            var myTypeOfRekananColls = DataMasterProvider.Instance.TypeOfRekananColls;
            model.TypeOfRekananColls = myTypeOfRekananColls.Where(x => x.IdTypeOfRekanan <= 7 || x.IdTypeOfRekanan == 11).ToList();

            //return View(model);
            //return View("RegisterByKiosk");
            return View("AfterRegister");
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterBindingModel model)
        //{
        //    string url = ConfigurationManager.AppSettings["SmartAPIUrl"];
        //    //HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/api/Account/Register", model);
        //    //masukan nama, no npwp, alamatemail
        //    HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/api/Account/Register/{1}/{2}/{3}"
        //        , url, model.NamaRekanan, model.NomorNPWP, model.Email));
        //    //HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/api/MstRekanan/RegisterCalonRekanan/{1}/{2}/{3}"
        //    //    , url, model.NamaRekanan, model.NomorNPWP, model.Email));
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return View("AfterRegister");
        //    }
        //    return RedirectToAction("Error"); 
        //}

        private async Task<bool> PerformLoginActions(string UserName, string Password)
        {
            var response = await loginClient.Login(UserName, Password);
            if (response.StatusIsSuccessful)
            {
                tokenContainer.ApiToken = response.Data;
            }

            return response.StatusIsSuccessful;
        }

        private async Task<ActionResult> PerformLogout()
        {
            var response = await loginClient.Logout();
            tokenContainer.ApiToken = null;
            tokenContainer.TokenRole = null;
            tokenContainer.RoleName = null;
            return RedirectToAction("Index", "Home");
        }




        private string videoAddress = "~/App_Data/Videos";

        [HttpPost]
        public string MultiUpload(string id, string fileName, string myPrefix)
        {
            var chunkNumber = id;
            var chunks = Request.InputStream;
            string path = Server.MapPath(videoAddress + "/Temp");
            string newpath = Path.Combine(path, myPrefix + "_" + fileName + chunkNumber);
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


        [HttpPost]
        //public string UploadComplete(string fileName, string complete)
        public ActionResult UploadComplete(string fileName, string complete)
        {
            string tempPath = Server.MapPath(videoAddress + "/Temp");
            string videoPath = Server.MapPath(videoAddress);
            string newPath = Path.Combine(tempPath, fileName);
            if (complete == "1")
            {
                string[] filePaths = Directory.GetFiles(tempPath).Where(p => p.Contains(fileName)).OrderBy(p => Int32.Parse(p.Replace(fileName, "$").Split('$')[1])).ToArray();
                foreach (string filePath in filePaths)
                {
                    MergeFiles(newPath, filePath);
                }
            }
            System.IO.File.Move(Path.Combine(tempPath, fileName), Path.Combine(videoPath, fileName));
            //return "success";
            //jika sukses maka load daftar filenya
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