using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnIdentity.Models
{
    public class AppIdentityDbContext  : IdentityDbContext<AppUser,AppRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
                
        }
    }
}
