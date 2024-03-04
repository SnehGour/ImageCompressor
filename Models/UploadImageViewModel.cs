using System.ComponentModel.DataAnnotations;

namespace ImageCompressor.Models
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
    }
}
