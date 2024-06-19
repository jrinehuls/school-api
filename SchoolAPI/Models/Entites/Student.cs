using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Entites
{
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "Date")]
        public DateTime? BirthDate { get; set; }

        public HashSet<Course> Courses { get; set; } = [];

        public HashSet<Grade> Grades { get; set; } = [];
    }
}
