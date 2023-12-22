using Lms.Model;

namespace Lms.IRepo;

internal interface IForumRepo
{
    Forum GetForumBySession(int sessionId);
}
