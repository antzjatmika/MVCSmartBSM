namespace MVCSmartClient01.ApiInfrastructure.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface ILoginClient
    {
        Task<TokenResponse> Login(string email, string password);
        Task<RegisterResponse> Register(RegisterBindingModel viewModel);
        Task<RegisterResponse> RegisterByKiosk(RegisterBindingModel viewModel);
        Task<TokenResponse> Logout();
    }
}