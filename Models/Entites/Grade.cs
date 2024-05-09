using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.Entites
{
    [Index("StudentId", "CourseId", IsUnique = true)]
    [Index("StudentId")]
    [Index("CourseId")]
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(2)]
        public string Score { get; set; } = null!;

        [Required]
        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null!;

        [Required]
        [ForeignKey("CourseId")]
        public Course Course { get; set; } = null!;
    }
}
