namespace MyFinance;

public partial class AppShell : Shell
{
    public AppShell(IServiceProvider serviceProvider)
    {
        this
        .Behaviors(
            new StatusBarBehavior()
            .StatusBarColor(DeepSkyBlue)
        )
        .FlyoutBehavior(FlyoutBehavior.Disabled)
        .Items(
            new TabBar()
            .Items(
                new Tab()
                .Title("Anasayfa")
                .Items(serviceProvider.GetService<MainPage>())
                .Route(nameof(MainPage))
                .FlyoutDisplayOptions(FlyoutDisplayOptions.AsSingleItem)
                .Icon("home.png"),

                new Tab()
                .Title("İstatistik")
                .Items(serviceProvider.GetService<ChartPage>())
                .Route(nameof(AccountPage))
                .FlyoutDisplayOptions(FlyoutDisplayOptions.AsSingleItem)
                .Icon("chart.png"),

                new Tab()
                .Title("İşlemler")
                .Items(serviceProvider.GetService<ItemsPage>())
                .Route(nameof(ItemsPage))
                .FlyoutDisplayOptions(FlyoutDisplayOptions.AsSingleItem)
                .Icon("adjust.png"),

                new Tab()
                .Title("Profil")
                .Items(serviceProvider.GetService<AccountPage>())
                .Route(nameof(AccountPage))
                .FlyoutDisplayOptions(FlyoutDisplayOptions.AsSingleItem)
                .Icon("user.png")
            ),

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
            .ContentTemplate(() => serviceProvider.GetService<RegisterPage>())
            .Route("RegisterPage")

        );
    }
}
