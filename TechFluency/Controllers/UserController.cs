using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController: ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public UserController(UserService userService, JwtService jwtService) {
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpGet("my-profile")]
        public async Task<UserDTO> GetMyProfile()
        {
            var user = await _jwtService.GetCurrentUser();
            return await _userService.GetMyProfile(user.Id);
        }

        [HttpPost("sign-up")]
        public async Task<User> UserRegistration(UserRegistrationDTO userRequest)
        {
           return await _userService.UserRegistration(userRequest);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<String>> Login(LoginRequestDTO loginRequest)
        {
            var token = await _jwtService.Authenticate(loginRequest);
            if (token != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddHours(1)
                };

                Response.Cookies.Append("jwt", token, cookieOptions);

                return Ok(new LoginResponseDTO
                {
                    Message = "Token enviado como cookie",
                    AcessToken = token
                });
            }
            return BadRequest();
        }

        [HttpPost("sign-off")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logout realizado com sucesso." });
        }

        [HttpPut("update-profile")]
        public async Task<UserDTO> UpdateMyProfile(UserDTO profileUpdate)
        {
            var user = await _jwtService.GetCurrentUser();
            return  _userService.UpdateMyProfile(user, profileUpdate);
        }
    }
}
