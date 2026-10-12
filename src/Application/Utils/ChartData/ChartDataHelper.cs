using Application.Queries.AdminPanel.Reports.ReportsDto;

namespace Application.Utils.ChartData;

public static class ChartDataBuilder
{
    public static Dictionary<string, List<StackedVerticalBarChartResult>> BuildDailyHourlyMultiValueChart<T>(
        IEnumerable<T> source,
        Func<T, DateTime> dateSelector,
        Func<T, string> categorySelector,
        Func<T, double> valueSelector)
    {
        // ✅ Handle empty source
        if (source == null || !source.Any())
            return new Dictionary<string, List<StackedVerticalBarChartResult>>();

        var grouped = source
            .GroupBy(o => new { Date = dateSelector(o).Date, Hour = dateSelector(o).Hour })
            .ToDictionary(
                g => g.Key,
                g => g.GroupBy(categorySelector)
                    .ToDictionary(cg => cg.Key, cg => cg.Sum(valueSelector))
            );

        var categories = source
            .Select(categorySelector)
            .Distinct()
            .ToList();

        var start = source.Min(dateSelector);
        var end = DateTime.Now;

        var result = new Dictionary<string, List<StackedVerticalBarChartResult>>();

        var current = start;
        while (current <= end)
        {
            var dayKey = current.Date.ToPersianDate();
            if (!result.ContainsKey(dayKey))
                result[dayKey] = new List<StackedVerticalBarChartResult>();

            var values = new Dictionary<string, double>();
            var total = 0.0;

            foreach (var category in categories)
            {
                var key = new { current.Date, current.Hour };
                var value = grouped.TryGetValue(key, out var hourGroup) &&
                            hourGroup.TryGetValue(category, out var sum)
                    ? sum
                    : 0;

                values[category] = value;
                total += value;
            }

            values[global::Common.ChartData.Value_Total] = total;

            result[dayKey].Add(new StackedVerticalBarChartResult(
                current.Hour.FormatHourLabelPersian(),
                values));

            current = current.AddHours(1);
        }

        return result;
    }
}
