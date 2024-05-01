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

        // Could add Required attribute and set prop to nullable and still makes column non-nullable
        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [StringLength(255)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "Date")]
        public DateTime? BirthDate { get; set; }
    }
}
