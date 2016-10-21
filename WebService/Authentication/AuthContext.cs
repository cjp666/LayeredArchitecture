using Microsoft.AspNet.Identity.EntityFramework;

namespace WebService.Authentication
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
        }
    }
}
