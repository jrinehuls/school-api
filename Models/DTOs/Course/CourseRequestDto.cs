using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs.Course
{
    public class CourseRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(10)]
        public string? Code { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
