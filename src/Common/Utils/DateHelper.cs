using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Common.Utils;

public static class DateHelper
{
    public static DateTime ParsePersianDate(this string persianDateString)
    {
        var parts = persianDateString.Split('/');

        if (parts.Length != 3 ||
            !int.TryParse(parts[0], out var year) ||
            !int.TryParse(parts[1], out var month) ||
            !int.TryParse(parts[2], out var day))
        {
            throw new FormatException("فرمت تاریخ معتبر نیست");
        }

        var persianCalendar = new PersianCalendar();

        try
        {
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
        catch (Exception)
        {
            throw new ValidationException("تاریخ شمسی وارد شده معتبر نیست");
        }
    }
    public static DateTime ConvertToUtc(DateTime dateTime)
    {
        var utcDateTime = dateTime.ToUniversalTime();
        return utcDateTime;
    }

    public static string ConvertGregorianToPersian(DateTime date)
    {
        var persianCalendar = new PersianCalendar();

        var persianYear = persianCalendar.GetYear(date);
        var persianMonth = persianCalendar.GetMonth(date);
        var persianDay = persianCalendar.GetDayOfMonth(date);

        var persianDateString =
            $"{persianYear:0000}/{persianMonth:00}/{persianDay:00}";

        return persianDateString;
    }
    public static string ConvertGregorianToFullPersian(DateTime date)
    {
        var persianCalendar = new PersianCalendar();

        var persianYear = persianCalendar.GetYear(date);
        var persianMonth = persianCalendar.GetMonth(date);
        var persianDay = persianCalendar.GetDayOfMonth(date);
        var persianHour = date.Hour;
        var persianMinute = date.Minute;
        var persianSecond = date.Second;

        var persianDateString =
            $"{persianYear:0000}/{persianMonth:00}/{persianDay:00} - {persianHour:00}:{persianMinute:00}:{persianSecond:00}";

        return persianDateString;
    }
    public static DateTime UnixTimestampToDateTime(long unixTimeStamp)
    {
        try
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(unixTimeStamp);
        }
        catch (Exception ex)
        {
            return DateTime.MaxValue;
        }
    }
    public static long DateTimeToUnixTimestamp(DateTime idateTime)
    {
        try
        {
            idateTime = new DateTime(idateTime.Year, idateTime.Month, idateTime.Day, idateTime.Hour, idateTime.Minute, idateTime.Second);
            var unixTimeSpan = (idateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).ToLocalTime());
            return long.Parse(unixTimeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture));
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}