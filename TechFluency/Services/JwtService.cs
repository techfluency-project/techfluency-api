using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechFluency.Context;
using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Repository;
using static BCrypt.Net.BCrypt;

namespace TechFluency.Services
{
    public class JwtService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtService(UserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<String?> Authenticate(LoginRequestDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.Username) || string.IsNullOrWhiteSpace(loginDTO.Password)) return null;

            var userAccount = await _userRepository.GetUserByUsername(loginDTO.Username);
            var isCorrectUserPassword = Verify(loginDTO.Password, userAccount.Password);
            if (userAccount is null || !isCorrectUserPassword)
            {
                throw new UnauthorizedAccessException("Senha incorreta.");
            }
                

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, loginDTO.Username),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userAccount.Id.ToString()),
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        SecurityAlgorithms.HmacSha512Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var acessToken = tokenHandler.WriteToken(securityToken);

            return acessToken;
        }


        public Task<User> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            return _userRepository.GetUserById(userId);
        }

    }
}
