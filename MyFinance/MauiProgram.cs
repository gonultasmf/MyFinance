using Microsoft.Extensions.Logging;
using UraniumUI;

namespace MyFinance;


[MauiMarkup(typeof(StatusBarBehavior), typeof(ShellContent), typeof(TextField), typeof(InputField))]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Logging.AddDebug();

        builder.Services
            .AddSingleton<App>()
            .AddSingleton<AppShell>()
            .AddScopedWithShellRoute<MainPage, MainPageViewModel>($"//{nameof(MainPage)}")
            .AddScopedWithShellRoute<LoginPage, LoginPageViewModel>($"//{nameof(LoginPage)}")
            .AddScoped<StartedPage>();

        return builder.Build();
    }
}
