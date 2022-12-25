using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web.SPA.Services;

namespace Web.SPA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly MapService _mapService;
 
        public MapController(IWebHostEnvironment _environment, MapService mapService)
        {
            this._environment = _environment;
            this._mapService = mapService;
        }

        [HttpGet("getvalue")]
        public string GetValue2()
        {
            return "stupied value";
        }

        [HttpGet("map")]
        public FileResult GetMap()
        {
            // string path = Path.Combine(this._environment. WebRootPath, "maps", fileName);
            // byte[] buffer = System.IO.File.ReadAllBytes(path);

            // return File(buffer, "application/xml", fileName);
            return null;
        }

        [HttpGet("getMapObject/{id}")]
        public IActionResult GetMapObject(string id)
        {
            return Ok(this._mapService.GetOrAdd(id));
        }
    }
}
