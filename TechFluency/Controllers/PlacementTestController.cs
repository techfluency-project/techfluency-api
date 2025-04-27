using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class PlacementTestController : ControllerBase
    {
        private readonly PlacementTestService _placementTestService;
        private readonly JwtService _jwtService;

        public PlacementTestController(PlacementTestService placementTestService, JwtService jwtService)
        {
            _placementTestService = placementTestService;
            _jwtService = jwtService;
        }

        [HttpGet("GetQuestionsForPlacementTest")]
        public IEnumerable<Question> GetQuestionsForPlacementTest()
        {
            try
            {
                return _placementTestService.GetQuestionsForPlacementTest();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost("ResultFromPlacementTest")]
        public async Task<IActionResult> ResultFromPlacementTest(List<UserAnswerDTO> userAnswers)
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
                if (user == null)
                {
                    return BadRequest("User has not been found.");
                }
                var result = _placementTestService.GetResultFromPlacementTest(userAnswers, user.Id);
                return Ok(new {message = "Resultado gerado com sucesso", level = result});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
