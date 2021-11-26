using FlatFileParser.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace FlatFileParserTests
{
    public class UnitTest1
    {
        [Fact]
        public void ParseFile()
        {
            var obj = new ExampleFileDTO();

            obj.Parse(CreateStreamFromString(TestFile()));

            Assert.Equal("H", obj.Header.Identifier);
            Assert.Equal("0001", obj.Header.FileVersion);
            Assert.Equal(new DateTime(2021, 07, 02), obj.Header.Date);

            Assert.Equal("D", obj.Details.First().Identifier);
            Assert.Equal(1, obj.Details.First().Id);
            Assert.Equal("John Doe", obj.Details.First().CustomerName);
            Assert.Equal(new DateTime(2021, 07, 02), obj.Details.First().CreationDate);
            Assert.Equal(100.12m, obj.Details.First().Amount);

            Assert.Equal("T", obj.Trailer.Identifier);
            Assert.Equal(1, obj.Trailer.DetailsCount);
        }

        private string TestFile()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("H000120210702");
            strBuilder.AppendLine("D01John Doe  20210702100,12");
            strBuilder.AppendLine("T0000001");

            return strBuilder.ToString();
        }

        private Stream CreateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}