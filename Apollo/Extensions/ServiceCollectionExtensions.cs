using Apollo.Services;
using Apollo.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Apollo.Areas.Identity.Data;

namespace Apollo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGeminiService(this IServiceCollection services)
        {
            services.AddTransient(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>()!;
                var googleGeminiSettings = configuration.GetSection(nameof(GoogleGeminiSettings))
                 .Get<GoogleGeminiSettings>() ?? throw new MissingFieldException(nameof(GoogleGeminiSettings));

                var geminiService = new GeminiService(googleGeminiSettings.ApiKey);

                return geminiService;
            });
        }

        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                 throw new InvalidOperationException("Connection string 'ApolloIdentityDbContextConnection' not found.");

            services.AddDbContext<ApolloIdentityDbContext>(options => options.UseNpgsql(connectionString));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApolloIdentityDbContext>();

        }
    }
}
