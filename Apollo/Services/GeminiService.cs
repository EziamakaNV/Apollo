using GenerativeAI.Models;

namespace Apollo.Services
{
    public class GeminiService
    {
        private GenerativeModel _model;
        public GeminiService(string apiKey)
        {
            _model = new GenerativeModel(apiKey ?? throw new MissingFieldException(nameof(apiKey)));
        }

        public async Task<string?> GetDiagnosisAsync(string symptoms, IFormFile image)
        {
            string? response = await _model.GenerateContentAsync(symptoms);

            return response;
        }
    }
}
