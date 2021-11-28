using FlatFileParser.Attributes;
using FlatFileParser.Converters;
using FlatFileParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlatFileParser.Core
{
    public abstract class FlatFileFixedLengthLine : IFlatFileElement
    {
        public void Read(string line)
        {
            var objProperties = this.GetType().GetProperties();
            foreach (var property in objProperties)
            {
                var attr = (FixedLengthFieldAttribute)property.GetCustomAttributes(typeof(FixedLengthFieldAttribute), false).First();

                var ret = ReadFixedLengthFileField(line, attr.StartPosition, attr.Length);

                property.SetValue(this, DynamicParser.Parse(ret, property.PropertyType, attr.DateFormat, attr.CultureInfoName), null);
            }
        }

        private string ReadFixedLengthFileField(string line, int startPosition, int length) =>
            line.Substring(startPosition, length).Trim();
    }
}
