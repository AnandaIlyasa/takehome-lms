using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class StudentClassRepo : IStudentClassRepo
{
    readonly DBContextConfig _context;

    public StudentClassRepo(DBContextConfig context)
    {
        _context = context;
    }

    public StudentClass CreateNewStudentClass(StudentClass studentClass)
    {
        _context.StudentClasses.Add(studentClass);
        _context.SaveChanges();
        return studentClass;
    }
}
