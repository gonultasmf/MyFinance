namespace MyFinance;

public partial class AppShell : Shell
{
    public AppShell(IServiceProvider serviceProvider)
    {
        this
        .FlyoutBehavior(FlyoutBehavior.Disabled)
        .Items(
            new ShellContent()
            .Title("")
            .ContentTemplate(() => new StartedPage())
            .Route("StartedPage"),

            new ShellContent()
            .Title("")
            .ContentTemplate(() => serviceProvider.GetService<LoginPage>())
            .Route("LoginPage"),

            new ShellContent()
            .Title("")
            .ContentTemplate(() => serviceProvider.GetService<MainPage>())
            .Route("MainPage")
        );
    }
}
