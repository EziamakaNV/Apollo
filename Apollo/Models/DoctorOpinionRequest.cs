namespace Apollo.Models
{
    public class DoctorOpinionRequest
    {
        public string Id { get; set; }
        public string UserSymptomsId { get; set; }
        public string DoctorId { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
