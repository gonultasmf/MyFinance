namespace MyFinance.ViewModels;

public partial class ItemsPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<OperatonItemsVM> allItems = new();

    [ObservableProperty]
    private List<OperatonItemsVM> inItems = new();

    [ObservableProperty]
    private List<OperatonItemsVM> outItems = new();

    [ObservableProperty]
    private List<int> loadingItems = new() { 0,0,0,0,0,0,0,0,0,0,0 };

    [ObservableProperty]
    private bool isLoadingAllItems = false;

    [ObservableProperty]
    private bool isLoadingInItems = false;

    [ObservableProperty]
    private bool isLoadingOutItems = false;

    [ObservableProperty]
    private int allIType = 0;

    [ObservableProperty]
    private int inIType = 0;

    [ObservableProperty]
    private int outIType = 0;

    public ItemsPageViewModel()
    {
        Random random = new Random();
        for (int i = 1; i <= 40; i++)
        {
            var amount = random.Next(1, 10000);
            AllItems.Add(
                new OperatonItemsVM
                {
                    Id = Guid.NewGuid(),
                    Icon = amount % 2 == 0 ? "loss.png" : "profits.png",
                    Color = amount % 2 == 0 ? Red : Green,
                    Date = DateTime.Now.AddDays(-(amount % 30)).ToString("dd.MM.yyyy HH:mm"),
                    Title = amount % 2 == 0 ? "Borç ödendi" : "Ödeme Alındı",
                    Description = amount % 2 == 0 ? "Ödemeler yapıldı" : "Yaka parası alındı.",
                    Amount = $"{amount} ₺"
                }
            );
        }
        AllItems = AllItems.OrderByDescending(e => DateTime.Parse(e.Date)).ToList();
        InItems = AllItems.Where(e => e.Color == Green).ToList();
        OutItems = AllItems.Where(e => e.Color == Red).ToList();
    }


    //[RelayCommand]
    public List<OperatonItemsVM> GetItems(DateTime date, bool? isIncome = null)
    {
        IsLoadingAllItems = true;

        List<OperatonItemsVM> tempList = new List<OperatonItemsVM>();
        Random random = new Random();
        for (int i = 1; i <= 70; i++)
        {
            var amount = random.Next(1, 10000);
            tempList.Add(
                new OperatonItemsVM
                {
                    Id = Guid.NewGuid(),
                    Icon = amount % 2 == 0 ? "loss.png" : "profits.png",
                    Color = amount % 2 == 0 ? Red : Green,
                    Date = DateTime.Now.AddDays(-(amount % 30)).ToString("dd.MM.yyyy HH:mm"),
                    Title = amount % 2 == 0 ? "Borç ödendi" : "Ödeme Alındı",
                    Description = amount % 2 == 0 ? "Ödemeler yapıldı" : "Yaka parası alındı.",
                    Amount = $"{amount} ₺"
                }
            );
        }
        tempList = tempList
            .Where(e => DateTime.Parse(e.Date) >= date && isIncome.HasValue ? isIncome.Value ? e.Color == Green : e.Color == Red : true)
            .OrderByDescending(e => DateTime.Parse(e.Date))
            .ToList();

        return tempList;
    }

    [RelayCommand]
    public async Task AllItemTypeChanged()
    {
        IsLoadingAllItems = true;
        await Task.Delay(3500);
        if (AllIType == 0)
            AllItems = GetItems(DateTime.Now.AddDays(-7));
        else if (AllIType == 1)
            AllItems = GetItems(DateTime.Now.AddMonths(-1));
        else if (AllIType == 2)
            AllItems = GetItems(DateTime.Now.AddMonths(-6));
        else if (AllIType == 3)
            AllItems = GetItems(DateTime.Now.AddYears(-1));

        IsLoadingAllItems = false;
    }

    [RelayCommand]
    public async Task InItemTypeChanged()
    {
        IsLoadingInItems = true;
        await Task.Delay(3500);
        if (InIType == 0)
            InItems = GetItems(DateTime.Now.AddDays(-7), true);
        else if (InIType == 1)
            InItems = GetItems(DateTime.Now.AddMonths(-1), true);
        else if (InIType == 2)
            InItems = GetItems(DateTime.Now.AddMonths(-6), true);
        else if (InIType == 3)
            InItems = GetItems(DateTime.Now.AddYears(-1), true);

        IsLoadingInItems = false;
    }

    [RelayCommand]
    public async Task OutItemTypeChanged()
    {
        IsLoadingOutItems = true;
        await Task.Delay(3500);
        if (OutIType == 0)
            OutItems = GetItems(DateTime.Now.AddDays(-7), false);
        else if (OutIType == 1)
            OutItems = GetItems(DateTime.Now.AddMonths(-1), false);
        else if (OutIType == 2)
            OutItems = GetItems(DateTime.Now.AddMonths(-6), false);
        else if (OutIType == 3)
            OutItems = GetItems(DateTime.Now.AddYears(-1), false);

        IsLoadingOutItems = false;
    }
}
