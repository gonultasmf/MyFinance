namespace MyFinance.Views;

public partial class ItemsPage(ItemsPageViewModel viewModel) : FmgLibContentPage<ItemsPageViewModel>(viewModel)
{
    public override void Build()
    {
        this
        .Content(
            new TabView()
            .ItemsSource(
                new List<TabViewItem>()
                {
                    new TabViewItem()
                    .HeaderText("Mail")
                    .HeaderTextColor(Black)
                    .HeaderIcon("home.png")
                    .HeaderIconColor(Black)
                    .Content(
                        new Grid()
                        .Children(
                            new Label()
                            .Text("Mail List")
                            .Center()
                        )
                    ),

                    new TabViewItem()
                    .HeaderText("Temp")
                    .HeaderTextColor(Black)
                    .HeaderIconColor(Black)
                    .HeaderIcon("home.png")
                    .Content(
                        new Grid()
                        .Children(
                            new Label()
                            .Text("Mail List")
                            .Center()
                        )
                    )
                }
            )
        );
    }
}