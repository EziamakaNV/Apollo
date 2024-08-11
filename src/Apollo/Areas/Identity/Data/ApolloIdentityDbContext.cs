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

    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed doctor data
        builder.Entity<Doctor>().HasData(
            new Doctor { Id = 1, Name = "Dr. John Smith", Rating = 4.8, Price = 25 },
            new Doctor { Id = 2, Name = "Dr. Emily Johnson", Rating = 4.7, Price = 30 },
            new Doctor { Id = 3, Name = "Dr. Michael Brown",  Rating = 4.9, Price = 23 },
            new Doctor { Id = 4, Name = "Dr. Linda Davis", Rating = 4.6, Price = 40 },
            new Doctor { Id = 5, Name = "Dr. Robert Wilson", Rating = 4.5, Price = 36 }
        );
    }
}
