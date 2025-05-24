using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Enums;

namespace TechFluency.DTOs
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
