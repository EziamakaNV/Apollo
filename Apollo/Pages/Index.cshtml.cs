using Apollo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apollo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GeminiService _geminiService;

        public IndexModel(GeminiService geminiService)
        {
            _geminiService = geminiService;
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
            }

            return Page();
        }
    }

}
