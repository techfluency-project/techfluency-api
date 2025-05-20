using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.DTOs;
using TechFluency.Enums;
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

        [HttpGet("GetAllFlashcardsGroup")]
        public async Task<IActionResult> GetAllFlashcardsGroup()
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
                if (user == null)
                {
                    return BadRequest("User has not been found.");
                }
                return Ok(_flashCardGroupService.GetAllFlashcardsGroup(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFlashcardGroupById")]
        public async Task<IActionResult> GetFlashcardGroupById(string id)
        {
            try
            {
                return Ok(_flashCardGroupService.GetFlashcardGroupById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllFlashcardsAvailableToReviewByGroupId")]
        public async Task<IActionResult> GetAllFlashcardsAvailableToReviewByGroupId([FromQuery] string groupId)
        {
            try
            {
                var flashcards = _flashCardService.GetAllFlashcardsAvailableToReviewByGroupId(groupId);
                return Ok(flashcards);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateFlashcardGroup")]
        public async Task<IActionResult> CreateFlashcardGroup(CreateFlashcardDTO flashCardDTO)
        {
            try
            {
                var user = await _jwtService.GetCurrentUser();
                if (user == null)
                {
                    return BadRequest("User has not been found.");
                }
                if (flashCardDTO.Name == null)
                {
                    return BadRequest("Flashcard Group needs a Name");
                }
                var flashCardGroup = _flashCardGroupService.CreateFlashcardGroup(user.Id, flashCardDTO.Name);
                return Ok(new {message = $"Created Group / FlashcardGroup = {flashCardGroup.Id}"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCardToFlashcardGroup")]
        public IActionResult AddCardToFlashcardGroup(FlashcardToCreateDTO flashcardDTO)
        {
            try
            {
                var flashcard = _flashCardGroupService.AddCardToFlashcardGroup(flashcardDTO);
                if(flashcard == null)
                {
                    return BadRequest("It wasn't possible to create flashcard.");
                }
                return Ok(new { message = $"Flashcard created sucessfully: {flashcard.Id} / Added in group: {flashcard.FlashcardGroupId}" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateFlashcardReview")]
        public IActionResult UpdateFlashcardReview(UpdateFlashCardReviewDTO flashReview)
        {
            try
            {
                var flashcard = _flashCardService.GetFlashcardById(flashReview.flashcardId);
                _flashCardService.UpdateFlashcardReview(flashcard, flashReview.level);
                return Ok(new { message = $"Flashcard Feedback Updated / FlashcardId: {flashReview.flashcardId}"});
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
