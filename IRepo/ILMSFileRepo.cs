using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ILMSFileRepo
{
    LMSFile CreateNewFile(LMSFile file);
}
