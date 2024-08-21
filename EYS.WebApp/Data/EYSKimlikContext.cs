using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EYS.WebApp.Data
{
    public class EYSKimlikContext(DbContextOptions<EYSKimlikContext> options) : IdentityDbContext<IdentityUser>(options)
    {
    }
}
