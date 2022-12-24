using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebAPIB64.Models.FilesEntities
{
    public class FuelFilesRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
