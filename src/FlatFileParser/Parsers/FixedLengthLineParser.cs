using FlatFileParser.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FlatFileParser.Parsers
{
    public class FixedLengthLineParser
    {
        public T Parse<T>(string line) where T : class
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            var objProperties = typeof(T).GetProperties();
            foreach (var property in objProperties)
            {
                var attr = (FixedLengthFieldAttribute)property.GetCustomAttributes(typeof(FixedLengthFieldAttribute), false).First();

                var ret = ReadFixedLengthFileField(line, attr.StartPosition, attr.Length);

                property.SetValue(obj, DynamicConvert(ret, property.PropertyType, attr.Format));
            }
            return obj;
        }

        private string ReadFixedLengthFileField(string line, int startPosition, int length)
        {
            return line.Substring(startPosition, length);
        }

        private object DynamicConvert(string value, Type type, string format)
        {
            if (type == typeof(DateTime))
            {
                if (string.IsNullOrWhiteSpace(format))
                    return DateTime.Parse(value);
                else
                    return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }

            if (type == typeof(Decimal))
            {
                if (!string.IsNullOrWhiteSpace(format))
                    return decimal.Parse(value, new CultureInfo(format));
                else
                    return decimal.Parse(value, CultureInfo.InvariantCulture);
            }

            return Convert.ChangeType(value, type);
        }
    }
}
