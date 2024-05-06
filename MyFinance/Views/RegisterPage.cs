using CommunityToolkit.Maui.Core;

namespace MyFinance.Views;

public partial class RegisterPage(RegisterPageViewModel viewModel) : BasePage<RegisterPageViewModel>(viewModel, "Register Page")
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
            .RowDefinitions(e => e.Star(2.5).Star(4).Star(2).Star(1.5))
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
                        .FontSize(44)
                        .FontAttributes(Bold),

                        new Label()
                        .Text("FINANCE")
                        .TextColor(Black)
                        .FontSize(44)
                        .FontAttributes(Bold)
                    ),

                    new Label()
                    .Text("Welcome to our app")
                    .TextColor(Black)
                    .FontSize(25)
                    .FontAttributes(Bold)
                    .Margin(0, 30, 0, 0),

                    new Label()
                    .Text("Create a free account")
                    .TextColor(Black)
                    .FontSize(15)
                    .FontAttributes(Italic)
                ),

                new VerticalStackLayout()
                .Spacing(15)
                .Row(1)
                .Children(
                    new TextEdit()
                    .LabelText("First Name")
                    .Text(e => e.Path("UserModel.FirstName")),

                    new TextEdit()
                    .LabelText("Last Name")
                    .Text(e => e.Path("UserModel.LastName")),

                    new TextEdit()
                    .LabelText("Email")
                    .Text(e => e.Path("UserModel.Username")),

                    new PasswordEdit()
                    .LabelText("Password")
                    .Text(e => e.Path("UserModel.Password")),

                    new PasswordEdit()
                    .LabelText("Password (Repeat)")
                    .Text(e => e.Path("UserModel.Password")),

                    new TextEdit()
                    .LabelText("Phone")
                    .Keyboard(Keyboard.Telephone)
                    .Text(e => e.Path("UserModel.PhoneNumber")),

                    new TextEdit()
                    .LabelText("Age")
                    .Keyboard(Keyboard.Numeric)
                    .Text(e => e.Path("UserModel.Age")),

                    new ComboBoxEdit()
                    .SelectedIndex(e => e.Path("UserModel.Gender"))
                    .ItemsSource(new List<string>
                    {
                        "Male",
                        "Female"
                    }),

                    new Grid()
                    .ColumnDefinitions(e => e.Star().Star())
                    .FillHorizontal()
                    .Children(
                        new CheckEdit()
                        .Label("Remember for 30 days")
                        .AlignStart()
                        .IsChecked(e => e.Path("UserModel.IsRememberMe")),

                         new Label()
                         .Text("Forget password")
                         .TextColor(DeepSkyBlue)
                         .TextDecorations(Underline)
                         .TextCenterVertical()
                         .Column(1)
                         .AlignEnd()
                    )
                ),

                new VerticalStackLayout()
                .Row(2)
                .Spacing(20)
                .Children(
                     new Button()
                    .Text("Sign up")
                    .BackgroundColor(DeepSkyBlue)
                    .TextColor(Black)
                    .FontSize(15)
                    .Command(BindingContext.RegisterCommand),

                     new HorizontalStackLayout()
                     .CenterHorizontal()
                     .Spacing(5)
                     .Children(
                         new Label()
                        .Text("Already have an account? ")
                        .TextColor(Black)
                        .FontAttributes(Italic)
                        .FontSize(16),

                         new Label()
                         .Text("Sign in")
                         .TextColor(DeepSkyBlue)
                         .TextDecorations(Underline)
                         .GestureRecognizers(
                             new TapGestureRecognizer()
                             .Command(BindingContext.GoToLoginCommand)
                         )
                     )
                ),

                new HorizontalStackLayout()
                .Row(3)
                .Center()
                .Spacing(5)
                .Children(
                    new Label()
                    .Text("By ")
                    .TextColor(Black)
                    .FontAttributes(Italic)
                    .FontSize(16),

                    new Label()
                    .Text("FmgLib.MauiMarkup")
                    .TextColor(DeepSkyBlue)
                    .FontAttributes(Bold)
                    .FontSize(18)
                ),

                new GeneralPopup("HATA", "Kullanıcı Adı veya Şifre Hatalı!!! ", "OK", PopupType.Error)
                .RowSpan(4)
                .IsOpen(e => e.Path("IsPopupShow"))
            )
        );
    }
}
