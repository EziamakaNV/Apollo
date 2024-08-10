﻿using Apollo.Areas.Identity.Data;
using GenerativeAI.Models;
using GenerativeAI.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Apollo.Services
{
    public class GeminiService
    {
        private GenerativeModel _model;
        private Gemini15Flash _visionModel;
        private ApolloIdentityDbContext _dbContext;

        public GeminiService(string apiKey, ApolloIdentityDbContext dbContext)
        {
            _model = new GenerativeModel(apiKey ?? throw new MissingFieldException(nameof(apiKey)));
            _visionModel = new Gemini15Flash(apiKey);
            _dbContext = dbContext;
        }

        public async Task<string?> GetDiagnosisAsync(string symptoms, IFormFile image)
        {
            string? response = string.Empty;

            if (image is not null)
            {
                var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp" };

                if (!allowedMimeTypes.Contains(image.ContentType))
                {
                    throw new InvalidOperationException("Invalid file type. Only images are allowed.");
                }

                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                var imagePart = new Part()
                {
                    InlineData = new GenerativeContentBlob()
                    {
                        MimeType = image.ContentType,
                        Data = Convert.ToBase64String(memoryStream.ToArray())
                    }
                };

                var textPart = new Part()
                {
                    Text = GenerateSymptomCheckerPromptWithImage(symptoms)
                };

                var parts = new[] { textPart, imagePart };

                var result = await _visionModel.GenerateContentAsync(parts);

                response = result.Text();
            }
            else
            {
                string prompt = GenerateSymptomCheckerPromptWithoutImage(symptoms);
                response = await _model.GenerateContentAsync(prompt);
            }

            

            return response;
        }

        public async Task SaveConsultationHistoryAsync(string userId, string symptoms,
            string diagnosis, IFormFile image)
        {
            var consultation = new ConsultationHistory
            {
                UserId = userId,
                Symptoms = symptoms,
                Diagnosis = diagnosis,
                ConsultationDate = DateTime.UtcNow,
                RequestedSecondOpinion = false
            };

            if (image is not null)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                var consultationImage = new ConsultationImage
                {
                    ConsultationHistory = consultation,
                    ImageData = Convert.ToBase64String(memoryStream.ToArray())
                };

                consultation.Images = new List<ConsultationImage>
                {
                    consultationImage
                };

            }

            _dbContext.ConsultationHistories.Add(consultation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetMedicalInformationAsync(string query)
        {
            string prompt = GenerateMedicalKnowledgePrompt(query);
            string? response = await _model.GenerateContentAsync(prompt);

            return response;
        }

        private string GenerateSymptomCheckerPromptWithoutImage(string message) => $"""
            You are well-versed in medical symptoms. Provide possible diagnoses, remedies, and advice on whether an ER visit is necessary.
            Here are the user's symptoms: {message}
            """;

        private string GenerateSymptomCheckerPromptWithImage(string message) => $"""
            You are well-versed in medical symptoms. Using all data provided by the user,
            proffer possible diagnoses, remedies, and advice on whether an ER visit is necessary.
            The user has provided an image.
            Here are the user's symptoms: {message}
            """;

        private string GenerateMedicalKnowledgePrompt(string query) => $"""
            You are an expert in medical knowledge. Provide detailed information on the following medical condition or topic: {query}
            """;
    }
}
