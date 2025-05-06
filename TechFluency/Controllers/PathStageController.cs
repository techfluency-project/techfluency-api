using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Models;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathStageController : ControllerBase
    {
        private readonly PathStageService _pathStageService;

        public PathStageController(PathStageService pathStageService)
        {
            _pathStageService = pathStageService;
        }

        [HttpGet("GetPathStageById")]
        public PathStage GetPathStageById(string id)
        {
            return _pathStageService.GetPathStageById(id);
        }
    }
}
