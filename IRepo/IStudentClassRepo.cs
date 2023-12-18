using Lms.Model;

namespace Lms.IRepo;

internal interface IStudentClassRepo
{
    StudentClass CreateNewStudentClass(StudentClass studentClass);
}
