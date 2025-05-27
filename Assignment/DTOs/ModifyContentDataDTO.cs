using System.ComponentModel.DataAnnotations;

namespace Assignment.DTOs
{
    public class ModifyContentDataDTO
    {
        [Required(ErrorMessage = "Content Title is required.")]
        [MinLength(5, ErrorMessage = "Content Title should be more than 5 characters.")]
        [MaxLength(150, ErrorMessage = "Content Title should be less than 150 characters.")]
        public string ContentTitle { get; set; }
        [Required(ErrorMessage = "Content Description is required.")]
        [MinLength(5, ErrorMessage = "Content Description should be more than 5 characters.")]
        [MaxLength(2000, ErrorMessage = "Content Description should be less than 2000 characters.")]
        public string ContentDescription { get; set; }
        [Required(ErrorMessage = "You should select an image for the content.")]
        public IFormFile ImagePath { get; set; }
    }
}
