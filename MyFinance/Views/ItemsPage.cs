using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Views;

public partial class ItemsPage(ItemsPageViewModel viewModel) : BasePage<ItemsPageViewModel>(viewModel, "Items Page")
{
    public override void Build()
    {
        this
        .Content(
            new Label()
            .Text("ItemsPage")
            .Center()
        );
    }
}