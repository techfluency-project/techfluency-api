using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Models;
using TechFluency.Repository;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgressController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly ProgressService _userProgressService;
        private readonly JwtService _jwtService;

        public UserProgressController(QuestionService questionService, JwtService jwtService, ProgressService userProgressService)
        {
            _questionService = questionService;
            _jwtService = jwtService;
            _userProgressService = userProgressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProgressByUser()
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
              
                var userProgress = _userProgressService.GetUserProgressByUserId(user.Id);
                return Ok(userProgress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
