using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FlatFileParser.Converters
{
    internal static class DynamicParser
    {
        internal static object Parse(string value, Type type, string dateFormat = "", string cultureInfoName = "")
        {
            switch (type)
            {
                case Type tp when tp == typeof(DateTime):
                    if (string.IsNullOrWhiteSpace(dateFormat))
                        return DateTime.Parse(value);
                    else
                        return DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
                case Type tp when tp == typeof(DateTimeOffset):
                    if (string.IsNullOrWhiteSpace(dateFormat))
                        return DateTimeOffset.Parse(value);
                    else
                        return DateTimeOffset.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
                case Type tp when tp == typeof(Decimal):
                    if (!string.IsNullOrWhiteSpace(cultureInfoName))
                        return decimal.Parse(value, new CultureInfo(cultureInfoName));
                    else
                        return decimal.Parse(value, CultureInfo.InvariantCulture);
                case Type tp when tp == typeof(float):
                    if (!string.IsNullOrWhiteSpace(cultureInfoName))
                        return float.Parse(value, new CultureInfo(cultureInfoName));
                    else
                        return float.Parse(value, CultureInfo.InvariantCulture);
                case Type tp when tp == typeof(double):
                    if (!string.IsNullOrWhiteSpace(cultureInfoName))
                        return double.Parse(value, new CultureInfo(cultureInfoName));
                    else
                        return double.Parse(value, CultureInfo.InvariantCulture);
                default:
                    return Convert.ChangeType(value, type);
            }
        }
    }
}
