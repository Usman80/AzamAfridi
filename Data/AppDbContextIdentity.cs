using AzamAfridi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Data
{
    public class AppDbContextIdentity : IdentityDbContext<AppUser>
    {
        public AppDbContextIdentity(DbContextOptions<AppDbContextIdentity> options) : base(options)
        {
        }
    }
}
