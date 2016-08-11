using Microsoft.AspNet.Identity.EntityFramework;

namespace WindowsService.Authentication
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
        }
    }
}
