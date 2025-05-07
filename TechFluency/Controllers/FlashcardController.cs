using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.DTOs;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardController : ControllerBase
    {
        private readonly FlashcardService _flashCardService;
        private readonly FlashcardGroupService _flashCardGroupService;
        private readonly JwtService _jwtService;

        public FlashcardController(FlashcardService flashcardService, FlashcardGroupService flashCardGroupService, JwtService jwtService)
        {
            _flashCardService = flashcardService;
            _flashCardGroupService = flashCardGroupService;
            _jwtService = jwtService;
        }

        [HttpGet("GetFlashcardsGroup")]
        public async Task<IActionResult> GetFlashcardsGroup()
        {
            var user = await _jwtService.GetCurrentUser();
            if (user == null)
            {
                return BadRequest("User has not been found.");
            }
            return Ok(_flashCardGroupService.GetFlashcardsGroup(user.Id));
        }

        [HttpPost("CreateFlashcardGroup")]
        public async Task<IActionResult> CreateFlashcardGroup(FlashcardGroupName flashcardGroupName)
        {
            var user = await _jwtService.GetCurrentUser();
            if (user == null)
            {
                return BadRequest("User has not been found.");
            }
            if(flashcardGroupName == null)
            {
                return BadRequest("Flashcard Group needs a Name");
            }
            _flashCardGroupService.CreateFlashcardGroup(user.Id, flashcardGroupName);
            return Ok();
        }
    }
}
