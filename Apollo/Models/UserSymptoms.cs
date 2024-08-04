namespace Apollo.Models
{
    public class UserSymptoms
    {
        public string Id { get; set; }
        public string Symptoms { get; set; }
        public IFormFile Image { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
    }
}
