using System.Globalization;
using System.Text.RegularExpressions;
using Common;
using Common.Exceptions;
using Domain.Enums;

namespace Application.Utils;

public static partial class StringHelpers
{
    public static string StandardPhoneNumberFormat(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var convertedDigits = ConvertPersianDigitsToEnglish(input);

        if (!ValidateMobile(new string(convertedDigits)))
        {
            throw new BadRequestException(ErrorMessages.PhoneNumber_Invalid);
        }
        
        return new string(convertedDigits);
    }
    public static bool ValidateMobile(this string input)
    {
        return input.StartsWith('0') && MyRegex().IsMatch(input);
    }
    
    public static string FormatHourLabel(this int hour)
    {
        var nextHour = (hour + 1) % 24;
        return $"{hour:00}:00 - {nextHour:00}:00";
    }

    [GeneratedRegex("^09([01239])([0-9]{8})$")]
    private static partial Regex MyRegex();
}

public static partial class StringHelpers
{
    public static string ToPersianDate(this DateTime date)
    {
        var pc = new PersianCalendar();

        var year = pc.GetYear(date);
        var month = pc.GetMonth(date);
        var day = pc.GetDayOfMonth(date);

        return ToPersianDigits($"{year:0000}/{month:00}/{day:00}");
    }

    public static string ToPersianDigits(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var convertedDigits = ConvertPersianDigitsToEnglish(input);

        return new string(convertedDigits);
    }

    public static string FormatHourLabelPersian(this int hour)
    {
        return FormatHourLabel(hour).ToPersianDigits();
    }
    
    private static char[] ConvertPersianDigitsToEnglish(string input)
    {
        var persianToEnglishDigits = new Dictionary<char, char>
        {
            { '۰', '0' },
            { '۱', '1' },
            { '۲', '2' },
            { '۳', '3' },
            { '۴', '4' },
            { '۵', '5' },
            { '۶', '6' },
            { '۷', '7' },
            { '۸', '8' },
            { '۹', '9' }
        };

        var result = input.ToCharArray();
        for (var i = 0; i < result.Length; i++)
        {
            if (persianToEnglishDigits.TryGetValue(result[i], out var englishDigit))
            {
                result[i] = englishDigit;
            }
        }

        return result;
    }
    
    public static string ConvertSourceToPersian(OrderSource source)
    {
        return source switch
        {
            OrderSource.Cashier => "صندوقدار",
            OrderSource.POS => "پوز",
            OrderSource.Website => "وب سایت",
            _ => "نامشخص"
        };
    }

}