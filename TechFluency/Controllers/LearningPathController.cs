using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LearningPathController : ControllerBase
    {

        private readonly LearningPathService _learningPathService;
        private readonly JwtService _jwtService;


        public LearningPathController(LearningPathService learningPathService, JwtService jwtService)
        {
            _learningPathService = learningPathService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> MountLearningPath()
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
                if(user == null)
                {
                    return BadRequest("User has not been found.");
                }
                _learningPathService.MountingLearningPath(user.Id);
                return Ok(new { message = "Trilha montada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLearningPath()
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
                if (user == null)
                {
                    return BadRequest("User has not been found.");
                }
                var learningPath = _learningPathService.GetLearningPath(user.Id);
                return Ok(learningPath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
