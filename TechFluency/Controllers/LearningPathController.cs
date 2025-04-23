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

        public LearningPathController(LearningPathService learningPathService)
        {
            _learningPathService = learningPathService;
        }

        [HttpPost]
        public IActionResult MountLearningPath()
        {
            try
            {
                _learningPathService.MountingLearningPath();
                return Ok(new { message = "Trilha montada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetLearningPath()
        {
            try
            {
                var learningPath = _learningPathService.GetLearningPath();
                return Ok(learningPath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
