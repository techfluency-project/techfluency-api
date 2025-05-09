using TechFluency.Enums;

namespace TechFluency.DTOs
{
    public class UpdateFlashCardReviewDTO
    {
        public string flashcardId { get; set; }
        public EnumDifficulty level { get; set; }
    }
}
