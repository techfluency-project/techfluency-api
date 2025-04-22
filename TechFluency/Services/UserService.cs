using Microsoft.AspNetCore.Identity;
using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Repository;
using static BCrypt.Net.BCrypt;

namespace TechFluency.Services
{
    public class UserService
    {
        public UserRepository _userRepository;

       
        public UserService(UserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public User UserRegistration(UserRegistrationDTO userRequest)
        {
            userRequest.Password = HashPassword(userRequest.Password);
            var user = new User
            {
                Username = userRequest.Username,
                Password = userRequest.Password,
                Name = userRequest.Name,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                Gender = userRequest.Gender,
                Birthdate = userRequest.Birthdate,
                Phone = userRequest.Phone
            };

            _userRepository.Add(user);
            
            return _userRepository.Get(user.Id);
        } 
    }
}
