namespace Lms.IService;

using Lms.Model;

internal interface IClassService
{
    List<Class> GetEnrolledClassList();
    List<Class> GetClassListByTeacher();
    List<Class> GetUnEnrolledClassList();
    StudentClass EnrollClass(int classId);
}
