using System.ComponentModel.DataAnnotations.Schema;

namespace Apollo.Areas.Identity.Data
{
    public class ConsultationHistory
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public DateTimeOffset ConsultationDate { get; set; }

        public string? SecondOpinion { get; set; }
        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
        
        public bool RequestedSecondOpinion { get; set; } = false;

        // Navigation properties
        public ApplicationUser User { get; set; }
        public List<ConsultationImage> Images { get; set; } // Related images
        public Doctor? Doctor { get; set; }
    }
}
