using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechFluency.Services;

namespace TechFluency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechFluencyController : ControllerBase
    {
        private readonly TechFluencyService _service;

        public TechFluencyController(TechFluencyService service)
        {
            _service = service;
        }
    }
}
