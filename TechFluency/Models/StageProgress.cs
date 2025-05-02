namespace TechFluency.Models
{
    public class StageProgress
    {
        public string StageId { get; set; }
        public int TotalAnswered { get; set; }
        public int TotalCorrect { get; set; }
        public bool HasFailed { get; set; }

    }

}
