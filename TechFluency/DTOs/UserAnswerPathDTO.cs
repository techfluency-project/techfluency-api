namespace TechFluency.DTOs
{
    public class UserAnswerPathDTO
    {
        public string PathStageId { get; set; }

        public List<UserAnswerDTO> Answers { get; set; }
    }
}
