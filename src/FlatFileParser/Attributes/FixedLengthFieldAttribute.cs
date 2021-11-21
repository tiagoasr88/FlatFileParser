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
        public string Format { get; set; }

        public FixedLengthFieldAttribute(int startPosition, int length)
        {
            StartPosition = startPosition;
            Length = length;
            Format = String.Empty;
        }

        public FixedLengthFieldAttribute(int startPosition, int length, string format)
        {
            StartPosition = startPosition;
            Length = length;
            Format = format;
        }
    }
}
