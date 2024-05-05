using System.Reflection;

namespace MyFinance.Views
{
    public partial class MainPage(MainPageViewModel viewModel) : BasePage<MainPageViewModel>(viewModel, "Main Page")
    {
        public override void Build()
        {
            var version = typeof(MauiApp).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            this
                .Content(
                    new ScrollView()
                    .Content(
                        new Grid()
                        .RowDefinitions(e => e.Star(90).Star(10))
                        .Children(
                            new StackLayout()
                            .Spacing(25)
                            .Children(
                                new Label()
                                .Text("Hello, World!")
                                .FontSize(32)
                                .CenterHorizontal()
                                .SemanticHeadingLevel(SemanticHeadingLevel.Level1),

                                new Label()
                                .Text("Welcome to .NET Multi-platform App UI")
                                .FontSize(18)
                                .CenterHorizontal()
                                .SemanticDescription("Welcome to dot net Multi platform App U I")
                                .SemanticHeadingLevel(SemanticHeadingLevel.Level1),

                                new Label()
                                .Text("Current count: 0")
                                .FontSize(18)
                                .FontAttributes(Bold)
                                .CenterHorizontal()
                                .Assign(out CounterLabel),

                                new Button()
                                .Text("Click me")
                                .CenterHorizontal()
                                .InvokeOnElement(btn => btn.Clicked += OnCounterClicked)
                                .SemanticHint("Counts the number of times you click"),

                                new Image()
                                .Source("dotnet_bot.png")
                                .WidthRequest(250)
                                .HeightRequest(310)
                                .CenterHorizontal()
                                .SemanticDescription("Cute dot net bot waving hi to you!")
                            ),

                            new Grid()
                            .BackgroundColor(AppColors.Primary)
                            .Row(1)
                            .Children(
                                new Label()
                                .Text($"dotNet version: {version}")
                                .TextColor(White)
                                .Center()
                            )
                        )
                    )

                );
        }

        private int _count = 0;
        private Label CounterLabel;


        private void OnCounterClicked(object? sender, EventArgs e)
        {
            _count++;
            CounterLabel.Text = $"Current count: {_count}";

            SemanticScreenReader.Announce(CounterLabel.Text);
        }
    }
}
