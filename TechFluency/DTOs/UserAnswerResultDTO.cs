namespace TechFluency.DTOs
{
    public class UserAnswerResultDTO
    {
        public bool IsCorrect { get; set; }
        public string QuestionID { get; set; }
        public bool StageCompleted { get; set; }
        public int StageAnswered { get; set; }
        public int StageCorrect { get; set; }
        public bool StageFailed { get; set; }
    }
}
