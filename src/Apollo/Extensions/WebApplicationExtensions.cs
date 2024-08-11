using Apollo.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApolloIdentityDbContext>();
            context.Database.Migrate();
        }
    }
}
