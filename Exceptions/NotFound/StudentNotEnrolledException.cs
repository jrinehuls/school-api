namespace SchoolAPI.Exceptions.NotFound
{
    public class StudentNotEnrolledException : NotFoundException
    {
        public StudentNotEnrolledException(long studentId, long courseId) :
            base($"Student with id {studentId} not found in course with id {courseId}")
        {
        }
    }
}
