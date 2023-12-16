namespace Lms.IService;

using Lms.Model;

internal interface IClassService
{
    List<Class> GetEnrolledClassList(int studentId);
}
