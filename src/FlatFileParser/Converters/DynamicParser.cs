using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FlatFileParser.Converters
{
    internal static class DynamicParser
    {
        internal static object Parse(string value, Type type, string format = "")
        {
            switch (type)
            {
                case Type tp when tp == typeof(DateTime):
                    if (string.IsNullOrWhiteSpace(format))
                        return DateTime.Parse(value);
                    else
                        return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
                case Type tp when tp == typeof(DateTimeOffset):
                    if (string.IsNullOrWhiteSpace(format))
                        return DateTimeOffset.Parse(value);
                    else
                        return DateTimeOffset.ParseExact(value, format, CultureInfo.InvariantCulture);
                case Type tp when tp == typeof(Decimal):
                    if (!string.IsNullOrWhiteSpace(format))
                        return decimal.Parse(value, new CultureInfo(format));
                    else
                        return decimal.Parse(value, CultureInfo.InvariantCulture);
                default:
                    return Convert.ChangeType(value, type);
            }
        }
    }
}
