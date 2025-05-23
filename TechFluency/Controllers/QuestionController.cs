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
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly JwtService _jwtService;
        private readonly LevelAdvancementService _levelAdvancementService;

        public QuestionController(QuestionService questionService, JwtService jwtService,
            LevelAdvancementService levelAdvancementService)
        {
            _questionService = questionService;
            _jwtService = jwtService;
            _levelAdvancementService = levelAdvancementService;
        }

        [HttpGet("GetAllQuestions")]
       public IEnumerable<Question> GetAllQuestions()
       {
           return _questionService.GetAll();
       }

        [HttpGet("GetQuestionById")]
        public IActionResult GetQuestionById(string id)
        {
            try
            {
                var question = _questionService.GetQuestionById(id);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("QuestionAnswer")]
        public async Task<IActionResult> QuestionAnswer(UserAnswerPathDTO answer)
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
            
                var response = _questionService.AnswerQuestion(answer, user.Id);
                var changeStage = await _levelAdvancementService.ChangeToNextStage(user.Id);

                if(changeStage) {
                    response.ChangeToNextStage = true;
                    return Ok(response); 
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
