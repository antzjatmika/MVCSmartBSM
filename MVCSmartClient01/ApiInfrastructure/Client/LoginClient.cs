namespace MVCSmartClient01.ApiInfrastructure.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApiHelper.Client;
    using ApiHelper.Response;
    using ApiModels;
    using Models;
    using Responses;
    using System.Configuration;

    public class LoginClient : ClientBase, ILoginClient
    {
        string SmartAPIUrl = string.Empty;
        private const string RegisterUri = "api/Account/Register";
        private const string RegisterUriRek = "api/MstRekanan/RegisterCalonRekanan";
        //private const string TokenUri = "api/token";
        //private string TokenUri = "http://localhost:2070/oauth/token";
        private string TokenUri = string.Empty;

        public LoginClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<TokenResponse> Login(string UserName, string Password)
        {
            SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            TokenUri = string.Format("{0}/oauth/token", SmartAPIUrl);
            var response = await ApiClient.PostFormEncodedContent(TokenUri, "grant_type".AsPair("password"),
                "username".AsPair(UserName), "password".AsPair(Password));
            var tokenResponse = await CreateJsonResponse<TokenResponse>(response);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await DecodeContent<dynamic>(response);
                tokenResponse.ErrorState = new ErrorStateResponse
                {
                    ModelState = new Dictionary<string, string[]>
                    {
                        {errorContent["error"], new string[] {errorContent["error_description"]}}
                    }
                };
                return tokenResponse;
            }

            var tokenData = await DecodeContent<dynamic>(response);
            tokenResponse.Data = tokenData["access_token"];
            
            return tokenResponse;
        }

        public async Task<RegisterResponse> Register(RegisterBindingModel viewModel)
        {
            var apiModel = new RegisterApiModel
            {
                NamaRekanan = viewModel.NamaRekanan,
                NomorNPWP = viewModel.NomorNPWP,
                ConfirmPassword = viewModel.ConfirmPassword,
                Email = viewModel.Email,
                Password = viewModel.Password,
                IdTypeOfRekanan = viewModel.IdTypeOfRekanan
            };
            string urlModel = ConfigurationManager.AppSettings["SmartAPIUrl"];
            var response = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUri), apiModel);
            var responseRek = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUriRek), apiModel);

            //var response = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUri), viewModel);
            var registerResponse = await CreateJsonResponse<RegisterResponse>(response);
            return registerResponse;
        }
        public async Task<RegisterResponse> RegisterByKiosk(RegisterBindingModel viewModel)
        {
            var apiModel = new RegisterApiModel
            {
                NamaRekanan = viewModel.NamaRekanan,
                NomorNPWP = System.Guid.NewGuid().ToString(),
                Email = viewModel.Email,
                Password = viewModel.Password,
                ConfirmPassword = viewModel.ConfirmPassword,
                IdTypeOfRekanan = viewModel.IdTypeOfRekanan
            };
            string urlModel = ConfigurationManager.AppSettings["SmartAPIUrl"];
            var response = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUri), apiModel);
            var responseRek = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUriRek), apiModel);

            //var response = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUri), viewModel);
            var registerResponse = await CreateJsonResponse<RegisterResponse>(response);
            return registerResponse;
        }
        public async Task<RegisterResponse> RegisterByAdmin(RegisterBindingModel viewModel)
        {
            var apiModel = new RegisterApiModel
            {
                NamaRekanan = viewModel.NamaRekanan,
                NomorNPWP = viewModel.NomorNPWP,
                Email = viewModel.Email,
                Password = viewModel.Password,
                ConfirmPassword = viewModel.ConfirmPassword,
                BarePassword = viewModel.BarePassword,
                IdTypeOfRekanan = viewModel.IdTypeOfRekanan,
                IsActive = viewModel.IsActive
            };
            string urlModel = ConfigurationManager.AppSettings["SmartAPIUrl"];
            var response = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUri), apiModel);
            var responseRek = await ApiClient.PostJsonEncodedContent(string.Format("{0}/{1}", urlModel, RegisterUriRek), apiModel);
            var registerResponse = await CreateJsonResponse<RegisterResponse>(response);

            return registerResponse;
        }



        public async Task<TokenResponse> Logout()
        {
            return null;
        }
    }
}