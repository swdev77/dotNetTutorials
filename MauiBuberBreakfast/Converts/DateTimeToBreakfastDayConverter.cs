using CommunityToolkit.Maui.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBuberBreakfast.Converts;

public class DateTimeToBreakfastDayConverter : BaseConverter<object, string>
{
    public override string DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object DefaultConvertBackReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override object ConvertBackTo(string value, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override string ConvertFrom(object value, CultureInfo culture)
    {
        var dateTime = (DateTime)value;

        return (dateTime.Date - DateTime.Today).TotalDays switch
        {
            0 => "Today",
            1 => "Tomorrow",
            _ => $"{dateTime.DayOfWeek}, {dateTime.Day}.{dateTime.Month}"
        };
    }
}
