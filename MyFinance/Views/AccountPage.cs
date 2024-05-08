namespace MyFinance.Views;

public partial class AccountPage(AccountPageViewModel viewModel) : BasePage<AccountPageViewModel>(viewModel, "Account Page")
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
