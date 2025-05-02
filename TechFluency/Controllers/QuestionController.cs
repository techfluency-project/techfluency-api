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

        public QuestionController(QuestionService questionService, JwtService jwtService)
        {
            _questionService = questionService;
            _jwtService = jwtService;
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
                if (user == null)
                {
                    return BadRequest("User has not been found.");
                }
                var response = _questionService.AnswerQuestion(answer, user.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
