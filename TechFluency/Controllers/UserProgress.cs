using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Repository;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgress : ControllerBase
    {
        private readonly QuestionService _questionService;

        public UserProgress(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public string Teste()
        {
            return _questionService.AnswerQuestion();
        }
    }
}
