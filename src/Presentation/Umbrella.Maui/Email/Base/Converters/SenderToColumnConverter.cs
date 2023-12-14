using System.Globalization;

namespace Umbrella.Maui.Email.Base.Converters;

public class SenderToColumnConverter : IValueConverter
{
    private enum Column { Left = 0, Right = 1 }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ChatSender sender)
        {
            return sender == ChatSender.Human ? (int)Column.Right : (int)Column.Left;
        }

        return (int)Column.Left; // Default to Left column
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

