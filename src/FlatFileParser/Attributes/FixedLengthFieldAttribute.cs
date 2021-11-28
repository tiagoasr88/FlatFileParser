using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FixedLengthFieldAttribute : Attribute
    {
        public int StartPosition { get; set; }
        public int Length { get; set; }
        public string DateFormat { get; set; }
        public string CultureInfoName { get; set; }

        public FixedLengthFieldAttribute(int startPosition, int length, string dateFormat = "", string cultureInfoName = "")
        {
            StartPosition = startPosition;
            Length = length;
            DateFormat = dateFormat;
            CultureInfoName = cultureInfoName;
        }
    }
}
