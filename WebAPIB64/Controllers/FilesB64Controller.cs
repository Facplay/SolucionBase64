using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using WebAPIB64.Models.FilesEntities;


namespace WebAPIB64.Controllers
{
    [ApiController]
    [Route("api")]
    public class FilesB64Controller : ControllerBase
    {
        private readonly ILogger<FilesB64Controller> _logger;

        public FilesB64Controller(ILogger<FilesB64Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("GetArchivo")]
        public IActionResult Get([FromForm] string base64)
        {
            try
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(base64);
                Stream stream = new MemoryStream(base64EncodedBytes);

                return File(stream, "application/octet-stream");
            }
            catch (System.Exception)
            {               
                throw;
            }

        }

        [HttpPost]
        [Route("CrearBase64")]
        public async Task<ActionResult<string>> Post([FromForm] FuelFilesRequest archivo)
        {
            try
            {
                string base64String = string.Empty;
                using (MemoryStream fileStream = new MemoryStream())
                {
                    await archivo.File.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.Flush();
        
                    base64String = Convert.ToBase64String(fileStream.ToArray());                
                }

                return new ObjectResult(base64String); 
            }
            catch (System.Exception)
            {              
                throw;
            }
        }
    }
}