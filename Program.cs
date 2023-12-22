using Lms.View;
using Lms.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Lms;

internal class Program
{
    static void Main()
    {
        var host = DIConfig.Init();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var mainView = host.Services.GetService<MainView>();
        mainView.MainMenu();
    }
}
