using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class ClassService : IClassService
{
    public IClassRepo ClassRepo { private get; init; }
    public IStudentClassRepo StudentClassRepo { private get; init; }

    public List<Class> GetEnrolledClassList(int studentId)
    {
        var enrolledClassList = ClassRepo.GetClassListByStudent(studentId);
        return enrolledClassList;
    }

    public List<Class> GetUnEnrolledClassList(int studentId)
    {
        var unEnrolledClassList = ClassRepo.GetUnEnrolledClassListByStudent(studentId);
        return unEnrolledClassList;
    }

    public StudentClass EnrollClass(StudentClass studentClass)
    {
        studentClass = StudentClassRepo.CreateNewStudentClass(studentClass);
        return studentClass;
    }
}
