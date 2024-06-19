namespace SchoolAPI.Exceptions.Conflict
{
    public class StudentAlreadyEnrolledException : ConflictException
    {
        public StudentAlreadyEnrolledException(long studentId, long courseId) :
            base($"Student with id {studentId} already enrolled in course with id {courseId}")
        {
        }
    }
}
