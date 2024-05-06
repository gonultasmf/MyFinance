using System.Windows.Input;

namespace MyFinance.ViewModels;

public partial class RegisterPageViewModel(IUserRepo repo) : BaseViewModel
{
    [ObservableProperty]
    private User userModel = new()
    {
        Email = string.Empty,
        FirstName = string.Empty,
        LastName = string.Empty,
        Password = string.Empty
    };

    [ObservableProperty]
    private bool isPopupShow = false;

    [ObservableProperty]
    private bool isUserAdded = false;

    public ICommand RegisterCommand => new Command(async () =>
    {
        if (UserModel == default!)
            return;

        UserModel.IsActive = true;
        var result = await repo.InsertAsync(UserModel);

        if (!result)
        {
            IsUserAdded = false;
            IsPopupShow = true;
        }
        else
        {
            IsUserAdded = true;
            IsPopupShow = true;
            await AppShell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    });

    public ICommand GoToLoginCommand => new Command(async () =>
    {
        await AppShell.Current.GoToAsync($"//{nameof(LoginPage)}");
    });


}
