using dotnet_webapi_demo_01_christenzarif.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace dotnet_webapi_demo_01_christenzarif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string UPLOAD = "Uploads";
        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("{id:int}", Name = "GetFileLink")]
        public IActionResult Get2(int id)
        {
            string uploads = Path.Combine(_environment.WebRootPath, UPLOAD);
            if (!Directory.Exists(uploads))
            {
                return NotFound($"'/{UPLOAD}' Directory Not Found");
            }
            string[] filePaths = Directory.GetFiles(uploads, id.ToString() + ".*");
            if (filePaths.Length <= 0)
            {
                return NotFound();
            }

            string filePath = filePaths[0];
            string fileName = Path.GetFileName(filePath);
            string fileUrl = Url.Content($"{Request.Scheme}://{Request.Host}/Uploads/{fileName}");

            return Ok(new { url = fileUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Get3([FromForm][Required][MaxFileSize(500 * 1024 * 1024)][AllowedExtensions(new string[] { ".jpg", ".svg" })] IFormFile file, [FromForm][Required] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var uploads = Path.Combine(_environment.WebRootPath, UPLOAD);
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            //string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExtension = Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploads, id.ToString() + fileExtension);

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }
            return Created($"{Url.Link("GetFileLink", new { id })}", new { id });
        }
    }
}