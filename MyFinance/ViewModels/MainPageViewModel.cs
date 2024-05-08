namespace MyFinance.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<OperatonItemsVM> items = new();

    [ObservableProperty]
    private string totalBalance;

    [ObservableProperty]
    private string totalIncome;

    [ObservableProperty]
    private string totalExpense;

    [ObservableProperty]
    private UserCardVM user = new();

    public MainPageViewModel(IUserRepo userRepo)
    {
        var accessUser = AuthCheckHelper.ParseBasicAuthToken(SecureStorage.GetAsync("USERAUTH").Result);
        var currentUser = userRepo.GetSingleAsync(e => e.Email == accessUser.Item1 && e.Password == accessUser.Item2).Result;
        if (currentUser == null)
            currentUser = default;

        TotalBalance = "25,291.50 ₺";
        TotalExpense = "2,367.82 ₺";
        TotalIncome = "167.82 ₺";
        User = new()
        {
            Name = $"{currentUser?.FirstName} {currentUser?.LastName}",
            Email = currentUser?.Email ?? string.Empty
        };
        Random random = new Random();
        for (int i = 1; i <= 10; i++)
        {
            var amount = random.Next(1,10000);
            Items.Add(
                new OperatonItemsVM
                {
                    Id = Guid.NewGuid(),
                    Icon = amount % 2 == 0 ? "loss.png" : "profits.png",
                    Color = amount % 2 == 0 ? Red : Green,
                    Date = DateTime.Now.AddDays(-(amount % 20)).ToString("dd.MM.yyyy HH:mm"),
                    Title = amount % 2 == 0 ? "Borç ödendi" : "Ödeme Alındı",
                    Description = amount % 2 == 0 ? "Ödemeler yapıldı" : "Yaka parası alındı.",
                    Amount = $"{amount} ₺"
                }
            );
        }
        Items = Items.OrderByDescending(e => DateTime.Parse(e.Date)).ToList();
    }
}
