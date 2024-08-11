namespace Apollo.Areas.Identity.Data
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }

        public ICollection<ConsultationHistory> Consultations { get; set; }
    }
}
