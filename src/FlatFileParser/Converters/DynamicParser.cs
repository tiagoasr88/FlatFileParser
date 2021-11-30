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
                    return ParseDatetime(value, dateFormat);
                case Type tp when tp == typeof(DateTimeOffset):
                    return ParseDatetimeOffset(value, dateFormat);
                case Type tp when tp == typeof(Decimal):
                    return ParseDecimal(value, cultureInfoName);
                case Type tp when tp == typeof(float):
                    return ParseFloat(value, cultureInfoName);
                case Type tp when tp == typeof(double):
                    return ParseDouble(value, cultureInfoName);
                default:
                    return Convert.ChangeType(value, type);
            }
        }

        private static object ParseDouble(string value, string cultureInfoName)
        {
            if (string.IsNullOrWhiteSpace(cultureInfoName))
                return double.Parse(value, CultureInfo.InvariantCulture);
            else
                return double.Parse(value, new CultureInfo(cultureInfoName));
        }

        private static object ParseFloat(string value, string cultureInfoName)
        {
            if (string.IsNullOrWhiteSpace(cultureInfoName))
                return float.Parse(value, CultureInfo.InvariantCulture);
            else
                return float.Parse(value, new CultureInfo(cultureInfoName));
        }

        private static object ParseDecimal(string value, string cultureInfoName)
        {
            if (string.IsNullOrWhiteSpace(cultureInfoName))
                return decimal.Parse(value, CultureInfo.InvariantCulture);
            else
                return decimal.Parse(value, new CultureInfo(cultureInfoName));
        }

        private static object ParseDatetimeOffset(string value, string dateFormat)
        {
            if (string.IsNullOrWhiteSpace(dateFormat))
                return DateTimeOffset.Parse(value);
            else
                return DateTimeOffset.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
        }

        private static object ParseDatetime(string value, string dateFormat)
        {
            if (string.IsNullOrWhiteSpace(dateFormat))
                return DateTime.Parse(value);
            else
                return DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
        }
    }
}
