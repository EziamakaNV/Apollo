using Apollo.Areas.Identity.Data;
using GenerativeAI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Apollo.Services
{
    public class GeminiService
    {
        private GenerativeModel _model;
        private ApolloIdentityDbContext _dbContext;

        public GeminiService(string apiKey, ApolloIdentityDbContext dbContext)
        {
            _model = new GenerativeModel(apiKey ?? throw new MissingFieldException(nameof(apiKey)));
            _dbContext = dbContext;
        }

        public async Task<string?> GetDiagnosisAsync(string symptoms, IFormFile image)
        {
            string prompt = GenerateSymptomCheckerPrompt(symptoms);
            string? response = await _model.GenerateContentAsync(prompt);

            return response;
        }

        public async Task SaveConsultationHistoryAsync(string userId, string symptoms, string diagnosis)
        {
            var consultation = new ConsultationHistory
            {
                UserId = userId,
                Symptoms = symptoms,
                Diagnosis = diagnosis,
                ConsultationDate = DateTime.UtcNow
            };

            _dbContext.ConsultationHistories.Add(consultation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetMedicalInformationAsync(string query)
        {
            string prompt = GenerateMedicalKnowledgePrompt(query);
            string? response = await _model.GenerateContentAsync(prompt);

            return response;
        }

        private string GenerateSymptomCheckerPrompt(string message) => $"""
            You are well-versed in medical symptoms. Provide possible diagnoses, remedies, and advice on whether an ER visit is necessary.
            Here are the user's symptoms: {message}
            """;

        private string GenerateMedicalKnowledgePrompt(string query) => $"""
            You are an expert in medical knowledge. Provide detailed information on the following medical condition or topic: {query}
            """;
    }
}
