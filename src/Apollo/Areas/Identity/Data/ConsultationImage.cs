using System.ComponentModel.DataAnnotations.Schema;

namespace Apollo.Areas.Identity.Data
{
    public class ConsultationImage
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ConsultationHistory))]
        public int ConsultationHistoryId { get; set; }
        public string ImageData { get; set; } // Base64 string

        public ConsultationHistory ConsultationHistory { get; set; }
    }
}
