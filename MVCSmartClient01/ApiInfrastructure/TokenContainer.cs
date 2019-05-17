namespace MVCSmartClient01.ApiInfrastructure
{
    using System.Web;
    using ApiHelper;

    public class TokenContainer : ITokenContainer
    {
        private const string ApiTokenKey = "ApiToken";
        private const string TokenRoleKey = "TokenRole";
        private const string RoleNameKey = "RoleName";
        private const string UserIdKey = "UserId";
        private const string SupervisorIdKey = "SupervisorId";
        private const string IdRekananContactKey = "IdRekananContact";
        private const string IdNotarisKey = "IdNotaris";
        private const string IdOrganisasiKey = "IdOrganisasi";
        private const string UserNameKey = "UserName";
        private const string UserEmailKey = "UserEmail";
        private const string IdTypeOfRekananKey = "IdTypeOfRekanan";
        private const string XLSPointerKey = "XLSPointer";
        private const string KeteranganKey = "Keterangan";
        public object ApiToken
        {
            get { return Current.Session != null ? Current.Session[ApiTokenKey] : null; }
            set { if (Current.Session != null) Current.Session[ApiTokenKey] = value; }
        }

        public object TokenRole
        {
            get { return Current.Session != null ? Current.Session[TokenRoleKey] : null; }
            set { if (Current.Session != null) Current.Session[TokenRoleKey] = value; }
        }

        public object RoleName
        {
            get { return Current.Session != null ? Current.Session[RoleNameKey] : null; }
            set { if (Current.Session != null) Current.Session[RoleNameKey] = value; }
        }

        public object UserId
        {
            get { return Current.Session != null ? Current.Session[UserIdKey] : null; }
            set { if (Current.Session != null) Current.Session[UserIdKey] = value; }
        }
        public object SupervisorId
        {
            get { return Current.Session != null ? Current.Session[SupervisorIdKey] : null; }
            set { if (Current.Session != null) Current.Session[SupervisorIdKey] = value; }
        }
        public object IdRekananContact
        {
            get { return Current.Session != null ? Current.Session[IdRekananContactKey] : null; }
            set { if (Current.Session != null) Current.Session[IdRekananContactKey] = value; }
        }
        public object IdNotaris
        {
            get { return Current.Session != null ? Current.Session[IdNotarisKey] : null; }
            set { if (Current.Session != null) Current.Session[IdNotarisKey] = value; }
        }
        public object IdOrganisasi
        {
            get { return Current.Session != null ? Current.Session[IdOrganisasiKey] : null; }
            set { if (Current.Session != null) Current.Session[IdOrganisasiKey] = value; }
        }
        public object UserName
        {
            get { return Current.Session != null ? Current.Session[UserNameKey] : null; }
            set { if (Current.Session != null) Current.Session[UserNameKey] = value; }
        }
        public object UserEmail
        {
            get { return Current.Session != null ? Current.Session[UserEmailKey] : null; }
            set { if (Current.Session != null) Current.Session[UserEmailKey] = value; }
        }
        public object IdTypeOfRekanan
        {
            get { return Current.Session != null ? Current.Session[IdTypeOfRekananKey] : null; }
            set { if (Current.Session != null) Current.Session[IdTypeOfRekananKey] = value; }
        }
        public object XLSPointer
        {
            get { return Current.Session != null ? Current.Session[XLSPointerKey] : null; }
            set { if (Current.Session != null) Current.Session[XLSPointerKey] = value; }
        }
        public object Keterangan
        {
            get { return Current.Session != null ? Current.Session[KeteranganKey] : null; }
            set { if (Current.Session != null) Current.Session[KeteranganKey] = value; }
        }
        private static HttpContextBase Current
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}