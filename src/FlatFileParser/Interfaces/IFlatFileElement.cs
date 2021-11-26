using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileParser.Interfaces
{
    public interface IFlatFileElement
    {
        void Parse(string line);
    }
}
