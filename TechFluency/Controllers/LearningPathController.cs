using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathController : ControllerBase
    {

        private readonly LearningPathService _learningPathService;

        public LearningPathController(LearningPathService learningPathService)
        {
            _learningPathService = learningPathService;
        }

        [HttpPost]
        public IActionResult MountLearningPath(User user)
        {
            try
            {
                _learningPathService.MountingLearningPath(user);
                return Ok(new { message = "Trilha montada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
