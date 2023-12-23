using Lms.IRepo;
using Lms.Repo;
using Lms.IService;
using Lms.Service;
using Lms.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lms.Helper;
using Lms.Model;

namespace Lms.Config;

internal class DIConfig
{
    public static IHost Init()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddDbContext<DBContextConfig>();

        builder.Services.AddSingleton<SessionHelper>();

        builder.Services.AddSingleton<IRoleRepo, RoleRepo>();
        builder.Services.AddSingleton<IUserRepo, UserRepo>();
        builder.Services.AddSingleton<IClassRepo, ClassRepo>();
        builder.Services.AddSingleton<ISessionAttendanceRepo, SessionAttendanceRepo>();
        builder.Services.AddSingleton<ISessionRepo, SessionRepo>();
        builder.Services.AddSingleton<ISessionMaterialRepo, SessionMaterialRepo>();
        builder.Services.AddSingleton<ISessionTaskRepo, SessionTaskRepo>();
        builder.Services.AddSingleton<ISubmissionRepo, SubmissionRepo>();
        builder.Services.AddSingleton<ISubmissionDetailQuestionRepo, SubmissionDetailQuestionRepo>();
        builder.Services.AddSingleton<ISubmissionDetailFileRepo, SubmissionDetailFileRepo>();
        builder.Services.AddSingleton<ILMSFileRepo, LMSFileRepo>();
        builder.Services.AddSingleton<IStudentClassRepo, StudentClassRepo>();
        builder.Services.AddSingleton<IForumRepo, ForumRepo>();
        builder.Services.AddSingleton<IForumCommentRepo, ForumCommentRepo>();
        builder.Services.AddSingleton<ILearningRepo, LearningRepo>();
        builder.Services.AddSingleton<ISessionMaterialFileRepo, SessionMaterialFileRepo>();
        builder.Services.AddSingleton<ITaskQuestionRepo, TaskQuestionRepo>();
        builder.Services.AddSingleton<ITaskMultipleChoiceOptionRepo, TaskMultipleChoiceOptionRepo>();
        builder.Services.AddSingleton<ITaskFileRepo, TaskFileRepo>();

        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IClassService, ClassService>();
        builder.Services.AddSingleton<ISessionService, SessionService>();
        builder.Services.AddSingleton<ITaskSubmissionService, TaskSubmissionService>();
        builder.Services.AddSingleton<IForumService, ForumService>();

        builder.Services.AddSingleton<SuperAdminView>();
        builder.Services.AddSingleton<TeacherView>();
        builder.Services.AddSingleton<StudentView>();
        builder.Services.AddSingleton<MainView>();

        return builder.Build();
    }
}
