using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Linq;
using LiveChartsCore.Drawing;
using System.Linq.Expressions;
using System.Globalization;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LiveChartsCore.Kernel.Sketches;

namespace MyFinance.ViewModels;

public partial class ChartPageViewModel : BaseViewModel
{
    CultureInfo culture = new CultureInfo("tr-TR");

    [ObservableProperty]
    private string totalBalance;

    [ObservableProperty]
    private string totalIncome;

    [ObservableProperty]
    private string totalExpense;

    [ObservableProperty]
    private Axis[] xAxes;

    [ObservableProperty]
    private ICartesianAxis[] yAxesnew =
    {
            new Axis // the "units" and "tens" series will be scaled on this axis
            {
                Name = "Tens",
                NameTextSize = 14,
                NamePaint = new SolidColorPaint(s_green),
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                Padding =  new LiveChartsCore.Drawing.Padding(0, 0, 20, 0),
                TextSize = 12,
                LabelsPaint = new SolidColorPaint(s_green),
                TicksPaint = new SolidColorPaint(s_green),
                SubticksPaint = new SolidColorPaint(s_green),
                DrawTicksPath = true
            },
            new Axis // the "hundreds" series will be scaled on this axis
            {
                Name = "Hundreds",
                NameTextSize = 14,
                NamePaint = new SolidColorPaint(s_red),
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                Padding =  new LiveChartsCore.Drawing.Padding(20, 0, 0, 0),
                TextSize = 12,
                LabelsPaint = new SolidColorPaint(s_red),
                TicksPaint = new SolidColorPaint(s_red),
                SubticksPaint = new SolidColorPaint(s_red),
                DrawTicksPath = true,
                ShowSeparatorLines = false,
                Position = LiveChartsCore.Measure.AxisPosition.End
            },
            new Axis // the "thousands" series will be scaled on this axis
            {
                Name = "Thousands",
                NameTextSize = 14,
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                Padding = new LiveChartsCore.Drawing.Padding(20, 0, 0, 0),
                NamePaint = new SolidColorPaint(s_blue),
                TextSize = 12,
                LabelsPaint = new SolidColorPaint(s_blue),
                TicksPaint = new SolidColorPaint(s_blue),
                SubticksPaint = new SolidColorPaint(s_blue),
                DrawTicksPath = true,
                ShowSeparatorLines = false,
                Position = LiveChartsCore.Measure.AxisPosition.End
            }
        };

    [ObservableProperty]
    private ISeries[] series;

    [ObservableProperty]
    private int cType;


    private static readonly SKColor s_green = new(0, 128, 0);
    private static readonly SKColor s_red = new(255, 0, 0);
    private static readonly SKColor s_blue = new(0, 0, 255);


    private List<OperatonItemsVM> items = new();

    public ChartPageViewModel()
    {
        CType = (int)ChartType.Weekly;
        TotalBalance = "25,291.50 ₺";
        TotalExpense = "2,367.82 ₺";
        TotalIncome = "167.82 ₺";



        Random random = new Random();
        for (int i = 1; i <= 500; i++)
        {
            var amount = random.Next(1, 10000);
            items.Add(
                new OperatonItemsVM
                {
                    Id = Guid.NewGuid(),
                    Icon = amount % 2 == 0 ? "loss.png" : "profits.png",
                    Color = amount % 2 == 0 ? Red : Green,
                    Date = DateTime.Now.AddDays(-(amount % 180)).ToString("dd.MM.yyyy HH:mm"),
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
        if (CType == (int)ChartType.Weekly)
            Calc(7);
        else if (CType == (int)ChartType.Monthly)
            Calc(30);
        else if (CType == (int)ChartType.SixMonthly)
            Calc(180);
    }

    private void Calc(int days)
    {
        var graphVals = items
            .Where(e => DateTime.Parse(e.Date) > DateTime.Now.AddDays(-days))
            .Select(e => new OperationGraphVM { Date = DateTime.Parse(e.Date).Date, Amount = double.Parse(e.Amount.Trim().Trim('₺')), IsIncome = e.Color == Green })
            .OrderBy(e => e.Date)
            .ToList();

        var strokeThickness = 6;
        var strokeDashArray = new float[] { 3 * strokeThickness, 2 * strokeThickness };
        var effect = new DashEffect(strokeDashArray);

        Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Name = "Gelir",
                Values = new List<double>(),
                Stroke = new SolidColorPaint(s_green, 5),
                GeometryStroke = new SolidColorPaint(s_green, 5),
                Fill = null,
                ScalesXAt = 0
            },

            new LineSeries<double>
            {
                Name = "Gider",
                Values = new List<double>(),
                Stroke = new SolidColorPaint(s_red, 5),
                GeometryStroke = new SolidColorPaint(s_red, 5),
                Fill = null,
                ScalesXAt = 0
            },

            new LineSeries<double>
            {
                Name = "Kar",
                Values = new List<double>(),
                Stroke = new SolidColorPaint(s_blue, 5),
                GeometryStroke = new SolidColorPaint(s_blue, 5)
            }
        };

        XAxes = new Axis[]
        {
            new Axis
            {
                //CrosshairLabelsBackground = SKColors.DarkOrange.AsLvcColor(),
                //CrosshairLabelsPaint = new SolidColorPaint(SKColors.DarkRed, 1),
                //CrosshairPaint = new SolidColorPaint(SKColors.DarkOrange, 1),
                Labels = new List<string>(),
                LabelsRotation = 90,
                NamePadding = new Padding(0),
                Padding = new Padding(0),
                NameTextSize = 4
            }
        };
        if (CType == (int)ChartType.Weekly)
        {
            double totalInVal = 0, totalOutVal = 0;
            for (int i = 7; i > 0; i--)
            {
                var date = DateTime.Now.Date.AddDays(-i);
                var inVal = graphVals
                    .Where(x => x.IsIncome && x.Date == date)
                    .Select(e => e.Amount)
                    .Sum();
                var outVal = graphVals
                    .Where(x => !x.IsIncome && x.Date == date)
                    .Select(e => e.Amount)
                    .Sum();

                totalInVal += inVal;
                totalOutVal += outVal;

                ((List<double>)Series[0].Values).Add(inVal);
                ((List<double>)Series[1].Values).Add(outVal);
                ((List<double>)Series[2].Values).Add(inVal - outVal);
                XAxes[0].Labels.Add(culture.DateTimeFormat.GetDayName(date.DayOfWeek));
            }

            TotalIncome = $"{totalInVal} ₺";
            TotalExpense = $"{totalOutVal} ₺";
            TotalBalance = $"{totalInVal - totalOutVal} ₺";
        }
        else if (CType == (int)ChartType.Monthly)
        {
            double totalInVal = 0, totalOutVal = 0;
            for (int i = 7; i > 0; i--)
            {
                var date1 = DateTime.Now.Date.AddDays(-i * 7);
                var date2 = DateTime.Now.Date.AddDays((-i + 1) * 7);

                var inVal = graphVals
                    .Where(x => x.IsIncome && x.Date >= date1 && x.Date < date2)
                    .Select(e => e.Amount)
                    .Sum();
                var outVal = graphVals
                    .Where(x => !x.IsIncome && x.Date >= date1 && x.Date < date2)
                    .Select(e => e.Amount)
                    .Sum();

                totalInVal += inVal;
                totalOutVal += outVal;

                ((List<double>)Series[0].Values).Add(inVal);
                ((List<double>)Series[1].Values).Add(outVal);
                ((List<double>)Series[2].Values).Add(inVal - outVal);
                XAxes[0].Labels.Add($"{i} Hafta Önce");
            }

            TotalIncome = $"{totalInVal} ₺";
            TotalExpense = $"{totalOutVal} ₺";
            TotalBalance = $"{totalInVal - totalOutVal} ₺";
        }
        else if (CType == (int)ChartType.SixMonthly)
        {
            double totalInVal = 0, totalOutVal = 0;
            for (int i = 6; i > 0; i--)
            {
                var date = DateTime.Now.Date.AddMonths(-i);
                var inVal = graphVals
                    .Where(x => x.IsIncome && new DateTime(x.Date.Year, x.Date.Month, 1) == new DateTime(date.Year, date.Month, 1))
                    .Select(e => e.Amount)
                    .Sum();
                var outVal = graphVals
                    .Where(x => !x.IsIncome && new DateTime(x.Date.Year, x.Date.Month, 1) == new DateTime(date.Year, date.Month, 1))
                    .Select(e => e.Amount)
                    .Sum();

                totalInVal += inVal;
                totalOutVal += outVal;

                ((List<double>)Series[0].Values).Add(inVal);
                ((List<double>)Series[1].Values).Add(outVal);
                ((List<double>)Series[2].Values).Add(inVal - outVal);
                XAxes[0].Labels.Add(culture.DateTimeFormat.GetMonthName(date.Month));
            }

            TotalIncome = $"{totalInVal} ₺";
            TotalExpense = $"{totalOutVal} ₺";
            TotalBalance = $"{totalInVal - totalOutVal} ₺";
        }
    }
}
