namespace SchoolAPI.Exceptions.Conflict
{
    public class GradeConflictException : ConflictException
    {
        public GradeConflictException(long studentId, long courseId) :
            base($"Grade with studentId {studentId} and courseId {courseId} already exists")
        {
        }
    }
}
