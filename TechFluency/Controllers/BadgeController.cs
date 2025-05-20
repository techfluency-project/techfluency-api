using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly BadgeService _badgeService;

        public BadgeController(BadgeService badgeService)
        {
             _badgeService = badgeService;
        }

        [HttpGet("GetBadgeById")]
        public ActionResult<Badge> GetBadgeById(string id) 
        { 
            try
            {
                var badge = _badgeService.GetBadgeById(id);
                if(badge == null)
                {
                    return NotFound();
                }
                return Ok(badge);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
