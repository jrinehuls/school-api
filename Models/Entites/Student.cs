using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Entites
{
    public class Student
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(255)]
        // Want this to be unique later
        public string? Email { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
