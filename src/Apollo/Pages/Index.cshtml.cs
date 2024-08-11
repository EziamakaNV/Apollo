using Apollo.Areas.Identity.Data;
using Apollo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apollo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public bool IsUserSignedIn { get; set; }
        public void OnGet()
        {
            IsUserSignedIn = _signInManager.IsSignedIn(User);
        }
    }

}
