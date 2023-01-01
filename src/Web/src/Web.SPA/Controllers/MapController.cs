using Microsoft.AspNetCore.Mvc;

namespace Web.SPA.Controllers
{
    using Web.Infrastructure.Configuration;

    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly AppConfiguration _config;

        public MapController(AppConfiguration config)
        {
            this._config = config;
        }

        [HttpGet("metadata")]
        public IActionResult GetMapMetadata()
        {
            return Ok(this._config.MapInfo);
        }
    }
}
