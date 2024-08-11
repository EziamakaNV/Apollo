using Apollo.Areas.Identity.Data;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
        public List<ConsultationHistoryViewModel> History { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            
            var consultations = await _dbContext.ConsultationHistories
                .Where(c => c.UserId == userId)
                .Include(c => c.Images)
                .Include(c => c.Doctor)
                .OrderByDescending(c => c.ConsultationDate)
                .ToListAsync();

            History = History = consultations.Select(c => new ConsultationHistoryViewModel
            {
                Id = c.Id,
                Symptoms = c.Symptoms,
                Diagnosis = Markdown.ToHtml(c.Diagnosis),
                SecondOpinion = string.IsNullOrEmpty(c.SecondOpinion) ? null : Markdown.ToHtml(c.SecondOpinion),
                ConsultationDate = c.ConsultationDate,
                RequestedSecondOpinion = c.RequestedSecondOpinion,
                Images = c.Images.Select(i => i.ImageData).ToList(),
                DoctorName = c.Doctor?.Name
            }).ToList();
        }

        public class ConsultationHistoryViewModel
        {
            public int Id { get; set; }
            public string Symptoms { get; set; }
            public string Diagnosis { get; set; }
            public string SecondOpinion { get; set; }
            public DateTimeOffset ConsultationDate { get; set; }
            public bool RequestedSecondOpinion { get; set; }
            public List<string> Images { get; set; }
            public string? DoctorName { get; set; }
        }
    }
}
