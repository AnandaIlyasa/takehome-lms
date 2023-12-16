using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class ClassService : IClassService
{
    public IClassRepo ClassRepo { private get; init; }

    public List<Class> GetEnrolledClassList(int studentId)
    {
        var enrolledClassList = ClassRepo.GetClassListByStudent(studentId);
        return enrolledClassList;
    }
}
