using Lms.Config;
using Lms.Helper;
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
    readonly SessionHelper _sessionHelper;

    public ClassService
    (
        IClassRepo classRepo,
        IStudentClassRepo studentClassRepo,
        ILearningRepo learningRepo,
        ISessionRepo sessionRepo,
        SessionHelper sessionHelper
    )
    {
        _classRepo = classRepo;
        _studentClassRepo = studentClassRepo;
        _learningRepo = learningRepo;
        _sessionRepo = sessionRepo;
        _sessionHelper = sessionHelper;
    }

    public List<Class> GetEnrolledClassList()
    {
        var enrolledClassList = _classRepo.GetClassListByStudent(_sessionHelper.UserId);
        GetClassContents(enrolledClassList);
        return enrolledClassList;
    }

    public List<Class> GetUnEnrolledClassList()
    {
        var unEnrolledClassList = _classRepo.GetUnEnrolledClassListByStudent(_sessionHelper.UserId);
        return unEnrolledClassList;
    }

    public StudentClass EnrollClass(int classId)
    {
        var studentId = _sessionHelper.UserId;
        var studentClass = new StudentClass()
        {
            StudentId = studentId,
            ClassId = classId,
        };
        studentClass = _studentClassRepo.CreateNewStudentClass(studentClass);
        return studentClass;
    }

    public List<Class> GetClassListByTeacher()
    {
        var classList = _classRepo.GetClassListByTeacher(_sessionHelper.UserId);
        GetClassContents(classList);
        return classList;
    }

    void GetClassContents(List<Class> classList)
    {
        foreach (var cls in classList)
        {
            var learningList = _learningRepo.GetLearningListByClass(cls.Id);
            cls.LearningList = learningList;
            foreach (var learning in learningList)
            {
                var sessionList = _sessionRepo.GetSessionListByLearning(learning.Id);
                learning.SessionList = sessionList;
            }
        }
    }
}
