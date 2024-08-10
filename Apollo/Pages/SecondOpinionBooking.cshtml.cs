using Apollo.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;

namespace Apollo.Pages
{
    [Authorize]
    public class SecondOpinionBookingModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SecondOpinionBookingModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<Doctor> Doctors { get; private set; }

        public void OnGet()
        {
            // Mock data for doctors
            Doctors = new List<Doctor>
            {
            new Doctor { Id = 1, Name = "Dr. Alice Johnson", Rating = 4.8, Price = 50 },
            new Doctor { Id = 2, Name = "Dr. Michael Smith", Rating = 4.6, Price = 45 },
            new Doctor { Id = 3, Name = "Dr. Emily Davis", Rating = 4.9, Price = 55 }
            };
        }
        public class Doctor
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Rating { get; set; }
            public double Price { get; set; }
        }

    }
}
