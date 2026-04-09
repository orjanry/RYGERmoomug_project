using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using moomug_project.Models;

namespace moomug_project.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<GlobalMugCollection> GlobalMugCollections => Set<GlobalMugCollection>();
    public DbSet<MyCollection> MyCollections => Set<MyCollection>();
   
}
