using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAPI.Models.Entites;


namespace SchoolAPI.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(new Student() { Id = 1, FirstName = "Patty", LastName = "Patty",
            Email = "patty@patty.com", BirthDate = new DateTime(2020, 11, 10)});
        }
    }
}
