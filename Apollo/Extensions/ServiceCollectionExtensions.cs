using Apollo.Services;
using Apollo.Settings;

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
    }
}
