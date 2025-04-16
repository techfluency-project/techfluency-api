using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementTestController : ControllerBase
    {
        private readonly PlacementTestService _placementTestService;

        public PlacementTestController(PlacementTestService placementTestService)
        {
            _placementTestService = placementTestService;
        }

        [HttpGet]
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
        public IActionResult ResultFromPlacementTest(List<UserAnswerDTO> userAnswers)
        {
            try
            {
                var result = _placementTestService.GetResultFromPlacementTest(userAnswers);
                return Ok(new {message = "Resultado gerado com sucesso", level = result});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
