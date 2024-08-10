using Apollo.Areas.Identity.Data;
using Apollo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apollo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GeminiService _geminiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(GeminiService geminiService, UserManager<ApplicationUser> userManager)
        {
            _geminiService = geminiService;
            _userManager = userManager;
        }

        [BindProperty]
        public string Symptoms { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public string Diagnosis { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Diagnosis = await _geminiService.GetDiagnosisAsync(Symptoms, Image);

                // Save consultation history
                var userId = _userManager.GetUserId(User);
                await _geminiService.SaveConsultationHistoryAsync(userId, Symptoms, Diagnosis);
            }

            return Page();
        }
    }

}
