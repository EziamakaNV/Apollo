using Apollo.Areas.Identity.Data;
using Apollo.Services;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apollo.Pages
{
    public class SymptomCheckerModel : PageModel
    {
        private readonly GeminiService _geminiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SymptomCheckerModel(GeminiService geminiService, UserManager<ApplicationUser> userManager)
        {
            _geminiService = geminiService;
            _userManager = userManager;
        }

        [BindProperty]
        public string Symptoms { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public string BloodPressure { get; set; }

        [BindProperty]
        public string Temperature { get; set; }

        [BindProperty]
        public string Weight { get; set; }

        [BindProperty]
        public string Height { get; set; }

        public string Diagnosis { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Symptoms))
            {
                ModelState.AddModelError(string.Empty, "Symptoms cannot be empty.");
                return Page();
            }

            // Combine all the information into a single query
            var query = $"Symptoms: {Symptoms}\nBlood Pressure: {BloodPressure}\nTemperature: {Temperature}\nWeight: {Weight}\nHeight: {Height}";

            // Get diagnosis from Gemini API
            var diagnosisAsMarkDown = await _geminiService.GetDiagnosisAsMarkdownAsync(query, Image);

            Diagnosis = Markdown.ToHtml(diagnosisAsMarkDown);


            // Save consultation history
            var userId = _userManager.GetUserId(User);
            var consultationHistoryId = await _geminiService.SaveConsultationHistoryAsync(userId, query,
                Diagnosis, Image);

            ViewData["ConsultationHistoryId"] = consultationHistoryId;

            return Page();
        }
    }
}
