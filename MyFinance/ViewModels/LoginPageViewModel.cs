﻿using System.Windows.Input;

namespace MyFinance.ViewModels;

public partial class LoginPageViewModel(IUserRepo repo) : BaseViewModel
{
    [ObservableProperty]
    private LoginDTO login = new();

    [ObservableProperty]
    private bool isPopupShow = false;

    public ICommand LoginCommand => new Command(async () =>
    {
        if (Login == default!)
            return;

        var result = await repo.GetSingleAsync(x => x.Email == Login.Username && x.Password == Login.Password);

        if (result is null)
        {
            IsPopupShow = true;
        }
        else
        {
            if (Login.IsRememberMe)
            {
                var expireTime = DateTime.Now.AddDays(30);
                var auth = AuthCheckHelper.BasicAuth(result.Email, result.Password, expireTime);
                await SecureStorage.SetAsync("USERAUTH", auth);
            }

            await AppShell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    });
}