namespace MyFinance.Views;

public partial class ChartPage(ChartPageViewModel viewModel) : BasePage<ChartPageViewModel>(viewModel, "Chart Page")
{
    public override void Build()
    {
        this
        .Resources(
            ResourceDictionaryExtension
            .Add(new ResourceDictionary(), "lineSeriesHintOptions", new Style<SeriesCrosshairOptions>(e => e
                    .PointTextPattern("{}{S}: {V}M")
                    .ShowInLabel(true)
                    .AxisLabelVisible(true)
                    .AxisLineVisible(true)
                )
            )
        )
        .Content(
            new ChartView()
            .Series(
                [
                    new LineSeries()
                    .DisplayName("deneme")
                    .Data(
                        new SeriesDataAdapter()
                        .ArgumentDataMember("Year")
                        .DataSource(e => e.Path(""))
                        
                    )
                ]
            )
        );
    }
}