using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apollo.Pages
{
    [Authorize]
    public class HealthCheckLocationsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
