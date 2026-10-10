namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var days = new string[30];
        for (var day = 0; day < days.Length; day++)
            days[day] = (day + 2).ToString();

        var months = new string[12];
        for (var month = 0; month < months.Length; month++)
            months[month] = (month + 1).ToString();

        var birthsCounts = new double[30, 12];
        foreach (var name in names)
        {
            if (name.BirthDate.Day != 1)
                birthsCounts[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
        }

        return new HeatmapData(
            "Рождаемость по дням и месяцам",
            birthsCounts,
            days,
            months);
    }
}
