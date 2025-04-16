using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

       [HttpGet]
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
    }
}
