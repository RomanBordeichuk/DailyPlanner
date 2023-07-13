namespace DailyPlanner.StaticClasses
{
    public static class DateStatic
    {
        public static DateOnly Date { get; set; } = new DateOnly(
            DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }
}
