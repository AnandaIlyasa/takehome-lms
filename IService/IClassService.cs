namespace Lms.IService;

using Lms.Model;

internal interface IClassService
{
    List<Class> GetEnrolledClassList(int studentId);
    List<Class> GetUnEnrolledClassList(int studentId);
    StudentClass EnrollClass(StudentClass studentClass);
}
