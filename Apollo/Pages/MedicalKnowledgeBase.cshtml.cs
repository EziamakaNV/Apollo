using Apollo.Areas.Identity.Data;
using Apollo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apollo.Pages
{
    [Authorize]
    public class MedicalKnowledgeBaseModel : PageModel
    {
        private readonly GeminiService _geminiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicalKnowledgeBaseModel(GeminiService geminiService, UserManager<ApplicationUser> userManager)
        {
            _geminiService = geminiService;
            _userManager = userManager;
        }

        [BindProperty]
        public string Query { get; set; }

        public string Response { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Query))
            {
                ModelState.AddModelError(string.Empty, "Query cannot be empty.");
                return Page();
            }

            // Get medical information from Gemini API
            Response = await _geminiService.GetMedicalInformationAsync(Query);

            return Page();
        }
    }
}
