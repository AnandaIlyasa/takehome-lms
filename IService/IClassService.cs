namespace Lms.IService;

using Lms.Model;

internal interface IClassService
{
    List<Class> GetAllClassList();
    List<Class> GetEnrolledClassList();
    List<Class> GetClassListByTeacher();
    List<Class> GetUnEnrolledClassList();
    StudentClass EnrollClass(int classId);
    Class CreateNewClass(Class newClass);
}
