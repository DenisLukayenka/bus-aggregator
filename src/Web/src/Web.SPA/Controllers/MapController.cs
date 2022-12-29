using Microsoft.AspNetCore.Mvc;

namespace Web.SPA.Controllers
{
    using Web.Infrastructure.Configuration;

    [ApiController]
    [Route("[controller]")]
    public class MapController : ControllerBase
    {
        private readonly AppConfiguration _config;

        public MapController(AppConfiguration config)
        {
            this._config = config;
        }

        [HttpGet("getvalue")]
        public string GetValue2()
        {
            return "stupied value";
        }

        [HttpGet("getMapObject")]
        public IActionResult GetMapObject()
        {
            return Ok(this._config.MapInfo);
        }
    }
}
