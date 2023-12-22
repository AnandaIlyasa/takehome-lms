using Lms.Model;

namespace Lms.IRepo;

internal interface ILearningRepo
{
    List<Learning> GetLearningListByClass(int classId);
}
