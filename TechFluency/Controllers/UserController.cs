using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("sign-up")]
        public User UserRegistration(UserRegistrationDTO userRequest)
        {
           return _userService.UserRegistration(userRequest);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequest)
        {
            var result = await _jwtService.Authenticate(loginRequest);
            if (result == null) 
                return Unauthorized();

             return result;
        }

    }
}
