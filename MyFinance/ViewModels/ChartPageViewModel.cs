using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Linq;
using LiveChartsCore.Drawing;

namespace MyFinance.ViewModels;

public partial class ChartPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string totalBalance;

    [ObservableProperty]
    private string totalIncome;

    [ObservableProperty]
    private string totalExpense;

    [ObservableProperty]
    private Axis[] xAxes;

    [ObservableProperty]
    private Axis[] yAxes;

    [ObservableProperty]
    private ISeries[] series;

    [ObservableProperty]
    private ChartType cType;


    private List<OperatonItemsVM> items = new();

    public ChartPageViewModel()
    {
        CType = ChartType.Weekly;
        TotalBalance = "25,291.50 ₺";
        TotalExpense = "2,367.82 ₺";
        TotalIncome = "167.82 ₺";



        Random random = new Random();
        for (int i = 1; i <= 100; i++)
        {
            var amount = random.Next(1, 10000);
            items.Add(
                new OperatonItemsVM
                {
                    Id = Guid.NewGuid(),
                    Icon = amount % 2 == 0 ? "loss.png" : "profits.png",
                    Color = amount % 2 == 0 ? Red : Green,
                    Date = DateTime.Now.AddDays(-(amount % 7)).ToString("dd.MM.yyyy HH:mm"),
                    Title = amount % 2 == 0 ? "Borç ödendi" : "Ödeme Alındı",
                    Description = amount % 2 == 0 ? "Ödemeler yapıldı" : "Yaka parası alındı.",
                    Amount = $"{amount} ₺"
                }
            );
        }

        Calc(7);
    }


    [RelayCommand]
    public void ChartTypeChanged()
    {
        if (CType == ChartType.Weekly)
            Calc(7);
        else if (CType == ChartType.Monthly)
            Calc(30);
        else if (CType == ChartType.SixMonthly)
            Calc(180);
        else if (CType == ChartType.Yearly)
            Calc(365);
    }

    private void Calc(int days)
    {
        var graphVals = items
            .Where(e => DateTime.Parse(e.Date) > DateTime.Now.AddDays(-days))
            .Select(e => new OperationGraphVM { Date = DateTime.Parse(e.Date).Date, Amount = double.Parse(e.Amount.Trim().Trim('₺')), IsIncome = e.Color == Green })
            .ToList();

        var strokeThickness = 6;
        var strokeDashArray = new float[] { 3 * strokeThickness, 2 * strokeThickness };
        var effect = new DashEffect(strokeDashArray);

        var inVals = graphVals
            .Where(x => x.IsIncome)
            .GroupBy(e => e.Date)
            .OrderBy(g => g.Key.Year)
            .ThenBy(g => g.Key.Month)
            .ThenBy(g => g.Key.Day)
            .Select(e => new OperationGraphVM { Date = e.Key, Amount = e.Sum(x => x.Amount), IsIncome = true })
            .ToList();

        var outVals = graphVals
            .Where(x => !x.IsIncome)
            .GroupBy(e => e.Date)
            .OrderBy(g => g.Key.Year)
            .ThenBy(g => g.Key.Month)
            .ThenBy(g => g.Key.Day)
            .Select(e => new OperationGraphVM { Date = e.Key, Amount = e.Sum(x => x.Amount), IsIncome = false })
            .ToList();

        var vals = inVals
            .Join(outVals, 
                i => i.Date, 
                e => e.Date, 
                (i, e) => i.Amount - e.Amount)
            .ToList();

        Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = inVals.Select(e => e.Amount),
                LineSmoothness = 0,
                Fill = null
            },

            new LineSeries<double>
            {
                Values = outVals.Select(e => e.Amount),
                LineSmoothness = 1,
                Fill = null
            },

            new LineSeries<double>
            {
                Values = vals,
                LineSmoothness = 2
            }
        };

        XAxes = new Axis[]
        {
            new Axis
            {
                CrosshairLabelsBackground = SKColors.DarkOrange.AsLvcColor(),
                CrosshairLabelsPaint = new SolidColorPaint(SKColors.DarkRed, 1),
                CrosshairPaint = new SolidColorPaint(SKColors.DarkOrange, 1),
                Labels = DateTimeHelper.GetDayNames(graphVals.GroupBy(e => e.Date).Select(e => e.Key).ToList()),
                LabelsRotation = 90,
                NamePadding = new Padding(0),
                Padding = new Padding(0),
                NameTextSize = 12
            }
        };

        YAxes = new Axis[]
        {
            new Axis
            {
                CrosshairLabelsBackground = SKColors.DarkOrange.AsLvcColor(),
                CrosshairLabelsPaint = new SolidColorPaint(SKColors.DarkRed, 1),
                CrosshairPaint = new SolidColorPaint(SKColors.DarkOrange, 1),
                CrosshairSnapEnabled = true // snapping is also supported
            }
        };
    }
}
