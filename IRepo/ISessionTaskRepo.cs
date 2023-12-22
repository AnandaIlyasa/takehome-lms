namespace Lms.IRepo;

using Lms.Config;
using Lms.Model;

internal interface ISessionTaskRepo
{
    List<LMSTask> GetTaskListBySession(int sessionId);
}
