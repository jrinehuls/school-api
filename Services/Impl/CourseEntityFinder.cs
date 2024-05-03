using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class CourseEntityFinder : IEntityFinder<Course, long>
    {
        public DataContext _dataContext;
        public CourseEntityFinder(DataContext dataContext) 
        { 
            _dataContext = dataContext;
        }

        public async Task<Course> FindEntityByIdOrThrow(long id)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
            {
                ThrowNotFoundById(id);
            }
            return course!;
        }

        private static void ThrowNotFoundById(long id)
        {
            Dictionary<string, List<string>> errors = new()
                {
                    { "id", [$"{id}"] }
                };
            throw new NotFoundException(errors, $"Course with id {id} not found");
        }

    }
}
