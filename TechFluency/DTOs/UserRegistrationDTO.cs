using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Enums;

namespace TechFluency.DTOs
{
    public class UserRegistrationDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public EnumGender? Gender { get; set; }
        public DateOnly? Birthdate { get; set; }

    }
}
