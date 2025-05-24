using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Services;
using static BCrypt.Net.BCrypt;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public UserController(UserService userService, JwtService jwtService) {
            _userService = userService;
            _jwtService = jwtService;
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

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
                if (user == null)
                {
                    return BadRequest("User has not been found.");
                }

                if (Verify(resetPasswordDTO.CurrentPassword, user.Password))
                {
                    var success = await _userService.ResetPassword(user.Id, resetPasswordDTO);
                    if(success)
                        return Ok("Changes made successfully!");

                    return BadRequest("Passwords do not match. Error.");
                }

                return BadRequest("Changes not made. Error.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
