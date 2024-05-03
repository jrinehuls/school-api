using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class StudentEntityFinder : IEntityFinder<Student, long>
    {
        public DataContext _dataContext;
        public StudentEntityFinder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Student> FindEntityByIdOrThrow(long id)
        {
            Student? student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student is null)
            {
                Dictionary<string, List<string>> errors = new()
                {
                    { "id", [$"{id}"] }
                };
                throw new NotFoundException(errors, $"Student with id {id} not found");
            }
            return student!;
        }

    }
}
