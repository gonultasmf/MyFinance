using DXImage = DevExpress.Maui.Core.DXImage;

namespace MyFinance.Views;

public partial class MainPage(MainPageViewModel viewModel) : BasePage<MainPageViewModel>(viewModel, "Main Page")
{
    public override void Build()
    {
        this
        .Content(
            new VerticalStackLayout()
            .Spacing(20)
            .Margin(20,20,20,10)
            .Children(
                new HorizontalStackLayout()
                .AlignStart()
                .Spacing(5)
                .Children(
                    new DXImage()
                    .Source("profile.png")
                    .SizeRequest(60,60),

                    new VerticalStackLayout()
                    .CenterVertical()
                    .Children(
                        new Label()
                        .FontAttributes(Bold)
                        .TextColor(Black)
                        .Text("Lilliie-May Mcdonelli"),

                        new Label()
                        .TextColor(Gray)
                        .Text("hello@amail.com")
                    )
                ),

                new VerticalStackLayout()
                .Spacing(-3)
                .Children(
                    new Label()
                    .FontAttributes(Bold)
                    .TextColor(Black)
                    .Text("Total balance"),

                    new Grid()
                    .RowDefinitions(e => e.Star().Star())
                    .ColumnDefinitions(e => e.Star(7).Star(3))
                    .Spacing(10,3)
                    .Children(
                        new Label()
                        .Text("25,291.50 ₺")
                        .FontAttributes(Bold)
                        .FontSize(40)
                        .RowSpan(2),

                        new Label()
                        .Text("+130.65%")
                        .TextColor(Green)
                        .FontSize(12)
                        .Column(1)
                        .AlignBottomEnd(),

                        new Label()
                        .Text("+2,367.72₺")
                        .TextColor(DarkGray)
                        .FontSize(12)
                        .Column(1)
                        .Row(1)
                        .AlignTopEnd()
                    )
                ),

                new Grid()
                .ColumnDefinitions(e => e.Star().Star())
                .ColumnSpacing(10)
                .Children(
                    new DXButton()
                    .HeightRequest(100)
                    .Content("Buy")
                    .TextColor(White)
                    .FontSize(16)
                    .ButtonType(Accent)
                    .Icon("up_arrow.png")
                    .IconColor(Red)
                    .CornerRadius(new CornerRadius(20))
                    .BackgroundColor(Black),

                    new DXButton()
                    .HeightRequest(100)
                    .Content("Sell")
                    .TextColor(Black)
                    .FontSize(16)
                    .ButtonType(Accent)
                    .Icon("down_arrow.png")
                    .IconColor(Green)
                    .CornerRadius(new CornerRadius(20))
                    .BackgroundColor(DeepSkyBlue)
                    .BorderColor(Black)
                    .BorderThickness(1)
                    .Column(1)
                ),

                new Frame()
                .CornerRadius(20)
                .BorderColor(Black)
                .BackgroundColor(Transparent)
                .Content(
                    new VerticalStackLayout()
                    .FillBothDirections()
                    .Children(
                        new Label()
                        .FontAttributes(Bold)
                        .TextColor(Black)
                        .Text("Watchlist"),

                        new Border()
                        .StrokeThickness(1),

                        new DXCollectionView()
                        .ItemsSource(e => e.Path(""))
                        .IsRefreshing(e => e.Path("").BindingMode(TwoWay))
                        .ItemTemplate(() =>
                            new Grid()
                            .RowDefinitions(e => e.Star().Star())
                            .ColumnDefinitions(e => e.Star(1).Star(6).Star(3))
                            .Margin(5)
                            .Children(
                                new DXImage()
                                .Source(e => e.Path(""))
                                .SizeRequest(40,40)
                                .RowSpan(2)

                            )
                        )
                    )
                )
            )
        );
    }
}
