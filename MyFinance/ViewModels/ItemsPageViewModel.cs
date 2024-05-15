namespace MyFinance.ViewModels;

public partial class ItemsPageViewModel : BaseViewModel
{
    static List<OperatonItemsVM> AllItems = new List<OperatonItemsVM>();

    [ObservableProperty]
    private List<OperatonItemsVM> items = new();

    [ObservableProperty]
    private List<int> loadingItems = new() { 0,0,0,0,0,0,0,0,0,0,0 };

    [ObservableProperty]
    private bool isLoadingItems = false;

    [ObservableProperty]
    private bool isRefreshing = false;

    [ObservableProperty]
    private int operationType = 0;

    [ObservableProperty]
    private int dateType = 3;

    [ObservableProperty]
    private bool isShowPopup = false;

    private int index = 0;

    public ItemsPageViewModel()
    {
        Random random = new Random();
        for (int i = 1; i <= 500; i++)
        {
            var amount = random.Next(1, 10000);
            AllItems.Add(
                new OperatonItemsVM
                {
                    Id = Guid.NewGuid(),
                    Icon = amount % 2 == 0 ? "loss.png" : "profits.png",
                    Color = amount % 2 == 0 ? Red : Green,
                    Date = DateTime.Now.AddDays(-(amount % 200)).ToString("dd.MM.yyyy HH:mm"),
                    Title = amount % 2 == 0 ? "Borç ödendi" : "Ödeme Alındı",
                    Description = amount % 2 == 0 ? "Ödemeler yapıldı" : "Yaka parası alındı.",
                    Amount = $"{amount} ₺"
                }
            );
        }

        Items = AllItems.Take(15).ToList();
    }


    public List<OperatonItemsVM> GetItems(DateTime date)
    {
        var tempList = AllItems
            .Where(e => DateTime.Parse(e.Date) >= date && OperationType == 0 ? true : OperationType == 1 ? e.Color == Green : e.Color == Red)
            .OrderByDescending(e => DateTime.Parse(e.Date))
            .Skip(index)
            .Take(15)
            .ToList();

        return tempList;
    }

    [RelayCommand]
    public async Task ApplyFilter()
    {
        IsLoadingItems = true;
        index = 0;

        if (DateType == 0)
            Items = GetItems(DateTime.Now.AddDays(-7));
        else if (DateType == 1)
            Items = GetItems(DateTime.Now.AddMonths(-1));
        else if (DateType == 2)
            Items = GetItems(DateTime.Now.AddMonths(-6));
        else if (DateType == 3)
            Items = GetItems(DateTime.Now.AddYears(-1));

        IsShowPopup = false;
        await Task.Delay(1500);

        IsLoadingItems = false;
    }

    [RelayCommand]
    public async Task LoadMore()
    {
        if (index != 0)
            IsRefreshing = true;

        if (DateType == 0)
            Items.AddRange(GetItems(DateTime.Now.AddDays(-7)));
        else if (DateType == 1)
            Items.AddRange(GetItems(DateTime.Now.AddMonths(-1)));
        else if (DateType == 2)
            Items.AddRange(GetItems(DateTime.Now.AddMonths(-6)));
        else if (DateType == 3)
            Items.AddRange(GetItems(DateTime.Now.AddYears(-1)));

        //await Task.Delay(1500);
        index += 15;
        IsRefreshing = false;
    }

    [RelayCommand]
    public async Task ShowFilterPopup()
    {
        IsShowPopup = true;
    }
}
