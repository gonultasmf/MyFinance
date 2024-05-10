using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;
using DXImage = DevExpress.Maui.Core.DXImage;

namespace MyFinance;


[MauiMarkup(typeof(StatusBarBehavior), typeof(TextEdit), typeof(TextEditBase), typeof(EditBase), typeof(ComboBoxEdit))]
[MauiMarkup(typeof(PasswordEdit), typeof(CheckEdit), typeof(DXPopup), typeof(ComboBoxEditBase), typeof(ItemsEditBase))]
[MauiMarkup(typeof(DXImage), typeof(DXButton), typeof(DXViewBase), typeof(DXBorder), typeof(DXContentPresenterBase))]
[MauiMarkup(typeof(DXContentPresenter), typeof(DXCollectionView), typeof(CartesianChart))]

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDevExpress()
            .UseMauiCommunityToolkit()
            .UseSkiaSharp(true)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCompatibility();
        
        builder.Logging.AddDebug();

        builder.Services
            .AddSingleton<App>()
            .AddSingleton<AppShell>()
            .AddDbContext<MyFinanceContext>()
            .AddScopedWithShellRoute<MainPage, MainPageViewModel>($"//{nameof(MainPage)}")
            .AddScopedWithShellRoute<ChartPage, ChartPageViewModel>($"//{nameof(ChartPage)}")
            .AddScopedWithShellRoute<ItemsPage, ItemsPageViewModel>($"//{nameof(ItemsPage)}")
            .AddScopedWithShellRoute<AccountPage, AccountPageViewModel>($"//{nameof(AccountPage)}")
            .AddScopedWithShellRoute<LoginPage, LoginPageViewModel>($"//{nameof(LoginPage)}")
            .AddScopedWithShellRoute<RegisterPage, RegisterPageViewModel>($"//{nameof(RegisterPage)}")
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
