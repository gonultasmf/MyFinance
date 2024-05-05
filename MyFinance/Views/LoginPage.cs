using CommunityToolkit.Maui.Core;

namespace MyFinance.Views;

public partial class LoginPage(LoginPageViewModel viewModel) : BasePage<LoginPageViewModel>(viewModel, "Login Page")
{
    public override void Build()
    {
        this
        .ShellNavBarIsVisible(false)
        .Behaviors(
            new StatusBarBehavior()
            .StatusBarColor(White)
            .StatusBarStyle(StatusBarStyle.DarkContent)
        )
        .Content(
            new Grid()
            .RowDefinitions(e => e.Star(3).Star(4).Star(3))
            .RowSpacing(15)
            .Margin(20, 15)
            .Children(
                (IView)new VerticalStackLayout()
                .Spacing(10)
                .Children(
                    new HorizontalStackLayout()
                    .CenterHorizontal()
                    .Spacing(5)
                    .Margin(0, 25, 0, 0)
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

                    new Label()
                    .Text("Welcome back")
                    .TextColor(Black)
                    .FontSize(25)
                    .FontAttributes(Bold),

                    new Label()
                    .Text("Please enter your details")
                    .TextColor(Black)
                    .FontSize(15)
                    .FontAttributes(Italic)
                ),

                new VerticalStackLayout()
                .Spacing(10)
                .Row(1)
                .Children( 
                    new TextField()
                    .Title("Email")
                    .TitleColor(DeepSkyBlue)
                    .BorderColor(DeepSkyBlue)
                    .TextColor(Black),

                    new TextField()
                    .Title("Password")
                    .TitleColor(DeepSkyBlue)
                    .BorderColor(DeepSkyBlue)
                    .TextColor(Black)
                    .IsPassword(true)
                )
            )
        );
    }
}
