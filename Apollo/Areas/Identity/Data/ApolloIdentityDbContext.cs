using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Areas.Identity.Data;

public class ApolloIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ApolloIdentityDbContext(DbContextOptions<ApolloIdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<ConsultationHistory> ConsultationHistories { get; set; }
    public DbSet<ConsultationImage> ConsultationImages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
