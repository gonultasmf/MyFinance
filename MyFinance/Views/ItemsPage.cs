using Microsoft.Maui.Controls.Shapes;
using DXImage = DevExpress.Maui.Core.DXImage;
using SwipeItem = DevExpress.Maui.CollectionView.SwipeItem;

namespace MyFinance.Views;

public partial class ItemsPage(ItemsPageViewModel viewModel) : FmgLibContentPage<ItemsPageViewModel>(viewModel)
{
    public override void Build()
    {
        this
        .Content(
            new Grid()
            .RowDefinitions(e => e.Star(.7).Star(9.3))
            .Spacing(10)
            .Margin(10)
            .Children(
                new Grid()
                .ColumnDefinitions(e => e.Star().Star())
                .FillHorizontal()
                .Children(
                    new Label()
                    .Text("İŞLEMLER")
                    .FontAttributes(Bold)
                    .FontSize(34)
                    .AlignStart(),

                    new ImageButton()
                    .Source("filter.png")
                    .SizeRequest(40,40)
                    .Command(e => e.Path("ShowFilterPopupCommand"))
                    .Column(1)
                    .AlignEnd()
                ),

                new ShimmerView()
                .WaveWidth(0.7)
                .WaveOpacity(0.8)
                .WaveDuration(new TimeSpan(0, 0, 0, 1))
                .IsLoading(e => e.Path("IsLoadingItems"))
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
                    .ItemsSource(e => e.Path("Items"))
                    .IsRefreshing(e => e.Path("IsRefreshing").BindingMode(TwoWay))
                    .IsLoadMoreEnabled(e => e.Path("IsLoadMoreEnabled"))
                    .LoadMoreCommand(e => e.Path("LoadMoreCommand"))
                    .IndicatorColor(DeepSkyBlue)
                    .ItemTemplate(() =>
                        new SwipeContainer()
                        .FullSwipeMode(FullSwipeMode.Both)
                        .StartSwipeItems(
                            new SwipeItemCollection()
                            {
                                new SwipeItem()
                                .Caption("Düzenle")
                                .BackgroundColor(Orange)
                                .Image("home.png")
                            }
                        )
                        .EndSwipeItems(
                            new SwipeItemCollection()
                            {
                                new SwipeItem()
                                .Caption("Sil")
                                .BackgroundColor(Red)
                                .Image("home.png")
                            }
                        )
                        .ItemView(
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
                ),

                new DXPopup()
                .IsOpen(e => e.Path("IsShowPopup"))
                .RowSpan(2)
                .AnimationDuration(new TimeSpan(0, 0, 0, 1))
                .VerticalAlignment(PopupVerticalAlignment.Center)
                .HorizontalAlignment(PopupHorizontalAlignment.Center)
                .AllowShadow(true)
                .AllowScrim(false)
                .ShadowHorizontalOffset(20)
                .ShadowVerticalOffset(20)
                .CornerRadius(20)
                .ShadowRadius(20)
                .ShadowColor(Gray)
                .Content(
                    new Grid()
                    .WidthRequest(250)
                    .HeightRequest(200)
                    .Padding(15)
                    .Margin(10)
                    .RowDefinitions(e => e.Absolute(50).Absolute(50).Absolute(50))
                    .RowSpacing(10)
                    .Children(
                        new ComboBoxEdit()
                        .SelectedIndex(e => e.Path("OperationType"))
                        .ItemsSource(new List<string>
                        {
                            "Tümü",
                            "Gelir",
                            "Gider"
                        }),

                        new ComboBoxEdit()
                        .SelectedIndex(e => e.Path("DateType"))
                        .Row(1)
                        .ItemsSource(new List<string>
                        {
                            "Son 1 Haftalık Veri",
                            "Son 1 Aylık Veri",
                            "Son 6 Aylık Veri",
                            "Son 1 Yıllık Veri"
                        }),

                        new Button()
                        .Text("Filtrele")
                        .FontAttributes(Bold)
                        .Command(e => e.Path("ApplyFilterCommand"))
                        .Row(2)
                    )
                )
            )
        );
    }
}