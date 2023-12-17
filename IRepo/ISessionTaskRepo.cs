namespace Lms.IRepo;

using Lms.Model;

internal interface ISessionTaskRepo
{
    List<LMSTask> GetSessionTaskListBySession(int sessionId);
}
