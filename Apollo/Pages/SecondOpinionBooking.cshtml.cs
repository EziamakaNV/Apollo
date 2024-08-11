using Apollo.Areas.Identity.Data;
using Apollo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Apollo.Pages
{
    [Authorize]
    public class SecondOpinionBookingModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApolloIdentityDbContext _dbContext;
        private readonly GeminiService _geminiService;

        public SecondOpinionBookingModel(UserManager<ApplicationUser> userManager, ApolloIdentityDbContext dbContext,
            GeminiService geminiService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _geminiService = geminiService;
        }

        public List<Doctor> Doctors { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int ConsultationHistoryId { get; set; }

        public async Task<IActionResult> OnGetAsync(int consultationHistoryId)
        {
            ConsultationHistoryId = consultationHistoryId;

            Doctors = await _dbContext.Doctors
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int doctorId)
        {
            return RedirectToPage("/SecondOpinionConfirmation", new { consultationHistoryId = ConsultationHistoryId, doctorId });
        }

    }
}
