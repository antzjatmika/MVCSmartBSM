using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MVCSmartAPI01.Models
{
    public class OwinAuthDbContext : IdentityDbContext
    {
        public OwinAuthDbContext()
            : base("DB_SMART_OWIN")
        {
        }
    }

}