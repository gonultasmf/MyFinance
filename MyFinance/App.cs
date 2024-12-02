namespace MyFinance;

public partial class App : Application
{
    public static string CurrentUserId = string.Empty;

    private readonly IServiceProvider _service;

    public App(IServiceProvider services)
    {
        _service = services;
        this
        .Resources(AppStyles.Default);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_service.GetService<AppShell>());
    }
}
