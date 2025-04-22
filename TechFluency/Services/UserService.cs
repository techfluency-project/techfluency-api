using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Repository;
using static BCrypt.Net.BCrypt;

namespace TechFluency.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserProgresRepository _userProgresRepository;
       
        public UserService(UserRepository userRepository, UserProgresRepository userProgresRepository) {
            _userRepository = userRepository;
            _userProgresRepository = userProgresRepository;
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
            
            var userProgress = new UserProgress()
            {
                UserId = user.Id,
            };

            _userRepository.Add(user);
            _userProgresRepository.Add(userProgress);
            
            return _userRepository.Get(user.Id);
        } 
    }
}
