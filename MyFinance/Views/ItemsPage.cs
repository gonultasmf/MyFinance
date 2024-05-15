using FmgLib.MauiMarkup;
using Microsoft.Maui.Controls.Shapes;
using DXImage = DevExpress.Maui.Core.DXImage;

namespace MyFinance.Views;

public partial class ItemsPage(ItemsPageViewModel viewModel) : FmgLibContentPage<ItemsPageViewModel>(viewModel)
{
    public override void Build()
    {
        this
        .Content(
            new TabView()
            .HeaderPanelBackgroundColor(DeepSkyBlue)
            .Items_ContentProp(
                new TabViewItem()
                .HeaderText("Tüm İşlemler")
                .HeaderTextColor(Black)
                .HeaderFontAttributes(Bold)
                .Content(
                    new Grid()
                    .RowDefinitions(e => e.Star(.7).Star(9.3))
                    .Spacing(10)
                    .Margin(10)
                    .Children(
                        new ComboBoxEdit()
                        .SelectedIndex(e => e.Path("AllIType"))
                        .SelectionChangedCommand(e => e.Path("AllItemTypeChangedCommand"))
                        .ItemsSource(new List<string>
                        {
                            "Son 1 Haftalık Veri",
                            "Son 1 Aylık Veri",
                            "Son 6 Aylık Veri",
                            "Son 1 Yıllık Veri"
                        }),

                        new ShimmerView()
                        .WaveWidth(0.7)
                        .WaveOpacity(0.8)
                        .WaveDuration(new TimeSpan(0,0,0,1))
                        .IsLoading(e => e.Path("IsLoadingAllItems"))
                        .Row(1)
                        .LoadingView(
                            new DXCollectionView()
                            .ItemsSource(e => e.Path("LoadingItems"))
                            .IsScrollBarVisible(false)
                            .ItemTemplate(() =>
                                new Border()
                                .Margin(5,5)
                                .BackgroundColor(LightGray)
                                .StrokeShape(new RoundRectangle().CornerRadius(25))
                                .HeightRequest(60)
                                .StrokeThickness(0)
                            )
                        )
                        .Content(
                            new DXCollectionView()
                            .ItemsSource(e => e.Path("AllItems"))
                            .ItemTemplate(() =>
                                new Grid()
                                .RowDefinitions(e => e.Star().Star())
                                .ColumnDefinitions(e => e.Star(1).Star(6).Star(3))
                                .Spacing(5)
                                .Margin(5)
                                .Children(
                                    new DXImage()
                                    .Source(e => e.Path("Icon"))
                                    .SizeRequest(30, 30)
                                    .RowSpan(2),

                                    new Label()
                                    .FontAttributes(Bold)
                                    .TextColor(Black)
                                    .Text(e => e.Path("Title"))
                                    .AlignBottom()
                                    .Column(1),

                                    new Label()
                                    .TextColor(DarkGray)
                                    .Text(e => e.Path("Description"))
                                    .FontSize(12)
                                    .FontAttributes(Italic)
                                    .AlignTop()
                                    .Column(1)
                                    .Row(1),

                                    new Label()
                                    .Text(e => e.Path("Date"))
                                    .TextColor(DarkGray)
                                    .FontSize(10)
                                    .Column(2)
                                    .AlignBottomEnd(),

                                    new Label()
                                    .Text(e => e.Path("Amount"))
                                    .TextColor(e => e.Path("Color"))
                                    .FontSize(12)
                                    .Column(2)
                                    .Row(1)
                                    .AlignTopEnd()
                                )
                            )
                        )
                    )
                ),

                new TabViewItem()
                .HeaderText("Gelirler")
                .HeaderTextColor(Black)
                .HeaderFontAttributes(Bold)
                .Content(
                    new Grid()
                    .RowDefinitions(e => e.Star(.7).Star(9.3))
                    .Spacing(10)
                    .Margin(10)
                    .Children(
                        new ComboBoxEdit()
                        .SelectedIndex(e => e.Path("InIType"))
                        .SelectionChangedCommand(e => e.Path("InItemTypeChangedCommand"))
                        .ItemsSource(new List<string>
                        {
                            "Son 1 Haftalık Veri",
                            "Son 1 Aylık Veri",
                            "Son 6 Aylık Veri",
                            "Son 1 Yıllık Veri"
                        }),

                        new ShimmerView()
                        .WaveWidth(0.7)
                        .WaveOpacity(0.8)
                        .WaveDuration(new TimeSpan(0, 0, 0, 1))
                        .IsLoading(e => e.Path("IsLoadingInItems"))
                        .Row(1)
                        .LoadingView(
                            new DXCollectionView()
                            .ItemsSource(e => e.Path("LoadingItems"))
                            .IsScrollBarVisible(false)
                            .ItemTemplate(() =>
                                new Border()
                                .Margin(5, 5)
                                .BackgroundColor(LightGray)
                                .StrokeShape(new RoundRectangle().CornerRadius(25))
                                .HeightRequest(60)
                                .StrokeThickness(0)
                            )
                        )
                        .Content(
                            new DXCollectionView()
                            .ItemsSource(e => e.Path("InItems"))
                            .ItemTemplate(() =>
                                new Grid()
                                .RowDefinitions(e => e.Star().Star())
                                .ColumnDefinitions(e => e.Star(1).Star(6).Star(3))
                                .Spacing(5)
                                .Margin(5)
                                .Children(
                                    new DXImage()
                                    .Source(e => e.Path("Icon"))
                                    .SizeRequest(30, 30)
                                    .RowSpan(2),

                                    new Label()
                                    .FontAttributes(Bold)
                                    .TextColor(Black)
                                    .Text(e => e.Path("Title"))
                                    .AlignBottom()
                                    .Column(1),

                                    new Label()
                                    .TextColor(DarkGray)
                                    .Text(e => e.Path("Description"))
                                    .FontSize(12)
                                    .FontAttributes(Italic)
                                    .AlignTop()
                                    .Column(1)
                                    .Row(1),

                                    new Label()
                                    .Text(e => e.Path("Date"))
                                    .TextColor(DarkGray)
                                    .FontSize(10)
                                    .Column(2)
                                    .AlignBottomEnd(),

                                    new Label()
                                    .Text(e => e.Path("Amount"))
                                    .TextColor(e => e.Path("Color"))
                                    .FontSize(12)
                                    .Column(2)
                                    .Row(1)
                                    .AlignTopEnd()
                                )
                            )
                        )
                    )
                ),

                new TabViewItem()
                .HeaderText("Giderler")
                .HeaderTextColor(Black)
                .HeaderFontAttributes(Bold)
                .Content(
                    new Grid()
                    .RowDefinitions(e => e.Star(.7).Star(9.3))
                    .Spacing(10)
                    .Margin(10)
                    .Children(
                        new ComboBoxEdit()
                        .SelectedIndex(e => e.Path("OutIType"))
                        .SelectionChangedCommand(e => e.Path("OutItemTypeChangedCommand"))
                        .ItemsSource(new List<string>
                        {
                            "Son 1 Haftalık Veri",
                            "Son 1 Aylık Veri",
                            "Son 6 Aylık Veri",
                            "Son 1 Yıllık Veri"
                        }),

                        new ShimmerView()
                        .WaveWidth(0.7)
                        .WaveOpacity(0.8)
                        .WaveDuration(new TimeSpan(0, 0, 0, 1))
                        .IsLoading(e => e.Path("IsLoadingOutItems"))
                        .Row(1)
                        .LoadingView(
                            new DXCollectionView()
                            .ItemsSource(e => e.Path("LoadingItems"))
                            .IsScrollBarVisible(false)
                            .ItemTemplate(() =>
                                new Border()
                                .Margin(5, 5)
                                .BackgroundColor(LightGray)
                                .StrokeShape(new RoundRectangle().CornerRadius(25))
                                .HeightRequest(60)
                                .StrokeThickness(0)
                            )
                        )
                        .Content(
                            new DXCollectionView()
                            .ItemsSource(e => e.Path("OutItems"))
                            .ItemTemplate(() =>
                                new Grid()
                                .RowDefinitions(e => e.Star().Star())
                                .ColumnDefinitions(e => e.Star(1).Star(6).Star(3))
                                .Spacing(5)
                                .Margin(5)
                                .Children(
                                    new DXImage()
                                    .Source(e => e.Path("Icon"))
                                    .SizeRequest(30, 30)
                                    .RowSpan(2),

                                    new Label()
                                    .FontAttributes(Bold)
                                    .TextColor(Black)
                                    .Text(e => e.Path("Title"))
                                    .AlignBottom()
                                    .Column(1),

                                    new Label()
                                    .TextColor(DarkGray)
                                    .Text(e => e.Path("Description"))
                                    .FontSize(12)
                                    .FontAttributes(Italic)
                                    .AlignTop()
                                    .Column(1)
                                    .Row(1),

                                    new Label()
                                    .Text(e => e.Path("Date"))
                                    .TextColor(DarkGray)
                                    .FontSize(10)
                                    .Column(2)
                                    .AlignBottomEnd(),

                                    new Label()
                                    .Text(e => e.Path("Amount"))
                                    .TextColor(e => e.Path("Color"))
                                    .FontSize(12)
                                    .Column(2)
                                    .Row(1)
                                    .AlignTopEnd()
                                )
                            )
                        )
                    )
                )
            )
        );
    }
}