using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs
{
    public class StudentResponseDto
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }
    }
}
