namespace MVCSmartClient01.ApiInfrastructure.ApiModels
{
    using ApiHelper.Model;

    public class RegisterApiModel : ApiModel
    {
        public string NamaRekanan { get; set; }
        public string NomorNPWP { get; set; }
        public string Email { get; set; }
        public string BarePassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int IdTypeOfRekanan { get; set; }
        public byte IsActive { get; set; }
    }
}