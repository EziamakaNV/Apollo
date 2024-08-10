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

        // Navigation property
        public ApplicationUser User { get; set; }
    }
}
