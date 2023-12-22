using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

namespace Lms.Repo;

internal class ClassRepo : IClassRepo
{
    DBContextConfig _context { get; set; }

    public ClassRepo(DBContextConfig context)
    {
        _context = context;
    }

    List<Class> IClassRepo.GetClassListByStudent(int studentId)
    {
        var classList = _context.Classes
                        .Join(
                            _context.StudentClasses,
                            c => c.Id,
                            sc => sc.ClassId,
                            (c, sc) => new { c, sc.StudentId }
                         )
                        .Where(csc => csc.StudentId == studentId)
                        .Select(csc => csc.c)
                        .Include(c => c.ClassImage)
                        .Include(c => c.Teacher)
                        .ToList();

        return classList;
    }

    List<Class> IClassRepo.GetUnEnrolledClassListByStudent(int studentId)
    {
        var classList = _context.Classes
                        .FromSql($@"
                            SELECT 
	                            *,
                                xmin
                            FROM  
	                            t_m_class c 
                            WHERE 
	                            c.id NOT IN 
	                            ( 
		                            SELECT 
			                            c.id 
		                            FROM 
			                            t_m_class c 
		                            INNER JOIN 
			                            t_r_student_class sc ON c.id = sc.class_id 
		                            WHERE 
			                            sc.student_id = {studentId} 
	                            )
                        ")
                        .ToList();

        return classList;
    }
}
