namespace MyFinance.Views;

public partial class ChartPage(ChartPageViewModel viewModel) : BasePage<ChartPageViewModel>(viewModel, "Chart Page")
{
    public override void Build()
    {
        this
        .Content(
            new Grid()
            .RowDefinitions(e => e.Star(.6).Star(8.8).Star(.6))
            .Margin(5)
            .Children(
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
                    .Spacing(10, 3)
                    .Children(
                        new Label()
                        .Text(e => e.Path("TotalBalance"))
                        .FontAttributes(Bold)
                        .FontSize(40)
                        .RowSpan(2),

                        new Label()
                        .Text(e => e.Path("TotalIncome"))
                        .TextColor(Green)
                        .FontSize(12)
                        .Column(1)
                        .AlignBottomEnd(),

                        new Label()
                        .Text(e => e.Path("TotalExpense"))
                        .TextColor(Red)
                        .FontSize(12)
                        .Column(1)
                        .Row(1)
                        .AlignTopEnd()
                    )
                ),

                new CartesianChart()
                .Row(1)
                .Series(e => e.Path("Series"))
                .XAxes(e => e.Path("XAxes"))
                .ZoomMode(ZoomX),

                new HorizontalStackLayout()
                .Row(2)
                .Spacing(10)
                .Children(
                    new Button().Command(e => e.Path("GoToPage1Command")).Text("Weeks"),
                    new Button().Command(e => e.Path("GoToPage2Command")).Text("Month"),
                    new Button().Command(e => e.Path("GoToPage3Command")).Text("6 Months"),
                    new Button().Command(e => e.Path("SeeAllCommand")).Text("Clear")
                )
            )
        );
    }
}