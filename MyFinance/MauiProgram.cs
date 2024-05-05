using Microsoft.Extensions.Logging;

namespace MyFinance;


[MauiMarkup(typeof(StatusBarBehavior), typeof(ShellContent), typeof(TextEdit), typeof(TextEditBase), typeof(EditBase))]
[MauiMarkup(typeof(PasswordEdit), typeof(CheckEdit), typeof(DXPopup))]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDevExpress()
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
            .AddDbContext<MyFinanceContext>()
            .AddScopedWithShellRoute<MainPage, MainPageViewModel>($"//{nameof(MainPage)}")
            .AddScopedWithShellRoute<LoginPage, LoginPageViewModel>($"//{nameof(LoginPage)}")
            .AddScoped<StartedPage>()
            .AddScoped<IUserRepo, UserRepo>();

        #region Init DB
        var dbContext = new MyFinanceContext();
        dbContext.Database.EnsureCreated();
        if (dbContext.Users.Count() <= 0)
        {
            dbContext.Users.Add(new()
            {
                Age = 23,
                Email = "mg",
                Password = "00",
                FirstName = "Mustafa",
                LastName = "Gönültaş",
                Gender = Gender.Male,
                IsActive = true,
                PhoneNumber = "1234567890"
            });
            dbContext.SaveChanges();
        }
        dbContext.Dispose();
        #endregion

        return builder.Build();
    }
}
