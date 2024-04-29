using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace SchoolAPI.Models.Entites
{
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [StringLength(255)]
        // Want this to be unique later
        public string Email { get; set; } = null!;

        [Column(TypeName = "Date")]
        public DateTime? BirthDate { get; set; }
    }
}
