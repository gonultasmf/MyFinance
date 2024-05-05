namespace MyFinance.Views;

public abstract class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
{
    protected BasePage(TViewModel viewModel, string title)
    {
        base.BindingContext = viewModel;
        Title = title;

        Build();
    }

    protected new TViewModel BindingContext => (TViewModel)base.BindingContext;

    public abstract void Build();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Build();

        HotReloadHandler.UpdateApplicationEvent += ReloadUI;
    }

    private void ReloadUI(Type[]? obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Build();
        });
    }
}

public abstract class BasePage : ContentPage
{
    protected BasePage(string title)
    {
        Title = title;

        Build();
    }

    public abstract void Build();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Build();

        HotReloadHandler.UpdateApplicationEvent += ReloadUI;
    }

    private void ReloadUI(Type[]? obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Build();
        });
    }
}
