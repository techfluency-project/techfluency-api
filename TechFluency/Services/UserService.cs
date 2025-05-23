using System.Xml.Linq;
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

        public async Task<User> UserRegistration(UserRegistrationDTO userRequest)
        {
            var userAccount = await _userRepository.GetUserByUsername(userRequest.Username);
            if(userAccount != null)
            {
                throw new Exception($"O username '{userRequest.Username}' já está em uso.");
            }
            userRequest.Password = HashPassword(userRequest.Password);
            var user = new User
            {
                Username = userRequest.Username,
                Password = userRequest.Password,
                Name = userRequest.Name,
                Email = userRequest.Email,
                Phone = userRequest.Phone
            };

            _userRepository.Add(user);

            var userProgress = new UserProgress()
            {
                UserId = user.Id,
            };
            _userProgresRepository.Add(userProgress);
            return await GetUserById(user.Id);
        }


        public async Task<User> GetUserById(string id)
        {
           return await _userRepository.GetUserById(id);
        }

        public UserDTO UpdateMyProfile(User user,UserDTO profileUpdate)
        {

            user.Username = profileUpdate.Username;
            user.Email = profileUpdate.Email;
            user.Name = profileUpdate.Name;
            user.Phone = profileUpdate.Phone;

            _userRepository.Update(user.Id, user);

            return profileUpdate;
        }
    }
}
