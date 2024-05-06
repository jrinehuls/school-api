namespace SchoolAPI.Exceptions.NotFound
{
    public class GradeNotFoundException : NotFoundException
    {
        public GradeNotFoundException(long studentId, long courseId) : 
            base($"Grade with studentId {studentId} and courseId {courseId} not found")
        {
        }
    }
}
