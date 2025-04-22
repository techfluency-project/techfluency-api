namespace TechFluency.DTOs
{
    public class LoginResponseDTO
    {
        public string? UserName { get; set; }
        public string? AcessToken { get; set; }
        public int ExpiresIn { get;set; }
    }
}
