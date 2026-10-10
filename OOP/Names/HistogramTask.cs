namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var days = new string[31];
        for (var day = 0; day < days.Length; day++)
            days[day] = (day + 1).ToString();

        var birthsCounts = new double[31];
        foreach (var person in names)
        {
            if (person.Name == name && person.BirthDate.Day != 1)
                birthsCounts[person.BirthDate.Day - 1]++;
        }

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'",
            days,
            birthsCounts);
    }
}
