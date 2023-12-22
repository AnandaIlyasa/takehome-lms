using Lms.Config;
using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class ClassService : IClassService
{
    readonly IClassRepo _classRepo;
    readonly IStudentClassRepo _studentClassRepo;
    readonly ILearningRepo _learningRepo;
    readonly ISessionRepo _sessionRepo;

    public ClassService
    (
        IClassRepo classRepo,
        IStudentClassRepo studentClassRepo,
        ILearningRepo learningRepo,
        ISessionRepo sessionRepo
    )
    {
        _classRepo = classRepo;
        _studentClassRepo = studentClassRepo;
        _learningRepo = learningRepo;
        _sessionRepo = sessionRepo;
    }

    public List<Class> GetEnrolledClassList(int studentId)
    {
        var enrolledClassList = _classRepo.GetClassListByStudent(studentId);
        foreach (var enrolledClass in enrolledClassList)
        {
            var learningList = _learningRepo.GetLearningListByClass(enrolledClass.Id);
            enrolledClass.LearningList = learningList;
            foreach (var learning in learningList)
            {
                var sessionList = _sessionRepo.GetSessionListByLearning(learning.Id);
                learning.SessionList = sessionList;
            }
        }
        return enrolledClassList;
    }

    public List<Class> GetUnEnrolledClassList(int studentId)
    {
        var unEnrolledClassList = _classRepo.GetUnEnrolledClassListByStudent(studentId);
        return unEnrolledClassList;
    }

    public StudentClass EnrollClass(StudentClass studentClass)
    {
        studentClass = _studentClassRepo.CreateNewStudentClass(studentClass);
        return studentClass;
    }
}
