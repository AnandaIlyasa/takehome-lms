using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface IClassRepo
{
    List<Class> GetClassList();
    List<Class> GetClassListByStudent(int studentId);
    List<Class> GetClassListByTeacher(int teacherId);
    List<Class> GetUnEnrolledClassListByStudent(int studentId);
    Class CreateNewClass(Class newClass);
}
