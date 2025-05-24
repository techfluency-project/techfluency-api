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
        public async Task<UserDTO> GetMyProfile(String userId)
        {
            var user = await GetUserById(userId);
            var userDTO = new UserDTO
            {
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
            return userDTO;
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

        public async Task<UserDTO> UpdateMyProfile(User user,UserDTO profileUpdate)
        {
            user.Username = profileUpdate.Username ?? user.Username;
            user.Email = profileUpdate.Email ?? user.Email;
            user.Name = profileUpdate.Name ?? user.Name;
            user.Phone = profileUpdate.Phone ?? user.Phone;

            _userRepository.Update(user.Id, user);

            var userUpdate = await GetUserById(user.Id);

            var result = new UserDTO
            {
                Name = userUpdate.Name,
                Email = userUpdate.Email,
                Username = userUpdate.Username,
                Phone = userUpdate.Phone
            };

            return result;
        }
    }
}
