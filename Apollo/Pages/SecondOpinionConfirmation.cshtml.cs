using Apollo.Areas.Identity.Data;
using Apollo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Apollo.Pages.SecondOpinionBookingModel;

namespace Apollo.Pages
{
    [Authorize]
    public class SecondOpinionConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApolloIdentityDbContext _dbContext;
        private readonly GeminiService _geminiService;

        public SecondOpinionConfirmationModel(UserManager<ApplicationUser> userManager,
            ApolloIdentityDbContext dbContext,
            GeminiService geminiService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _geminiService = geminiService;
        }

        [BindProperty(SupportsGet = true)]
        public int DoctorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ConsultationHistoryId { get; set; }

        public Doctor Doctor { get; private set; }
        public ConsultationHistory Consultation { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Doctor = await _dbContext.Doctors
                .FindAsync(DoctorId);

            Consultation = await _dbContext.ConsultationHistories
                .FindAsync(ConsultationHistoryId);

            if (Doctor == null || Consultation == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var consultation = await _dbContext.ConsultationHistories
                .FindAsync(ConsultationHistoryId);
            if (consultation == null)
            {
                return NotFound();
            }

            var secondOpinion = await _geminiService.SimulateDoctorReplyAsync(consultation);

            consultation.DoctorId = DoctorId;
            consultation.RequestedSecondOpinion = true;
            consultation.SecondOpinion = secondOpinion;

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/ConsultationHistory");
        }
    }
}
