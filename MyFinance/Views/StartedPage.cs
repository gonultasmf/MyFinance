namespace MyFinance.Views;

public partial class StartedPage() : BasePage("Get Started")
{
    public override void Build()
    {
        this
        .ShellNavBarIsVisible(false)
        .Behaviors(
            new StatusBarBehavior()
            .StatusBarColor(DeepSkyBlue)
        )
        .Content(
            new Grid()
            .RowDefinitions(e => e.Star(1.2).Star(5).Star(3).Star(0.8))
            .RowSpacing(15)
            .Margin(20,15)
            .Children(
                new HorizontalStackLayout()
                .CenterHorizontal()
                .Spacing(5)
                .Margin(0,25,0,0)
                .Children(
                    new Label()
                    .Text("My")
                    .TextColor(DeepSkyBlue)
                    .FontSize(40),

                    new Label()
                    .Text("FINANCE")
                    .TextColor(Black)
                    .FontSize(40)
                    .FontAttributes(Bold)
                ),

                new Image()
                .Source("getstarted")
                .SizeRequest(300,500)
                .Row(1),

                new VerticalStackLayout()
                .Spacing(10)
                .Row(2)
                .Children(
                    new Label()
                    .Text("New age of the financial structure")
                    .TextColor(DeepSkyBlue)
                    .FontSize(34)
                    .FontAttributes(Bold),

                    new Label()
                    .Text("Start exploring the crypto world now with our app it's easy and secure")
                    .TextColor(Black)
                    .FontAttributes(Italic)
                    .FontSize(16)
                ),

                new Button()
                .Text("Get started")
                .BackgroundColor(DeepSkyBlue)
                .TextColor(Black)
                .FontSize(16)
                .Row(3)
                .OnClicked(async (sender,arg) =>
                {
                    await AppShell.Current.GoToAsync("//LoginPage", true);
                })
            )
        );
    }
}
