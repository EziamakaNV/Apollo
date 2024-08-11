using Apollo.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Pages
{
    [Authorize]
    public class ConsultationHistoryModel : PageModel
    {
        private readonly ApolloIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConsultationHistoryModel(ApolloIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }
        public List<ConsultationHistory> History { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            History = await _dbContext.ConsultationHistories
                .Where(c => c.UserId == userId)
                .Include(c => c.Images)
                .Include(c => c.Doctor)
                .OrderByDescending(c => c.ConsultationDate)
                .ToListAsync();
        }
    }
}
