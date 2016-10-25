using Chinook.Framework.Entities.Security; // for Application user 
using Microsoft.AspNet.Identity.EntityFramework; // for IdentityDbContext

namespace Chinook.Framework.DAL.Security
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //CTRl + .
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
