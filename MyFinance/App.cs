namespace MyFinance;

public partial class App : MC.Application
{
    public App(IServiceProvider services)
    {
        this
        .Resources(AppStyles.Default)
        .MainPage(services.GetService<AppShell>());
    }
}
