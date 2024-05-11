namespace MyFinance.Views;

public partial class AccountPage(AccountPageViewModel viewModel) : FmgLibContentPage<AccountPageViewModel>(viewModel)
{
    public override void Build()
    {
        this
        .Content(
            new Label()
            .Text("AccountPage")
            .Center()
        );
    }
}
