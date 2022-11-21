using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebSPA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
 
        public MapController(IWebHostEnvironment _environment)
        {
            this._environment = _environment;
        }

        [HttpGet("getvalue")]
        public string GetValue2()
        {
            return "stupied value";
        }

        [HttpGet("getMap/{id}")]
        public FileResult GetMap(string id)
        {
            string fileName = Path.ChangeExtension(id.ToLowerInvariant(), "xml");
            string path = Path.Combine(this._environment. WebRootPath, "maps", fileName);
            byte[] buffer = System.IO.File.ReadAllBytes(path);

            return File(buffer, "application/xml", fileName);
        }
    }
}
