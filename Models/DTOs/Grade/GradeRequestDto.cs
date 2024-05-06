using SchoolAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs.Grade
{
    public class GradeRequestDto
    {
        [Required]
        [Score]
        public string Score { get; set; } = null!;
    }
}
