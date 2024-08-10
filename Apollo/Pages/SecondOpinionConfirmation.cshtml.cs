using Apollo.Areas.Identity.Data;
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

        public SecondOpinionConfirmationModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; private set; }

        public void OnGet()
        {
            // Mock data for the selected doctor
            Doctor = new Doctor { Id = DoctorId, Name = "Dr. Alice Johnson", Rating = 4.8, Price = 50 };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);

            // Mock payment and second opinion booking process
            await Task.Delay(1000); // Simulate processing time

            // Redirect to the consultation history page after booking
            return RedirectToPage("/ConsultationHistory");
        }
    }
}
