using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileParser.Interfaces
{
    public interface IFlatFileElement
    {
        void Read(string line);
    }
}
